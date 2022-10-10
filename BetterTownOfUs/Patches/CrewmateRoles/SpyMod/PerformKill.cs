using System.Linq;
using HarmonyLib;
using Hazel;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.CrewmateRoles.SpyMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class Alert
    {
        public static bool Prefix(KillButton __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Spy);
            if (!flag) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            if (__instance.isCoolingDown) return false;
            if (!__instance.isActiveAndEnabled) return false;
            var systems = ShipStatus.Instance.Systems;
            if (HudManagerUpdate.CheckCommsSab(systems) & !systems[SystemTypes.Sabotage].Cast<SabotageSystemType>().dummy.IsActive) return false;
            var role = Role.GetRole<Spy>(PlayerControl.LocalPlayer);
            if (role.SpyTimer() != 0) return false;
            role.TimeRemaining = CustomGameOptions.SpyDuration;
            return false;
        }
    }
}