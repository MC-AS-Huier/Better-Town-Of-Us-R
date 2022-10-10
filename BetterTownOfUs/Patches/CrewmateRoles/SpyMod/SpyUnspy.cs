using HarmonyLib;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.CrewmateRoles.SpyMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    [HarmonyPriority(Priority.Last)]
    public class SpyUnspy
    {
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Spy))
            {
                var spy = (Spy) role;
                if (spy.IsSpyed)
                    spy.Spyed();
                else if (spy.Enabled) spy.UnSpy();
            }
        }
    }
}