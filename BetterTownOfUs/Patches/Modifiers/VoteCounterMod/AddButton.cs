using System;
using HarmonyLib;
using BetterTownOfUs.Roles.Modifiers;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BetterTownOfUs.Modifiers.VoteCounterMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class AddButton
    {
        private static Sprite ActiveSprite => BetterTownOfUs.VoteCount;
        public static Sprite DisabledSprite => BetterTownOfUs.VoteCountDisabled;


        public static void GenButton(VoteCounter role, int index, bool isDead)
        {
            if (isDead || (byte) index == role.Player.PlayerId)
            {
                role.Buttons.Add(null);
                return;
            }

            var confirmButton = MeetingHud.Instance.playerStates[index].Buttons.transform.GetChild(0).gameObject;
            var newButton = Object.Instantiate(confirmButton, MeetingHud.Instance.playerStates[index].transform);
            var renderer = newButton.GetComponent<SpriteRenderer>();
            var passive = newButton.GetComponent<PassiveButton>();
            renderer.sprite = DisabledSprite;
            newButton.transform.position = confirmButton.transform.position - new Vector3(1.2f, 0.08f, 0f);
            newButton.transform.localScale *= 0.8f;
            newButton.layer = 5;
            newButton.transform.parent = confirmButton.transform.parent.parent;

            passive.OnClick = new Button.ButtonClickedEvent();
            passive.OnClick.AddListener(SetActive(role, index));
            role.Buttons.Add(newButton);
        }


        private static Action SetActive(VoteCounter role, int index)
        {
            void Listener()
            {
                foreach (var button in role.Buttons)
                {
                    if (button == null) continue;
                    button.GetComponent<SpriteRenderer>().sprite = DisabledSprite;
                }
                role.Buttons[index].GetComponent<SpriteRenderer>().sprite =
                    role.TargetId == (byte)index ? DisabledSprite : ActiveSprite;
                role.TargetId =
                    role.TargetId == (byte)index ? byte.MaxValue : (byte)index;
            }

            return Listener;
        }

        public static void Postfix(MeetingHud __instance)
        {
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (!PlayerControl.LocalPlayer.Is(ModifierEnum.VoteCounter)) return;
            var votecounter = Modifier.GetModifier<VoteCounter>(PlayerControl.LocalPlayer);
            if (votecounter.Hidden) return;
            votecounter.TargetId = byte.MaxValue;
            votecounter.Buttons.Clear();
            for (var i = 0; i < __instance.playerStates.Length; i++)
                GenButton(votecounter, i, __instance.playerStates[i].AmDead);
        }
    }
}