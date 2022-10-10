using System;
using HarmonyLib;
using BetterTownOfUs.Roles;
using Object = UnityEngine.Object;

namespace BetterTownOfUs.ImpostorRoles.UndertakerMod
{
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public static class HUDClose
    {
        public static void Postfix()
        {
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Undertaker))
            {
                var role = Role.GetRole<Undertaker>(PlayerControl.LocalPlayer);
                role.DragDropButton.graphic.sprite = BetterTownOfUs.DragSprite;
                role.CurrentlyDragging = null;
                role.LastDragged = DateTime.UtcNow;
            }
        }
    }
}