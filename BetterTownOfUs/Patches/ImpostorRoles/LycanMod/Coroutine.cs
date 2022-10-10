using System.Collections;
using BetterTownOfUs.Roles;
using UnityEngine;
using Reactor;

namespace BetterTownOfUs.ImpostorRoles.LycanMod
{
    public class Coroutine
    {
        private static readonly int BodyColor = Shader.PropertyToID("_BodyColor");
        private static readonly int BackColor = Shader.PropertyToID("_BackColor");

        public static bool Eating(PlayerControl player)
        {
            if (!player.Is(RoleEnum.Lycan)) return false;
            var role = Role.GetRole<Lycan>(player);
            if (!role.Eating) return false;
            return true;
        }

        public static IEnumerator CleanCoroutine(byte bodyId, Lycan role)
        {
            var deadBodies = Object.FindObjectsOfType<DeadBody>();
            role.Eaten = byte.MaxValue;
            foreach (var body in deadBodies)
            {
                if (body.ParentId == bodyId)
                {
                    var renderer = body.bodyRenderer;
                    var backColor = renderer.material.GetColor(BackColor);
                    var bodyColor = renderer.material.GetColor(BodyColor);
                    var newColor = new Color(1f, 1f, 1f, 0f);
                    for (var i = 0; i < 60; i++)
                    {
                        if (body == null) yield break;
                        renderer.color = Color.Lerp(backColor, newColor, i / 60f);
                        renderer.color = Color.Lerp(bodyColor, newColor, i / 60f);
                        yield return null;
                    }
                    Object.Destroy(body.gameObject);
                    role.Eating = false;
                }
            }
        }
    }
}