using System;
using UnityEngine;
using BetterTownOfUs.Extensions;

namespace BetterTownOfUs.Roles
{
    public class Lycan : Role, IVisualAlteration
    {
        public bool Wolfed { get; set; }
        public KillButton _lycanButton;
        public DateTime LastWolfed;
        public float TimeRemaining;
        public byte Eaten { get; set; } = byte.MaxValue;
        public bool Eating { get; set; } = false;
        public Lycan(PlayerControl player) : base(player)
        {
            Name = "Lycan";
            ImpostorText = () => "Eat Crewmates";
            TaskText = () => "Transform you into wolf to eat Crewmates but still discret.";
            Color = Palette.ImpostorRed;
            Wolfed = false;
            LastWolfed = DateTime.UtcNow;
            RoleType = RoleEnum.Lycan;
            AddToRoleHistory(RoleType);
            Faction = Faction.Impostors;
        }

        public KillButton LycanButton
        {
            get => _lycanButton;
            set
            {
                _lycanButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public static VisualAppearance WolfAppear = new VisualAppearance()
        {
            SizeFactor = new Vector3(0.9f, 0.9f, 1f)
        };

        public static GameData.PlayerOutfit WolfOutfit = new GameData.PlayerOutfit()
        {
          ColorId = 9,
          HatId = "wolf",
          SkinId = "",
          VisorId = "",
          PetId = "",
          PlayerName = "Lycan"
        };

        public bool WolfedTiming => TimeRemaining > 0f;

        public void Morph()
        {
            TimeRemaining -= Time.deltaTime;
            Utils.Morph(Player, null);
            if (Player.Data.IsDead)
            {
                TimeRemaining = 0f;
            }
        }

        public void Unmorph()
        {
            Wolfed = false;
            Utils.Unmorph(Player);
            LastWolfed = DateTime.UtcNow;
        }

        public float WolfTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastWolfed;
            var num = CustomGameOptions.WolfCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }

        public bool TryGetModifiedAppearance(out VisualAppearance appearance)
        {
            if (Wolfed)
            {
                appearance = WolfAppear;
                return true;
            }

            appearance = Player.GetDefaultAppearance();
            return false;
        }
    }
}