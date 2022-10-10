using Il2CppSystem.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.CrewmateRoles.SpyMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Spy)) return;
            var role = Role.GetRole<Spy>(PlayerControl.LocalPlayer);
            if (role.SpyButton == null)
            {
                role.SpyButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.SpyButton.graphic.enabled = true;
                role.SpyButton.GetComponent<AspectPosition>().DistanceFromEdge = BetterTownOfUs.ButtonPosition;
                role.SpyButton.gameObject.SetActive(false);
            }

            role.SpyButton.GetComponent<AspectPosition>().Update();
            role.SpyButton.graphic.sprite = BetterTownOfUs.SpySprite;
            role.SpyButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);

            if (role.Enabled)
            {
                role.SpyButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.SpyDuration);
                return;
            }

            role.SpyButton.SetCoolDown(role.SpyTimer(), CustomGameOptions.SpyCd);

            var systems = ShipStatus.Instance.Systems;
            if (CheckCommsSab(systems) & !systems[SystemTypes.Sabotage].Cast<SabotageSystemType>().dummy.IsActive)
            {
                role.SpyButton.graphic.color = Palette.DisabledClear;
                role.SpyButton.graphic.material.SetFloat("_Desat", 1f);
                return;
            }

            role.SpyButton.graphic.color = Palette.EnabledColor;
            role.SpyButton.graphic.material.SetFloat("_Desat", 0f);
        }

        public static bool CheckCommsSab(Dictionary<SystemTypes, ISystemType> systems)
        {
            switch (PlayerControl.GameOptions.MapId)
            {
                default:
                    if (systems[SystemTypes.Comms].Cast<HudOverrideSystemType>().IsActive) return true;
                    break;
                case 1:
                    if (systems[SystemTypes.Comms].Cast<HqHudSystemType>().IsActive) return true;
                    break;
            }
            return false;
        }
    }
}