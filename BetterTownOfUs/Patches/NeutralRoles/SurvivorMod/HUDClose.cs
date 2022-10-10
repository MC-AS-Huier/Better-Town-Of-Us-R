using System;
using HarmonyLib;
using BetterTownOfUs.Roles;
using Object = UnityEngine.Object;

namespace BetterTownOfUs.NeutralRoles.SurvivorMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Survivor))
            {
                var role = Role.GetRole<Survivor>(PlayerControl.LocalPlayer);
                role.LastVested = DateTime.UtcNow;
            }
        }
    }
}