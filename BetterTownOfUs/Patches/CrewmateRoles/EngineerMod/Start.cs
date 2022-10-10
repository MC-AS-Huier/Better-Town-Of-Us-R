using System;
using HarmonyLib;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.CrewmateRoles.EngineerMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__19), nameof(IntroCutscene._CoBegin_d__19.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__19 __instance)
        {
            if (CustomGameOptions.EngineerFixPer != EngineerFixPer.Custom || !CustomGameOptions.EngiHasCooldown) return;
            foreach (var role in Role.GetRoles(RoleEnum.Engineer))
            {
                var engineer = (Engineer) role;
                engineer.LastFix = DateTime.UtcNow;
                engineer.LastFix = engineer.LastFix.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.EngiCooldown);
            }
        }
    }
}