using System.Collections.Generic;
using System.Linq;
using TMPro;
using BetterTownOfUs.Patches;
using UnityEngine;
using Hazel;
using BetterTownOfUs.NeutralRoles.ExecutionerMod;
using BetterTownOfUs.NeutralRoles.GuardianAngelMod;

namespace BetterTownOfUs.Roles.Modifiers
{
    public class Assassin : Ability
    {
        public Dictionary<byte, (GameObject, GameObject, GameObject, GameObject, TMP_Text)> Buttons = new Dictionary<byte, (GameObject, GameObject, GameObject, GameObject, TMP_Text)>();


        private Dictionary<string, Color> ColorMapping = new Dictionary<string, Color>();

        public Dictionary<string, Color> SortedColorMapping;

        private Dictionary<string, Color> EraseColorMapping = new Dictionary<string, Color>();

        public Dictionary<string, Color> EraseSortedColorMapping;

        public Dictionary<byte, string> Guesses = new Dictionary<byte, string>();

        public Dictionary<byte, bool> EraseMode = new Dictionary<byte, bool>();


        public Assassin(PlayerControl player) : base(player)
        {
            Name = "Assassin";
            TaskText = () => "Guess the roles of the people and kill them mid-meeting";
            Color = Patches.Colors.Impostor;
            AbilityType = AbilityEnum.Assassin;

            RemainingKills = CustomGameOptions.AssassinKills;

            // Adds all the roles that have a non-zero chance of being in the game.
            if (CustomGameOptions.MayorOn > 0) ColorMapping.Add("Mayor", Colors.Mayor);
            if (CustomGameOptions.SheriffOn > 0) ColorMapping.Add("Sheriff", Colors.Sheriff);
            if (CustomGameOptions.EngineerOn > 0) ColorMapping.Add("Engineer", Colors.Engineer);
            if (CustomGameOptions.SwapperOn > 0) ColorMapping.Add("Swapper", Colors.Swapper);
            if (CustomGameOptions.InvestigatorOn > 0) ColorMapping.Add("Investigator", Colors.Investigator);
            if (CustomGameOptions.TimeLordOn > 0) ColorMapping.Add("Time Lord", Colors.TimeLord);
            if (CustomGameOptions.MedicOn > 0) ColorMapping.Add("Medic", Colors.Medic);
            if (CustomGameOptions.SeerOn > 0) ColorMapping.Add("Seer", Colors.Seer);
            if (CustomGameOptions.SpyOn > 0) ColorMapping.Add("Spy", Colors.Spy);
            if (CustomGameOptions.SnitchOn > 0 && !CustomGameOptions.AssassinSnitchViaCrewmate) ColorMapping.Add("Snitch", Colors.Snitch);
            if (CustomGameOptions.AltruistOn > 0) ColorMapping.Add("Altruist", Colors.Altruist);
            if (CustomGameOptions.VeteranOn > 0) ColorMapping.Add("Veteran", Colors.Veteran);
            if (CustomGameOptions.TrackerOn > 0) ColorMapping.Add("Tracker", Colors.Tracker);
            if (CustomGameOptions.TrapperOn > 0) ColorMapping.Add("Trapper", Colors.Trapper);
            if (CustomGameOptions.TransporterOn > 0) ColorMapping.Add("Transporter", Colors.Transporter);
            if (CustomGameOptions.MediumOn > 0) ColorMapping.Add("Medium", Colors.Medium);
            if (CustomGameOptions.MysticOn > 0) ColorMapping.Add("Mystic", Colors.Mystic);
            if (CustomGameOptions.DetectiveOn > 0) ColorMapping.Add("Detective", Colors.Detective);

            // Add Neutral roles if enabled
            if (CustomGameOptions.AssassinGuessNeutralBenign)
            {
                if (CustomGameOptions.AmnesiacOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Amnesiac) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Amnesiac)) ColorMapping.Add("Amnesiac", Colors.Amnesiac);
                if (CustomGameOptions.GuardianAngelOn > 0) ColorMapping.Add("Guardian Angel", Colors.GuardianAngel);
                if (CustomGameOptions.SurvivorOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Survivor) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Survivor)) ColorMapping.Add("Survivor", Colors.Survivor);
            }
            if (CustomGameOptions.AssassinGuessNeutralEvil)
            {
                if (CustomGameOptions.ArsonistOn > 0) ColorMapping.Add("Arsonist", Colors.Arsonist);
                if (CustomGameOptions.ExecutionerOn > 0) ColorMapping.Add("Executioner", Colors.Executioner);
                if (CustomGameOptions.JesterOn > 0 || (CustomGameOptions.ExecutionerOn > 0 && CustomGameOptions.OnTargetDead == OnTargetDead.Jester) || (CustomGameOptions.GuardianAngelOn > 0 && CustomGameOptions.GaOnTargetDeath == BecomeOptions.Jester)) ColorMapping.Add("Jester", Colors.Jester);
                if (CustomGameOptions.CannibalOn > 0) ColorMapping.Add("Cannibal", Colors.Cannibal);
            }
            if (CustomGameOptions.AssassinGuessNeutralKilling)
            {
                if (CustomGameOptions.GlitchOn > 0 && !player.Is(RoleEnum.Glitch)) ColorMapping.Add("The Glitch", Colors.Glitch);
                if (CustomGameOptions.PlaguebearerOn > 0 && !player.Is(RoleEnum.Plaguebearer)) ColorMapping.Add("Plaguebearer", Colors.Plaguebearer);
                if (RpcHandling.NumImpostors > 1 && CustomGameOptions.WerewolfOn > 0 && !player.Is(RoleEnum.Werewolf)) ColorMapping.Add("Werewolf", Colors.Werewolf);
                ColorMapping.Add("Juggernaut", Colors.Juggernaut);
            }
            if (CustomGameOptions.NeutralGuess && player.Is(Faction.Neutral))
            {
                if (CustomGameOptions.GrenadierOn > 0) ColorMapping.Add("Grenadier", Colors.Impostor);
                if (CustomGameOptions.SwooperOn > 0) ColorMapping.Add("Swooper", Colors.Impostor);
                if (CustomGameOptions.MorphlingOn > 0) ColorMapping.Add("Morphling", Colors.Impostor);
                if (CustomGameOptions.PoisonerOn > 0) ColorMapping.Add("Poisoner", Colors.Impostor);
                if (RpcHandling.NumImpostors > 1 && CustomGameOptions.UnderdogOn > 0) ColorMapping.Add("Underdog", Colors.Impostor);
                if (CustomGameOptions.LycanOn > 0) ColorMapping.Add("Lycan", Colors.Impostor);
                if (CustomGameOptions.TraitorOn > 0) ColorMapping.Add("Traitor", Colors.Impostor);
                if (RpcHandling.NumImpostors > 1 && CustomGameOptions.JanitorOn > 0) ColorMapping.Add("Janitor", Colors.Impostor);
                if (CustomGameOptions.MinerOn > 0) ColorMapping.Add("Miner", Colors.Impostor);
                if (CustomGameOptions.UndertakerOn > 0) ColorMapping.Add("Undertaker", Colors.Impostor);
                if (CustomGameOptions.BlackmailerOn > 0) ColorMapping.Add("Blackmailer", Colors.Impostor);
            }

            // Add vanilla crewmate if enabled
            if (CustomGameOptions.AssassinCrewmateGuess) ColorMapping.Add("Crewmate", Colors.Crewmate);
            if (CustomGameOptions.AssassinCrewmateGuess && CustomGameOptions.NeutralGuess && player.Is(Faction.Neutral)) ColorMapping.Add("Impostor", Colors.Impostor);
            //Add modifiers if enabled
            if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.BaitOn > 0) ColorMapping.Add("Bait", Colors.Bait);
            if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.DiseasedOn > 0) ColorMapping.Add("Diseased", Colors.Diseased);
            if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.TorchOn > 0) ColorMapping.Add("Torch", Colors.Torch);
            if (PlayerControl.GameOptions.AnonymousVotes && CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.VoteCounterOn > 0) ColorMapping.Add("Vote Counter", Colors.VoteCounter);
            if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.SleuthOn > 0 && !player.Is(ModifierEnum.Sleuth)) ColorMapping.Add("Sleuth", Colors.Sleuth);
            if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.TiebreakerOn > 0 && !player.Is(ModifierEnum.Tiebreaker)) ColorMapping.Add("Tiebreaker", Colors.Sleuth);
            if (CustomGameOptions.AssassinGuessModifiers && CustomGameOptions.BlindOn > 0 && !player.Is(ModifierEnum.Blind)) ColorMapping.Add("Blind", Colors.Sleuth);
            if (CustomGameOptions.AssassinGuessLovers && CustomGameOptions.LoversOn > 0 && !player.Is(ModifierEnum.Lover)) ColorMapping.Add("Lover", Colors.Lovers);

            if (CustomGameOptions.FlashOn > 0) EraseColorMapping.Add("Flash", Colors.Flash);
            if (CustomGameOptions.BaitOn > 0) EraseColorMapping.Add("Bait", Colors.Bait);
            if (CustomGameOptions.GiantOn > 0) EraseColorMapping.Add("Giant", Colors.Giant);
            if (CustomGameOptions.DiseasedOn > 0) EraseColorMapping.Add("Diseased", Colors.Diseased);
            if (CustomGameOptions.ButtonBarryOn > 0) EraseColorMapping.Add("Button Barry", Colors.ButtonBarry);
            if (CustomGameOptions.LoversOn > 0) EraseColorMapping.Add("Lover", Colors.Lovers);
            if (CustomGameOptions.SleuthOn > 0) EraseColorMapping.Add("Sleuth", Colors.Sleuth);
            if (CustomGameOptions.TiebreakerOn > 0) EraseColorMapping.Add("Tiebreaker", Colors.Tiebreaker);
            if (CustomGameOptions.TorchOn > 0) EraseColorMapping.Add("Torch", Colors.Torch);

            // Sorts the list alphabetically. 
            SortedColorMapping = ColorMapping.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            EraseSortedColorMapping = EraseColorMapping.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public bool GuessedThisMeeting { get; set; } = false;

        public int RemainingKills { get; set; }

        public List<string> PossibleGuesses => SortedColorMapping.Keys.ToList();
        public List<string> PossibleModGuesses => EraseSortedColorMapping.Keys.ToList();
    }
}
