using HarmonyLib;
using Hazel;
using BetterTownOfUs.Roles;
using System.Linq;

namespace BetterTownOfUs.ImpostorRoles.LycanMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKill
    {
        public static bool Prefix(KillButton __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Lycan)) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            if (!__instance.isActiveAndEnabled) return false;
            if (__instance.isCoolingDown) return false;
            var role = Role.GetRole<Lycan>(PlayerControl.LocalPlayer);

            if (__instance == role.LycanButton)
            {
                if (role.WolfTimer() != 0) return false;
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.Wolf, SendOption.Reliable, -1);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                role.TimeRemaining = CustomGameOptions.WolfDuration;
                role.Wolfed = true;
                Utils.Morph(role.Player, null, true);
                return false;
            }
             
            return true;
        }
    }
}