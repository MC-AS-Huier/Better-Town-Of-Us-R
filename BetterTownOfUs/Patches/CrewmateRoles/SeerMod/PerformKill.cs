using System;
using HarmonyLib;
using Hazel;
using BetterTownOfUs.Roles;
using UnityEngine;
using Reactor;
using BetterTownOfUs.CrewmateRoles.MedicMod;

namespace BetterTownOfUs.CrewmateRoles.SeerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKill
    {
        public static bool Prefix(KillButton __instance)
        {
            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Seer)) return true;
            var role = Role.GetRole<Seer>(PlayerControl.LocalPlayer);
            if (!PlayerControl.LocalPlayer.CanMove || role.ClosestPlayer == null) return false;
            if (role.SeerTimer() != 0f) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
            if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            if (role.ClosestPlayer == null) return false;
            if (role.ClosestPlayer.IsInfected() || role.Player.IsInfected())
            {
                foreach (var pb in Role.GetRoles(RoleEnum.Plaguebearer)) ((Plaguebearer)pb).RpcSpreadInfection(role.ClosestPlayer, role.Player);
            }
            if (role.ClosestPlayer.IsOnAlert() || role.ClosestPlayer.Is(RoleEnum.Pestilence))
            {
                if (role.Player.IsShielded())
                {
                    var writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                    writer2.Write(PlayerControl.LocalPlayer.GetMedic().Player.PlayerId);
                    writer2.Write(PlayerControl.LocalPlayer.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer2);

                    System.Console.WriteLine(CustomGameOptions.ShieldBreaks + "- shield break");
                    if (CustomGameOptions.ShieldBreaks)
                        role.LastInvestigated = DateTime.UtcNow;
                    StopKill.BreakShield(PlayerControl.LocalPlayer.GetMedic().Player.PlayerId, PlayerControl.LocalPlayer.PlayerId, CustomGameOptions.ShieldBreaks);
                    return false;
                }
                else if (!role.Player.IsProtected())
                {
                    Utils.RpcMurderPlayer(role.ClosestPlayer, PlayerControl.LocalPlayer);
                    return false;
                }
                role.LastInvestigated = DateTime.UtcNow;

                return false;
            }

            var targetId = role.ClosestPlayer.PlayerId;
            var successfulSee = CheckSeerChance(role.ClosestPlayer);

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.Investigate, SendOption.Reliable, -1);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.Write(targetId);
            writer.Write(successfulSee);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            role.LastInvestigated = DateTime.UtcNow;
            if (!successfulSee) return false;
            role.Investigated.Add(role.ClosestPlayer.PlayerId);
            Coroutines.Start(Utils.FlashCoroutine(role.Color));
            return false;
        }

        private static bool CheckSeerChance(PlayerControl target)
        {
            float chance;
            switch (Role.GetRole(target).Faction)
            {
                case Faction.Crewmates:
                    chance = CustomGameOptions.SeerCrewmateChance;
                    break;
                case Faction.Neutral:
                    chance = CustomGameOptions.SeerNeutralChance;
                    break;
                case Faction.Impostors:
                default:
                    chance = CustomGameOptions.SeerImpostorChance;
                    break;
            }

            var seen = BetterTownOfUs.Random.Next(1, 101) <= chance;
            return seen;
        }
    }
}
