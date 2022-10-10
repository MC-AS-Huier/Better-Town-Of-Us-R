using System;
using HarmonyLib;
using Hazel;
using Reactor;
using BetterTownOfUs.Roles;
using UnityEngine;

namespace BetterTownOfUs.NeutralRoles.CannibalMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKillButton

    {
        public static bool Prefix(KillButton __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal)) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            if (__instance.isCoolingDown) return false;
            var role = Role.GetRole<Cannibal>(PlayerControl.LocalPlayer);
            if (role.EatTimer() != 0) return false;
            var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
            if (Vector2.Distance(role.CurrentTarget.TruePosition,
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            var playerId = role.CurrentTarget.ParentId;
            var player = Utils.PlayerById(playerId);
            if (player.IsInfected() || role.Player.IsInfected())
            {
                foreach (var pb in Role.GetRoles(RoleEnum.Plaguebearer)) ((Plaguebearer)pb).RpcSpreadInfection(player, role.Player);
            }

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.CannibalClean, SendOption.Reliable, -1);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.Write(playerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
            if (CustomGameOptions.CannibalCdOn) role.LastEat = DateTime.UtcNow;

            Coroutines.Start(Coroutine.CleanCoroutine(role.CurrentTarget, role));
            return false;
        }
    }
}