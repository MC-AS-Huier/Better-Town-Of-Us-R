using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Reactor.Extensions;
using BetterTownOfUs.Roles;
using BetterTownOfUs.CrewmateRoles.MedicMod;

namespace BetterTownOfUs.CrewmateRoles.SpyMod
{
    [HarmonyPatch(typeof(VitalsMinigame), nameof(VitalsMinigame.Update))]
    public class Vitals
    {
        public static void Postfix(VitalsMinigame __instance)
        {
            if (!CustomGameOptions.SpyVitals || !PlayerControl.LocalPlayer.Is(RoleEnum.Spy)) return;
            var spy = Role.GetRole<Spy>(PlayerControl.LocalPlayer);
            if (!spy.Enabled) return;
            for (var i = 0; i < __instance.vitals.Count; i++)
            {
                ;
                var panel = __instance.vitals[i];
                var info = GameData.Instance.AllPlayers.ToArray()[i];
                if (!panel.IsDead) continue;
                var deadBody = Murder.KilledPlayers.First(x => x.PlayerId == info.PlayerId);
                var num = (float) (DateTime.UtcNow - deadBody.KillTime).TotalMilliseconds;
                var cardio = panel.Cardio.gameObject;
                var tmp = cardio.GetComponent<TMPro.TextMeshPro>();
                if (tmp == null) tmp = cardio.AddComponent<TMPro.TextMeshPro>();
                if (!spy.Enabled)
                {
                    tmp.gameObject.Destroy();
                    return;
                }
                var transform = tmp.transform;
                transform.localPosition = new Vector3(-0.85f, -0.4f, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = Vector3.one / 20;
                tmp.color = Color.red;
                tmp.text = Math.Ceiling(num / 1000) + "s";
            }
        }
    }
}