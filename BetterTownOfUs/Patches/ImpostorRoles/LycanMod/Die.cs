using HarmonyLib;
using Hazel;
using Reactor;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.ImpostorRoles.LycanMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Die))]
    public class Die
    {
        public static void Postfix(PlayerControl __instance)
        {
            __instance.Data.IsDead = true;

            foreach (var role in Role.GetRoles(RoleEnum.Lycan))
            {
                Lycan lycan = (Lycan) role;
                var id = __instance.PlayerId;
                if (lycan.Eaten == id)
                {
                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.WolfClean, SendOption.Reliable, -1);
                    writer.Write(lycan.Player.PlayerId);
                    writer.Write(__instance.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    Coroutines.Start(Coroutine.CleanCoroutine(id, lycan));
                }
            }
        }
    }
}