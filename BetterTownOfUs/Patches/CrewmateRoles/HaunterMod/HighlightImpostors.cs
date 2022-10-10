using HarmonyLib;
using BetterTownOfUs.Roles;
using Hazel;

namespace BetterTownOfUs.CrewmateRoles.HaunterMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HighlightImpostors
    {
        public static void UpdateMeeting(MeetingHud __instance)
        {
            foreach (var state in __instance.playerStates)
            {
                var player = Utils.PlayerById(state.TargetPlayerId);
                if (player.Is(Faction.Impostors))
                    state.NameText.color = Palette.ImpostorRed;
                if (player.Is(Faction.Neutral) && CustomGameOptions.HaunterRevealsNeutrals)
                {
                    var role = Role.GetRole(player);
                    state.NameText.color = role.Color;
                }
            }
        }
        public static void Postfix(HudManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Haunter)) return;
            var role = Role.GetRole<Haunter>(PlayerControl.LocalPlayer);
            if (!role.CompletedTasks || role.Caught) return;
            if (MeetingHud.Instance)
            {
                UpdateMeeting(MeetingHud.Instance);
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte)CustomRPC.HaunterFinished, SendOption.Reliable, -1);
                writer.Write(role.Player.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }
        }
    }
}