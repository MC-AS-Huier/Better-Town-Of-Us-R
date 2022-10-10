using System;
using HarmonyLib;
using BetterTownOfUs.Roles;
using Object = UnityEngine.Object;

namespace BetterTownOfUs.CrewmateRoles.SheriffMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            foreach (var role in Role.GetRoles(RoleEnum.Sheriff))
            {
                var sheriff = (Sheriff) role;
                sheriff.LastKilled = DateTime.UtcNow;
                sheriff.FirstRound = true;
            }
        }
    }
}