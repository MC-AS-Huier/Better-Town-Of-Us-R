using HarmonyLib;
using BetterTownOfUs.Roles;
using BetterTownOfUs.Extensions;

namespace BetterTownOfUs.CrewmateRoles.MedicMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public class HUDProtect
    {
        public static void Postfix(PlayerControl __instance)
        {
            UpdateProtectButton(__instance);
        }

        public static void UpdateProtectButton(PlayerControl __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Medic)) return;
            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var protectButton = DestroyableSingleton<HudManager>.Instance.KillButton;

            var role = Role.GetRole<Medic>(PlayerControl.LocalPlayer);

            if (isDead || role.UsedAbility)
            {
                protectButton.gameObject.SetActive(false);
                return;
            }
            else
            {
                protectButton.gameObject.SetActive(!MeetingHud.Instance);
                protectButton.SetCoolDown(0f, 1f);
                Utils.SetTarget(ref role.ClosestPlayer, protectButton);
            }

            var renderer = protectButton.graphic;
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
