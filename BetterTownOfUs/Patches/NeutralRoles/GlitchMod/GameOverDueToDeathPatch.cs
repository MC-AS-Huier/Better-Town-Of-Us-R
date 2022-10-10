using HarmonyLib;

namespace BetterTownOfUs.NeutralRoles.GlitchMod
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.IsGameOverDueToDeath))]
    internal class GameOverDueToDeathPatch
    {
        public static void Postfix(out bool __result)
        {
            __result = false;
        }
    }
}