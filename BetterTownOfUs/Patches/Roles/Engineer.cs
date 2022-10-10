using System;
using TMPro;
using BetterTownOfUs.CrewmateRoles.EngineerMod;
namespace BetterTownOfUs.Roles
{
    public class Engineer : Role
    {
        public float EngiCooldown { get; set; }
        public float EngiFixPerRound { get; set; }
        public float EngiFixPerGame { get; set; }
        public DateTime LastFix { get; set; }
        public DateTime LastVent { get; set; }
        public TextMeshPro UsesText;
        public float TimeRemaining;
        public Engineer(PlayerControl player) : base(player)
        {
            Name = "Engineer";
            ImpostorText = () => "Maintain important systems on the ship";
            TaskText = () => "Vent and fix a sabotage from anywhere!";
            Color = Patches.Colors.Engineer;
            RoleType = RoleEnum.Engineer;
            EngiCooldown = CustomGameOptions.EngiCooldown;
            EngiFixPerRound = CustomGameOptions.EngineerFixPer  ==  EngineerFixPer.Custom ? CustomGameOptions.EngiFixPerRound : 1;
            EngiFixPerGame = CustomGameOptions.EngiFixPerGame;


            AddToRoleHistory(RoleType);
        }

        public float EngineerTimer(DateTime last, float timer)
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - last;
            var num = timer * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}