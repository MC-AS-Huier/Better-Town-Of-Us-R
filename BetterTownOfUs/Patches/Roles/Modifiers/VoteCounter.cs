using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BetterTownOfUs.Roles.Modifiers
{
    public class VoteCounter : Modifier
    {
        public VoteCounter(PlayerControl player) : base(player)
        {
            Name = "Vote Counter";
            TaskText = () => "Learn votes of others!";
            TaskText = () =>
                TasksDone
                    ? "Learn votes of others!"
                    : "";
            Hidden = true;
            Color = Patches.Colors.VoteCounter;
            ModifierType = ModifierEnum.VoteCounter;
        }

        public readonly List<GameObject> Buttons = new List<GameObject>();

        public byte TargetId = byte.MaxValue;

        protected internal int TasksLeft => Player.Data.Tasks.ToArray().Count(x => !x.Complete);

        public bool TasksDone => TasksLeft <= 0;
    }
}