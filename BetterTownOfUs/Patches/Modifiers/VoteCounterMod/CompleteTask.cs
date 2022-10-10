using System.Linq;
using HarmonyLib;
using Reactor;
using BetterTownOfUs.Roles;
using BetterTownOfUs.Roles.Modifiers;
using UnityEngine;

namespace BetterTownOfUs.Modifiers.VoteCounterMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CompleteTask))]
    public class CompleteTask
    {

        public static void Postfix(PlayerControl __instance)
        {
            if (!__instance.Is(ModifierEnum.VoteCounter)) return;
            if (__instance.Data.IsDead) return;
            var taskinfos = __instance.Data.Tasks.ToArray();

            var tasksLeft = taskinfos.Count(x => !x.Complete);
            var role = Role.GetRole(__instance);
            var modifier = Modifier.GetModifier<VoteCounter>(__instance);
            switch (tasksLeft)
            {
                case 0:
                    modifier.Hidden = false;
                    var modTask = new GameObject(modifier.Name + "Task").AddComponent<ImportantTextTask>();
                    modTask.transform.SetParent(__instance.transform, false);
                    modTask.Text =
                        $"{modifier.ColorString}Modifier: {modifier.Name}\n{modifier.TaskText()}</color>";
                    __instance.myTasks.Insert(1, modTask);
                    if (PlayerControl.LocalPlayer.Is(ModifierEnum.VoteCounter))
                    {
                        Coroutines.Start(Utils.FlashCoroutine(modifier.Color));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}