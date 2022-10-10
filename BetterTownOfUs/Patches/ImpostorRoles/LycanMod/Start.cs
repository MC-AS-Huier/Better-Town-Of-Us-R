using System;
using HarmonyLib;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.ImpostorRoles.LycanMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__19), nameof(IntroCutscene._CoBegin_d__19.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__19 __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Lycan))
            {
                var lycan = (Lycan) role;
                lycan.LastWolfed = DateTime.UtcNow;
                lycan.LastWolfed = lycan.LastWolfed.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.WolfCd);
            }
        }
    }
}