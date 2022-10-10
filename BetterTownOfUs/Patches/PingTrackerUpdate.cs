using HarmonyLib;
using UnityEngine;

namespace BetterTownOfUs
{
    //[HarmonyPriority(Priority.VeryHigh)] // to show this message first, or be overrided if any plugins do
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {

        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
            position.AdjustPosition();

            __instance.text.text =
                ("BetterTownOfUs v" + BetterTownOfUs.DisplayVersion).ColoredString("#018001FF") +
                $"\nPing: {AmongUsClient.Instance.Ping}ms\n" +
                "Modified By: Vincent Vision and JMC\n".ColoredString("#018001FF") +
                (!MeetingHud.Instance
                    ? "Modded By: the team from, ToU - R</color>".ColoredString("#00FF00FF") : ""); 
                
        }
    }
}
