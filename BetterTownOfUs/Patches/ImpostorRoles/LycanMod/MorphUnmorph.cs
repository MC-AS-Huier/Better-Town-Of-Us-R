using HarmonyLib;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.ImpostorRoles.LycanMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class MorphUnmorph
    {
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Lycan))
            {
                var lycan = (Lycan) role;
                if (lycan.WolfedTiming)
                    lycan.Morph();
                else if (lycan.Wolfed) lycan.Unmorph();
            }
        }
    }
}