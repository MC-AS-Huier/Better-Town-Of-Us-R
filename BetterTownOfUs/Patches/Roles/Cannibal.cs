using System;
using TMPro;
using Hazel;

namespace BetterTownOfUs.Roles
{
    public class Cannibal : Role
    {
        public KillButton _cleanButton;
        public DateTime LastEat { get; set; }
        public int EatNeeded;
        public bool CannibalWins;
        public TextMeshPro UsesText;

        public Cannibal(PlayerControl player) : base(player)
        {
            EatNeeded = CustomGameOptions.EatNeeded == 0 ? PlayerControl.AllPlayerControls._size / 4 : CustomGameOptions.EatNeeded;
            string body = EatNeeded == 1 ? "Body" : "Bodies";
            Name = "Cannibal";
            ImpostorText = () => "Eat bodies to win";
            TaskText = () => $"You're hungry, you need to eat {EatNeeded} Dead {body} to Win\nFake Tasks:";
            Color = Patches.Colors.Cannibal;
            RoleType = RoleEnum.Cannibal;
            AddToRoleHistory(RoleType);
            Faction = Faction.Neutral;
        }

        public DeadBody CurrentTarget { get; set; }

        public KillButton CleanButton
        {
            get => _cleanButton;
            set
            {
                _cleanButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        internal override bool EABBNOODFGL(ShipStatus __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected || EatNeeded > 0) return true;
            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte) CustomRPC.CannibalWin, SendOption.Reliable, -1);
            writer.Write(Player.PlayerId);
            Wins();
            AmongUsClient.Instance.FinishRpcImmediately(writer);
            Utils.EndGame();
            return false;
        }


        public void Wins()
        {
            CannibalWins = true;
        }

        public void Loses()
        {
            LostByRPC = true;
        }

        protected override void IntroPrefix(IntroCutscene._ShowTeam_d__21 __instance)
        {
            var cannibalTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            cannibalTeam.Add(PlayerControl.LocalPlayer);
            __instance.teamToShow = cannibalTeam;
        }

        public float EatTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastEat;
            var num = CustomGameOptions.CannibalCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}