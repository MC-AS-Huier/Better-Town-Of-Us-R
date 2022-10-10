using HarmonyLib;
using BetterTownOfUs.Roles;
using UnityEngine;

namespace BetterTownOfUs.ImpostorRoles.LycanMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Lycan)) return;
            var role = Role.GetRole<Lycan>(PlayerControl.LocalPlayer);
            if (role.LycanButton == null)
            {
                role.LycanButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.LycanButton.graphic.enabled = true;
                role.LycanButton.GetComponent<AspectPosition>().DistanceFromEdge = BetterTownOfUs.ButtonPosition;
                role.LycanButton.gameObject.SetActive(false);
            }

            role.LycanButton.GetComponent<AspectPosition>().Update();
            role.LycanButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
            role.LycanButton.graphic.sprite = BetterTownOfUs.LycanWolf;

            if (role.Wolfed)
            {
                role.LycanButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.WolfDuration);
                return;
            }
            role.LycanButton.SetCoolDown(role.WolfTimer(), CustomGameOptions.WolfCd);
            role.LycanButton.graphic.color = Palette.EnabledColor;
            role.LycanButton.graphic.material.SetFloat("_Desat", 0f);
        }
    }
}