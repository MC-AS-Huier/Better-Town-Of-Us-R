using System;
using HarmonyLib;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.CrewmateRoles.SpyMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__19), nameof(IntroCutscene._CoBegin_d__19.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__19 __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Spy))
            {
                var spy = (Spy)role;
                spy.LastSpyed = DateTime.UtcNow;
                spy.LastSpyed = spy.LastSpyed.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.SpyCd);
            }
        }
    }
}