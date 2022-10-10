using System;
using HarmonyLib;
using BetterTownOfUs.Roles;
using Object = UnityEngine.Object;

namespace BetterTownOfUs.ImpostorRoles.SwooperMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Swooper))
            {
                var role = Role.GetRole<Swooper>(PlayerControl.LocalPlayer);
                role.LastSwooped = DateTime.UtcNow;
            }
        }
    }
}