using System;
using HarmonyLib;
using BetterTownOfUs.Roles;
using Object = UnityEngine.Object;

namespace BetterTownOfUs.ImpostorRoles.LycanMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Lycan))
            {
                var role = Role.GetRole<Lycan>(PlayerControl.LocalPlayer);
                role.LastWolfed = DateTime.UtcNow;
                role.Eating = false;
            }
        }
    }
}