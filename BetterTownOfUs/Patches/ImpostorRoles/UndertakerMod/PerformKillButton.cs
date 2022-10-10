using System;
using HarmonyLib;
using Hazel;
using BetterTownOfUs.Extensions;
using Reactor.Extensions;
using BetterTownOfUs.Roles;
using UnityEngine;

namespace BetterTownOfUs.ImpostorRoles.UndertakerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKillButton
    {
        public static bool Prefix(KillButton __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Undertaker);
            if (!flag) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Undertaker>(PlayerControl.LocalPlayer);

            if (__instance == role.DragDropButton)
            {
                if (role.DragDropButton.graphic.sprite == BetterTownOfUs.DragSprite)
                {
                    if (__instance.isCoolingDown) return false;
                    if (!__instance.enabled) return false;
                    var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
                    if (Vector2.Distance(role.CurrentTarget.TruePosition,
                        PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
                    var playerId = role.CurrentTarget.ParentId;

                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte) CustomRPC.Drag, SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    writer.Write(playerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    role.CurrentlyDragging = role.CurrentTarget;

                    KillButtonTarget.SetTarget(__instance, null, role);
                    __instance.graphic.sprite = BetterTownOfUs.DropSprite;
                    return false;
                }
                else
                {
                    if (!__instance.enabled) return false;
                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte) CustomRPC.Drop, SendOption.Reliable, -1);
                    writer.Write(PlayerControl.LocalPlayer.PlayerId);
                    Vector3 position = PlayerControl.LocalPlayer.GetTruePosition();


                    float z = Utils.CalculateZ(position);
                    position.z = z;

                    writer.Write(position);
                    writer.Write(position.z);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    var body = role.CurrentlyDragging;
                    body.bodyRenderer.material.SetFloat("_Outline", 0f);
                    role.CurrentlyDragging = null;
                    __instance.graphic.sprite = BetterTownOfUs.DragSprite;
                    role.LastDragged = DateTime.UtcNow;

                    body.transform.position = position;


                    return false;
                }
            }

            return true;
        }
    }
}
