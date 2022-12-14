using HarmonyLib;
using Hazel;
using BetterTownOfUs.Roles;

namespace BetterTownOfUs.NeutralRoles.GuardianAngelMod
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.RpcEndGame))]
    public class EndGame
    {
        public static bool Prefix(ShipStatus __instance, [HarmonyArgument(0)] GameOverReason reason)
        {
            foreach (var role in Role.AllRoles)
                if (role.RoleType == RoleEnum.GuardianAngel && ((GuardianAngel)role).target.Is(Faction.Impostors))
                {
                    if (reason != GameOverReason.HumansByVote && reason != GameOverReason.HumansByTask)
                    {
                        ((GuardianAngel)role).ImpTargetWin();

                        var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                            (byte)CustomRPC.GAImpWin,
                            SendOption.Reliable, -1);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                    }
                    else
                    {
                        ((GuardianAngel)role).ImpTargetLose();

                        var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                            (byte)CustomRPC.GAImpLose,
                            SendOption.Reliable, -1);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                    }
                    return true;
                }
            return true;
        }
    }
}