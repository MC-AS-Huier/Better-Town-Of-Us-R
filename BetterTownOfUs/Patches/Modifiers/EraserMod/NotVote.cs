using HarmonyLib;
using BetterTownOfUs.Roles.Modifiers;

namespace BetterTownOfUs.Modifiers.EraserMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))] // BBFDNCCEJHI
    public static class VotingComplete
    {
        public static void Postfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Is(AbilityEnum.Eraser))
            {
                var eraser = Ability.GetAbility<Eraser>(PlayerControl.LocalPlayer);
                ShowHideButtons.HideButtons(eraser);
            }
        }
    }
}