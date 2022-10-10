using System;
using HarmonyLib;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.NeutralRoles.CannibalMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__19), nameof(IntroCutscene._CoBegin_d__19.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__19 __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Cannibal))
            {
                var cannibal = (Cannibal) role;
                cannibal.LastEat = DateTime.UtcNow.AddSeconds(-CustomGameOptions.CannibalCd);
            }
        }
    }
}