using System.Linq;
using HarmonyLib;
using BetterTownOfUs.Roles;
using BetterTownOfUs.Extensions;

namespace BetterTownOfUs
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.SetTarget))]
    public class SetTarget
    {
        private static PlayerControl Target;

        public static bool Prefix(ref PlayerControl target, [HarmonyArgument(0)] KillButton __instance)
        {
            if (KillButtonSprite.Kill != null && __instance.graphic.sprite != KillButtonSprite.Kill) return false;
            target = Target;
            return true;
        }

        [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
        public static class Update
        {
            public static void Postfix(HudManager __instance)
            {
                if (PlayerControl.AllPlayerControls.Count <= 1) return;
                var player = PlayerControl.LocalPlayer;
                if (player == null) return;
                if (player.Data == null) return;
                if (__instance.KillButton == null) return;
                if (Role.GetRole(player) == null) return;
                Utils.SetTarget(ref Target, __instance.KillButton, float.NaN, PlayerControl.AllPlayerControls.ToArray().Where(x =>
                {
                    if (player.Data.IsImpostor()) return !x.Is(Faction.Impostors);
                    return x;
                }).ToList(), killButton:true);
            }
        }
    }
}