using System;
using UnityEngine;

namespace BetterTownOfUs.Roles
{
    public class Spy : Role
    {
        public KillButton _spyButton;
        public bool Enabled;
        public DateTime LastSpyed;
        public float TimeRemaining;

        public Spy(PlayerControl player) : base(player)
        {
            Name = "Spy";
            ImpostorText = () => "Snoop around and find stuff out";
            TaskText = () => "Spy on people and find the Impostors";
            Color = Patches.Colors.Spy;
            RoleType = RoleEnum.Spy;
            AddToRoleHistory(RoleType);
        }

        public bool IsSpyed => TimeRemaining > 0f;
        public KillButton SpyButton
        {
            get => _spyButton;
            set
            {
                _spyButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public float SpyTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastSpyed;
            ;
            var num = CustomGameOptions.SpyCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

        public void Spyed()
        {
            Enabled = true;
            TimeRemaining -= Time.deltaTime;
            if (Player.Data.IsDead) TimeRemaining = 0f;
        }


        public void UnSpy()
        {
            Enabled = false;
            LastSpyed = DateTime.UtcNow;
        }
    }
}