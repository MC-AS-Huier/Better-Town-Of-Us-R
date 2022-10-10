using System.Linq;
using HarmonyLib;
using BetterTownOfUs.Roles;
using BetterTownOfUs.Extensions;

namespace BetterTownOfUs.CrewmateRoles.SeerMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public class HudInvestigate
    {
        public static void Postfix(PlayerControl __instance)
        {
            UpdateInvButton(__instance);
        }

        public static void UpdateInvButton(PlayerControl __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Seer)) return;
            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var investigateButton = DestroyableSingleton<HudManager>.Instance.KillButton;

            var role = Role.GetRole<Seer>(PlayerControl.LocalPlayer);

            var notInvestigated = PlayerControl.AllPlayerControls
                .ToArray()
                .Where(x => !role.Investigated.Contains(x.PlayerId))
                .ToList();

            if (isDead)
            {
                investigateButton.gameObject.SetActive(false);
            }
            else
            {
                investigateButton.gameObject.SetActive(!MeetingHud.Instance);
                investigateButton.SetCoolDown(role.SeerTimer(), CustomGameOptions.SeerCd);

                Utils.SetTarget(ref role.ClosestPlayer, investigateButton, float.NaN, notInvestigated);
            }

            var renderer = investigateButton.graphic;
            if (role.ClosestPlayer != null)
            {
                renderer.color = Palette.EnabledColor;
                renderer.material.SetFloat("_Desat", 0f);
            }
            else
            {
                renderer.color = Palette.DisabledClear;
                renderer.material.SetFloat("_Desat", 1f);
            }

            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (role.ClosestPlayer != null && player == role.ClosestPlayer && __instance.enabled)
                {
                    player.myRend().material.SetFloat("_Outline", 1f);
                    player.myRend().material.SetColor("_OutlineColor", Palette.CrewmateBlue);
                    continue;
                }
                player.myRend().material.SetFloat("_Outline", 0f);
            }
        }
    }
}
