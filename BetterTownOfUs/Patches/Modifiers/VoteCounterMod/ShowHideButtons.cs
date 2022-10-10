using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Hazel;
using Reactor;
using BetterTownOfUs.CrewmateRoles.MayorMod;
using BetterTownOfUs.Extensions;
using BetterTownOfUs.Roles.Modifiers;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.UI;

namespace BetterTownOfUs.Modifiers.VoteCounterMod
{
    public class ShowHideButtons
    {
        [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.VotingComplete))] // BBFDNCCEJHI
        public static class VotingComplete
        {
            public static void Postfix(MeetingHud __instance)
            {
                if (PlayerControl.LocalPlayer.Is(ModifierEnum.VoteCounter))
                {
                    var votecounter = Modifier.GetModifier<VoteCounter>(PlayerControl.LocalPlayer);
                    foreach (var button in votecounter.Buttons)
                    {
                        if (button == null) continue;
                        button.SetActive(false);
                        button.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                    }
                }
            }
        }
    }
}