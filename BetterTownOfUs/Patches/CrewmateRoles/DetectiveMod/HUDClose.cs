using System;
using HarmonyLib;
using BetterTownOfUs.Roles;
using Object = UnityEngine.Object;

namespace BetterTownOfUs.CrewmateRoles.DetectiveMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            foreach (var role in Role.GetRoles(RoleEnum.Detective))
            {
                var detective = (Detective) role;
                detective.LastExamined = DateTime.UtcNow;
                detective.LastExamined = detective.LastExamined.AddSeconds(CustomGameOptions.InitialExamineCd - CustomGameOptions.ExamineCd);
            }
        }
    }
}