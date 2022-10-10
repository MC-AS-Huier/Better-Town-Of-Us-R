using System.Linq;
using HarmonyLib;
using BetterTownOfUs.Extensions;
using BetterTownOfUs.Roles;
using UnityEngine;

namespace BetterTownOfUs.CrewmateRoles.SeerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class Update
    {
        public static string NameText(PlayerControl player, string str = "", bool meeting = false)
        {
            if (CamouflageUnCamouflage.IsCamoed)
            {
                if (meeting) return player.name + str;

                return "";
            }

            return player.name + str;
        }

        private static void UpdateNameText(PlayerControl player, TMPro.TextMeshPro nameText)
        {
            var roleType = Utils.GetRole(player);
            switch (roleType)
            {
                case RoleEnum.Crewmate:
                    nameText.color =
                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? Color.green : Color.white;
                    nameText.text = NameText(player,
                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Crew)" : "");
                    break;
                case RoleEnum.Impostor:
                    nameText.color = CustomGameOptions.SeerInfo == SeerInfo.Faction
                        ? Color.red
                        : Palette.ImpostorRed;
                    nameText.text = NameText(player,
                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Imp)" : "");
                    break;
                default:
                    var role = Role.GetRole(player);
                    nameText.color = CustomGameOptions.SeerInfo == SeerInfo.Faction
                        ? FactionColor(role)
                        : role.Color;
                    nameText.text = NameText(player,
                        CustomGameOptions.SeerInfo == SeerInfo.Role ? $" ({role.Name})" : "");
                    break;
                }
            
        }

        private static Color FactionColor(Role role)
        {
            switch (role.Faction)
            {
                case Faction.Crewmates:
                    return Color.green;
                case Faction.Impostors:
                    return Color.red;
                case Faction.Neutral:
                    return CustomGameOptions.NeutralRed ? Color.red : Color.grey;
                default:
                    return Color.white;
            };
        }

        private static void UpdateMeeting(MeetingHud __instance, Seer seer)
        {
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (!seer.Investigated.Contains(player.PlayerId)) continue;
                foreach (var state in __instance.playerStates)
                {
                    if (player.PlayerId != state.TargetPlayerId) continue;
                    UpdateNameText(player, state.NameText);
                }
            }
        }

        [HarmonyPriority(Priority.Last)]
        private static void Postfix(HudManager __instance)

        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Seer)) return;
            var seer = Role.GetRole<Seer>(PlayerControl.LocalPlayer);
            if (MeetingHud.Instance != null) UpdateMeeting(MeetingHud.Instance, seer);


            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (!seer.Investigated.Contains(player.PlayerId)) continue;
                player.cosmetics.nameText.transform.localPosition = new Vector3(0f, 2f, -0.5f);
                UpdateNameText(player, player.cosmetics.nameText);
            }
        }
    }
}