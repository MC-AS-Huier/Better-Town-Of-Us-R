using System.Linq;
using HarmonyLib;
using BetterTownOfUs.Roles;
using UnityEngine;

namespace BetterTownOfUs.CrewmateRoles.EngineerMod
{
    [HarmonyPatch(typeof(HudManager))]
    public class KillButtonSprite
    {
        private static Sprite Sprite => BetterTownOfUs.EngineerFix;

        private static void UpdtateEngineerVentTimer(HudManager __instance, Engineer role)
        {
            var ventButton = __instance.ImpostorVentButton;
            if (ventButton.cooldownTimerText == null) ventButton.cooldownTimerText = Object.Instantiate(__instance.KillButton.cooldownTimerText, ventButton.transform);
            if (PlayerControl.LocalPlayer.inVent)
            {
                if (role.TimeRemaining <= 0)
                {
                    ventButton.DoClick();
                }
                ventButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.EngiVentDuration);
                role.TimeRemaining -= Time.deltaTime;
            }
            else
            {
                ventButton.SetCoolDown(role.EngineerTimer(role.LastVent, CustomGameOptions.EngiVentCooldown), CustomGameOptions.EngiVentCooldown);
            }
        }

        [HarmonyPatch(nameof(HudManager.Update))]
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Engineer)) return;
            var role = Role.GetRole<Engineer>(PlayerControl.LocalPlayer);
            if (CustomGameOptions.EngiHasVentCooldown) UpdtateEngineerVentTimer(__instance, role);
            if (__instance.KillButton == null) return;

            if (role.UsesText == null && role.EngiFixPerRound > 0 && role.EngiFixPerGame > 0)
            {
                role.UsesText = Object.Instantiate(__instance.KillButton.cooldownTimerText, __instance.KillButton.transform);
                role.UsesText.gameObject.SetActive(true);
                role.UsesText.transform.localPosition = new Vector3(
                    role.UsesText.transform.localPosition.x + 0.26f,
                    role.UsesText.transform.localPosition.y + 0.29f,
                    role.UsesText.transform.localPosition.z);
                role.UsesText.transform.localScale = role.UsesText.transform.localScale * 0.65f;
                role.UsesText.alignment = TMPro.TextAlignmentOptions.Right;
                role.UsesText.fontStyle = TMPro.FontStyles.Bold;
            }
            if (role.UsesText != null)
            {
                role.UsesText.text = role.EngiFixPerRound + "/" + role.EngiFixPerGame;
            }
            
            __instance.KillButton.graphic.sprite = Sprite;
            if ((CustomGameOptions.EngineerFixPer == EngineerFixPer.Custom) && CustomGameOptions.EngiHasCooldown) __instance.KillButton.SetCoolDown(role.EngineerTimer(role.LastFix, CustomGameOptions.EngiCooldown), CustomGameOptions.EngiCooldown);
            else __instance.KillButton.SetCoolDown(0f, 10f);
            __instance.KillButton.SetCoolDown(0f, 10f);
            __instance.KillButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead &&
                                                       __instance.UseButton.isActiveAndEnabled && !MeetingHud.Instance && role.EngiFixPerRound > 0 && role.EngiFixPerGame > 0);
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (!ShipStatus.Instance) return;
            var system = ShipStatus.Instance.Systems[SystemTypes.Sabotage].Cast<SabotageSystemType>();
            if (system == null) return;
            var specials = system.specials.ToArray();
            var dummyActive = system.dummy.IsActive;
            var sabActive = specials.Any(s => s.IsActive);
            var renderer = __instance.KillButton.graphic;
            if (sabActive & !dummyActive & role.EngiFixPerRound > 0 & role.EngiFixPerGame > 0 & __instance.KillButton.enabled)
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
                role.UsesText.color = Palette.EnabledColor;
                role.UsesText.material.SetFloat("_Desat", 0f);
                return;
            }

            renderer.color = Palette.DisabledClear;
            renderer.material.SetFloat("_Desat", 1f);
            role.UsesText.color = Palette.DisabledClear;
            role.UsesText.material.SetFloat("_Desat", 1f);
        }
    }
}