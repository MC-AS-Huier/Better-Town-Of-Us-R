using System;
using System.Collections.Generic;

namespace BetterTownOfUs.CustomOption
{
    public class Generate
    {
        public static CustomNumberOption MaxPlayers;
        public static CustomHeaderOption BetterPolus;
        public static CustomToggleOption VentImprovements;
        public static CustomToggleOption VitalsLab;
        public static CustomToggleOption ColdTempDeathValley;
        public static CustomToggleOption WifiChartCourseSwap;
        public static CustomHeaderOption CrewInvestigativeRoles;
        public static CustomNumberOption DetectiveOn;
        public static CustomNumberOption HaunterOn;
        public static CustomNumberOption InvestigatorOn;
        public static CustomNumberOption MysticOn;
        public static CustomNumberOption SeerOn;
        public static CustomNumberOption SnitchOn;
        public static CustomNumberOption SpyOn;
        public static CustomNumberOption TrackerOn;
        public static CustomNumberOption TrapperOn;

        public static CustomHeaderOption CrewProtectiveRoles;
        public static CustomNumberOption AltruistOn;
        public static CustomNumberOption MedicOn;

        public static CustomHeaderOption CrewKillingRoles;
        public static CustomNumberOption SheriffOn;
        public static CustomNumberOption VeteranOn;

        public static CustomHeaderOption CrewSupportRoles;
        public static CustomNumberOption EngineerOn;
        public static CustomNumberOption MayorOn;
        public static CustomNumberOption MediumOn;
        public static CustomNumberOption SwapperOn;
        public static CustomNumberOption TimeLordOn;
        public static CustomNumberOption TransporterOn;

        public static CustomHeaderOption NeutralBenignRoles;
        public static CustomNumberOption AmnesiacOn;
        public static CustomNumberOption GuardianAngelOn;
        public static CustomNumberOption SurvivorOn;

        public static CustomHeaderOption NeutralEvilRoles;
        public static CustomNumberOption ExecutionerOn;
        public static CustomNumberOption JesterOn;
        public static CustomNumberOption CannibalOn;
        public static CustomNumberOption PhantomOn;

        public static CustomHeaderOption NeutralKillingRoles;
        public static CustomNumberOption ArsonistOn;
        public static CustomNumberOption PlaguebearerOn;
        public static CustomNumberOption GlitchOn;
        public static CustomNumberOption WerewolfOn;

        public static CustomHeaderOption ImpostorConcealingRoles;
        public static CustomNumberOption MorphlingOn;
        public static CustomNumberOption SwooperOn;
        public static CustomNumberOption GrenadierOn;
        //public static CustomNumberOption CamouflagerOn;

        public static CustomHeaderOption ImpostorKillingRoles;
        public static CustomNumberOption PoisonerOn;
        public static CustomNumberOption TraitorOn;
        public static CustomNumberOption UnderdogOn;
        public static CustomNumberOption LycanOn;

        public static CustomHeaderOption ImpostorSupportRoles;
        public static CustomNumberOption BlackmailerOn;
        public static CustomNumberOption JanitorOn;
        public static CustomNumberOption MinerOn;
        public static CustomNumberOption UndertakerOn;

        public static CustomHeaderOption CrewmateModifiers;
        public static CustomNumberOption BaitOn;
        public static CustomNumberOption DiseasedOn;
        public static CustomNumberOption TorchOn;
        public static CustomNumberOption VoteCounterOn;

        public static CustomHeaderOption GlobalModifiers;
        public static CustomNumberOption BlindOn;
        public static CustomNumberOption ButtonBarryOn;
        public static CustomNumberOption FlashOn;
        public static CustomNumberOption GiantOn;
        public static CustomNumberOption LoversOn;
        public static CustomNumberOption SleuthOn;
        public static CustomNumberOption TiebreakerOn;

        public static CustomHeaderOption CustomGameSettings;
        public static CustomToggleOption ColourblindComms;
        public static CustomToggleOption ImpostorSeeRoles;
        public static CustomToggleOption DeadSeeRoles;
        public static CustomToggleOption DisableLevels;
        public static CustomToggleOption WhiteNameplates;
        public static CustomNumberOption VanillaGame;
        public static CustomNumberOption InitialCooldowns;
        public static CustomToggleOption ParallelMedScans;
        public static CustomStringOption SkipButtonDisable;

        public static CustomHeaderOption RoleCountSettings;
        public static CustomNumberOption MinNeutralNonKillingRoles;
        public static CustomNumberOption MaxNeutralNonKillingRoles;
        public static CustomToggleOption BegninNeutralHasTasks;
        public static CustomNumberOption MinNeutralKillingRoles;
        public static CustomNumberOption MaxNeutralKillingRoles;

        public static CustomHeaderOption TaskTrackingSettings;
        public static CustomToggleOption SeeTasksDuringRound;
        public static CustomToggleOption SeeTasksDuringMeeting;
        public static CustomToggleOption SeeTasksWhenDead;

        public static CustomHeaderOption Mayor;
        public static CustomNumberOption MayorVoteBank;
        public static CustomToggleOption MayorAnonymous;

        public static CustomHeaderOption Sheriff;
        public static CustomToggleOption SheriffKillOther;
        public static CustomToggleOption SheriffKillsJester;
        public static CustomToggleOption SheriffKillsCannibal;
        public static CustomToggleOption SheriffKillsGlitch;
        public static CustomToggleOption SheriffKillsExecutioner;
        public static CustomToggleOption SheriffKillsArsonist;
        public static CustomToggleOption SheriffKillsWerewolf;
        public static CustomToggleOption SheriffKillsPlaguebearer;
        public static CustomNumberOption SheriffKillCd;
        public static CustomToggleOption SheriffBodyReport;

        /*public static CustomHeaderOption Camouflager;
        public static CustomNumberOption CamouflagerCooldown;
        public static CustomNumberOption CamouflagerDuration;*/

        public static CustomHeaderOption Engineer;
        public static CustomToggleOption EngiHasVentCooldown;
        public static CustomNumberOption EngiVentCooldown;
        public static CustomNumberOption EngiVentDuration;
        public static CustomStringOption EngineerPer;
        public static CustomToggleOption EngiHasCooldown;
        public static CustomNumberOption EngiCooldown;
        public static CustomNumberOption EngiFixPerRound;
        public static CustomNumberOption EngiFixPerGame;

        public static CustomHeaderOption Investigator;
        public static CustomNumberOption FootprintSize;
        public static CustomNumberOption FootprintInterval;
        public static CustomNumberOption FootprintDuration;
        public static CustomToggleOption AnonymousFootPrint;
        public static CustomToggleOption VentFootprintVisible;

        public static CustomHeaderOption TimeLord;
        public static CustomToggleOption RewindRevive;
        public static CustomNumberOption RewindDuration;
        public static CustomNumberOption RewindCooldown;
        public static CustomNumberOption RewindMaxUses;
        public static CustomToggleOption TimeLordVitals;

        public static CustomHeaderOption Medic;
        public static CustomStringOption ShowShielded;
        public static CustomStringOption WhoGetsNotification;
        public static CustomToggleOption ShieldBreaks;
        public static CustomToggleOption MedicReportSwitch;
        public static CustomToggleOption MedicFlashReport;
        public static CustomNumberOption MedicReportNameDuration;
        public static CustomNumberOption MedicReportColorDuration;

        public static CustomHeaderOption Seer;
        public static CustomNumberOption SeerCooldown;
        public static CustomStringOption SeerInfo;
        public static CustomStringOption SeeReveal;
        public static CustomToggleOption NeutralRed;
        public static CustomNumberOption SeerCrewmateChance;
        public static CustomNumberOption SeerNeutralChance;
        public static CustomNumberOption SeerImpostorChance;

        public static CustomHeaderOption Swapper;
        public static CustomToggleOption SwapAfterVoting;
        public static CustomToggleOption SwapperButton;

        public static CustomHeaderOption Transporter;
        public static CustomNumberOption TransportCooldown;
        public static CustomNumberOption TransportMaxUses;
        public static CustomToggleOption TransporterVitals;

        public static CustomHeaderOption Jester;
        public static CustomToggleOption JesterButton;
        public static CustomToggleOption JesterVent;
        public static CustomToggleOption JesterSwitchVent;

        public static CustomHeaderOption Cannibal;
        public static CustomStringOption EatNeeded;
        public static CustomToggleOption CannibalCdOn;
        public static CustomNumberOption CannibalCd;

        public static CustomHeaderOption TheGlitch;
        public static CustomNumberOption MimicCooldownOption;
        public static CustomNumberOption MimicDurationOption;
        public static CustomNumberOption HackCooldownOption;
        public static CustomNumberOption HackDurationOption;
        public static CustomNumberOption GlitchKillCooldownOption;
        public static CustomStringOption GlitchHackDistanceOption;
        public static CustomToggleOption GlitchVent;
        public static CustomToggleOption GlitchMimicVent;

        public static CustomHeaderOption Morphling;
        public static CustomNumberOption MorphlingCooldown;
        public static CustomNumberOption MorphlingDuration;
        public static CustomToggleOption MorphlingVent;
        public static CustomToggleOption MorphlingMorphVent;

        public static CustomHeaderOption Executioner;
        public static CustomStringOption OnTargetDead;
        public static CustomToggleOption ExecutionerButton;

        public static CustomHeaderOption Phantom;
        public static CustomNumberOption PhantomTasksRemaining;

        public static CustomHeaderOption Snitch;
        public static CustomToggleOption SnitchOnLaunch;
        public static CustomToggleOption SnitchSeesNeutrals;
        public static CustomNumberOption SnitchTasksRemaining;
        public static CustomToggleOption SnitchSeesImpInMeeting;
        public static CustomToggleOption SnitchSeesTraitor;

        public static CustomHeaderOption Spy;
        public static CustomNumberOption SpyCd;
        public static CustomNumberOption SpyDuration;
        public static CustomToggleOption SpyAdmin;
        public static CustomToggleOption SpyVitals;

        public static CustomHeaderOption Altruist;
        public static CustomNumberOption ReviveDuration;
        public static CustomToggleOption AltruistTargetBody;

        public static CustomHeaderOption Miner;
        public static CustomNumberOption MineCooldown;
        public static CustomToggleOption MinerHiddenVent;

        public static CustomHeaderOption Swooper;
        public static CustomNumberOption SwoopCooldown;
        public static CustomNumberOption SwoopDuration;
        public static CustomToggleOption SwooperVent;

        public static CustomHeaderOption Arsonist;
        public static CustomNumberOption DouseCooldown;
        public static CustomNumberOption IgniteCooldown;
        public static CustomNumberOption MaxDoused;

        public static CustomHeaderOption Undertaker;
        public static CustomNumberOption DragCooldown;
        public static CustomToggleOption UndertakerVent;
        public static CustomToggleOption UndertakerVentWithBody;

        public static CustomHeaderOption Assassin;
        public static CustomNumberOption NumberOfAssassins;
        public static CustomToggleOption NeutralGuess;
        public static CustomNumberOption NumberOfCrewAssassins;
        public static CustomToggleOption AssassinProtection;
        public static CustomStringOption WhoSeesFailedFlash;
        public static CustomToggleOption AmneTurnAssassin;
        public static CustomToggleOption TraitorCanAssassin;
        public static CustomNumberOption AssassinKills;
        public static CustomToggleOption AssassinMultiKill;
        public static CustomToggleOption AssassinCrewmateGuess;
        public static CustomToggleOption AssassinSnitchViaCrewmate;
        public static CustomToggleOption AssassinGuessNeutralBenign;
        public static CustomToggleOption AssassinGuessNeutralEvil;
        public static CustomToggleOption AssassinGuessNeutralKilling;
        public static CustomToggleOption AssassinGuessModifiers;
        public static CustomToggleOption AssassinGuessLovers;
        public static CustomToggleOption AssassinateAfterVoting;

        public static CustomHeaderOption Eraser;
        public static CustomHeaderOption EraserSeperateHeader;
        public static CustomToggleOption EraserSeperateAbility;
        public static CustomToggleOption SecondAssassinAlwaysEraser;
        public static CustomToggleOption DontReplaceFirstAssassin;
        public static CustomNumberOption EraserChance;
        public static CustomToggleOption AmneTurnEraser;
        public static CustomToggleOption TraitorCanErase;
        public static CustomNumberOption EraserMod;
        public static CustomToggleOption EraserMultiErase;

        public static CustomHeaderOption Underdog;
        public static CustomNumberOption UnderdogKillBonus;
        public static CustomToggleOption UnderdogIncreasedKC;

        public static CustomHeaderOption Lycan;
        public static CustomNumberOption WolfCooldown;
        public static CustomNumberOption WolfDuration;

        public static CustomHeaderOption Haunter;
        public static CustomNumberOption HaunterTasksRemainingClicked;
        public static CustomNumberOption HaunterTasksRemainingAlert;
        public static CustomToggleOption HaunterRevealsNeutrals;
        public static CustomStringOption HaunterCanBeClickedBy;

        public static CustomHeaderOption Grenadier;
        public static CustomNumberOption GrenadeCooldown;
        public static CustomNumberOption GrenadeDuration;
        public static CustomToggleOption GrenadierIndicators;
        public static CustomToggleOption GrenadierVent;
        public static CustomNumberOption FlashRadius;

        public static CustomHeaderOption Veteran;
        public static CustomToggleOption KilledOnAlert;
        public static CustomNumberOption AlertCooldown;
        public static CustomNumberOption AlertDuration;
        public static CustomNumberOption MaxAlerts;

        public static CustomHeaderOption Tracker;
        public static CustomNumberOption UpdateInterval;
        public static CustomNumberOption TrackCooldown;
        public static CustomToggleOption ResetOnNewRound;
        public static CustomNumberOption MaxTracks;

        public static CustomHeaderOption Trapper;
        public static CustomNumberOption TrapCooldown;
        public static CustomToggleOption TrapsRemoveOnNewRound;
        public static CustomNumberOption MaxTraps;
        public static CustomNumberOption MinAmountOfTimeInTrap;
        public static CustomNumberOption TrapSize;
        public static CustomNumberOption MinAmountOfPlayersInTrap;

        public static CustomHeaderOption Poisoner;
        public static CustomNumberOption PoisonCooldown;
        public static CustomNumberOption PoisonDuration;
        public static CustomToggleOption PoisonerVent;

        public static CustomHeaderOption Traitor;
        public static CustomNumberOption LatestSpawn;
        public static CustomToggleOption NeutralKillingStopsTraitor;

        public static CustomHeaderOption Amnesiac;
        public static CustomToggleOption RememberArrows;
        public static CustomNumberOption RememberArrowDelay;

        public static CustomHeaderOption Medium;
        public static CustomNumberOption MediateCooldown;
        public static CustomToggleOption ShowMediatePlayer;
        public static CustomToggleOption ShowMediumToDead;
        public static CustomStringOption DeadRevealed;

        public static CustomHeaderOption Survivor;
        public static CustomNumberOption VestCd;
        public static CustomNumberOption VestDuration;
        public static CustomNumberOption VestKCReset;
        public static CustomNumberOption MaxVests;

        public static CustomHeaderOption GuardianAngel;
        public static CustomNumberOption ProtectCd;
        public static CustomNumberOption ProtectDuration;
        public static CustomNumberOption ProtectKCReset;
        public static CustomNumberOption MaxProtects;
        public static CustomStringOption ShowProtect;
        public static CustomStringOption GaOnTargetDeath;
        public static CustomToggleOption GATargetKnows;
        public static CustomToggleOption GAKnowsTargetRole;

        public static CustomHeaderOption Mystic;
        public static CustomNumberOption MysticArrowDuration;

        public static CustomHeaderOption Blackmailer;
        public static CustomNumberOption BlackmailCooldown;

        public static CustomHeaderOption Plaguebearer;
        public static CustomNumberOption InfectCooldown;
        public static CustomNumberOption PestKillCooldown;
        public static CustomToggleOption PlaguebearerEndGame;
        public static CustomToggleOption PestVent;

        public static CustomHeaderOption Werewolf;
        public static CustomNumberOption RampageCooldown;
        public static CustomNumberOption RampageDuration;
        public static CustomNumberOption RampageKillCooldown;
        public static CustomToggleOption WerewolfVent;

        public static CustomHeaderOption Detective;
        public static CustomNumberOption InitialExamineCooldown;
        public static CustomNumberOption ExamineCooldown;
        public static CustomNumberOption RecentKill;
        public static CustomToggleOption DetectiveReportOn;
        public static CustomNumberOption DetectiveRoleDuration;
        public static CustomNumberOption DetectiveFactionDuration;

        public static CustomHeaderOption Giant;
        public static CustomNumberOption GiantSlow;

        public static CustomHeaderOption Flash;
        public static CustomNumberOption FlashSpeed;

        public static CustomHeaderOption Diseased;
        public static CustomNumberOption DiseasedKillMultiplier;

        public static CustomHeaderOption Bait;
        public static CustomNumberOption BaitMinDelay;
        public static CustomNumberOption BaitMaxDelay;

        public static CustomHeaderOption Lovers;
        public static CustomToggleOption BothLoversDie;
        public static CustomNumberOption LovingImpPercent;
        public static CustomToggleOption NeutralLovers;

        public static Func<object, string> PercentFormat { get; } = value => $"{value:0}%";
        private static Func<object, string> CooldownFormat { get; } = value => $"{value:0.0#}s";
        private static Func<object, string> MultiplierFormat { get; } = value => $"{value:0.0#}x";


        public static void GenerateAll()
        {
            var num = 0;

            Patches.ExportButton = new Export(num++);   
            Patches.ImportButton = new Import(num++);
            
            

            MaxPlayers = new CustomNumberOption(num++, MultiMenu.main, "Max Players", 12, 4, 15, 1);

            BetterPolus = new CustomHeaderOption(num++, MultiMenu.main, "Better Polus");
            VentImprovements = new CustomToggleOption(num++, MultiMenu.main, "Better Polus Vent Layout", false);
            VitalsLab = new CustomToggleOption(num++, MultiMenu.main, "Vitals Moved To Lab", false);
            ColdTempDeathValley = new CustomToggleOption(num++, MultiMenu.main, "Cold Temp Moved To Death Valley", false);
            WifiChartCourseSwap = new CustomToggleOption(num++, MultiMenu.main, "Reboot Wifi And Chart Course Swapped", false);
            
            RoleCountSettings =
                new CustomHeaderOption(num++, MultiMenu.main, "Role Count Settings");
            MinNeutralNonKillingRoles =
                new CustomNumberOption(true,num++, MultiMenu.main, "Min Neutral Non-Killing Roles", 1f, 0f, 6f, 1f);
            MaxNeutralNonKillingRoles =
                new CustomNumberOption(true,num++, MultiMenu.main, "Max Neutral Non-Killing Roles", 2f, 0f, 6f, 1f);
            MinNeutralKillingRoles =
                new CustomNumberOption(true,num++, MultiMenu.main, "Min Neutral Killing Roles", 1f, 0f, 4f, 1f);
            MaxNeutralKillingRoles =
                new CustomNumberOption(true,num++, MultiMenu.main, "Max Neutral Killing Roles", 1f, 0f, 4f, 1f);
            BegninNeutralHasTasks =
                new CustomToggleOption(num++, MultiMenu.main, "Begnin Neutrals Roles Can Use Tasks", false);

            CrewInvestigativeRoles = new CustomHeaderOption(num++, MultiMenu.crewmate, "Crewmate Investigative Roles");
            DetectiveOn = new CustomNumberOption(true, num++, MultiMenu.crewmate,  "<color=#4D4DFFFF>Detective</color>", 90f, 0f, 100f, 10f,
                PercentFormat);
            HaunterOn = new CustomNumberOption(true, num++, MultiMenu.crewmate, "<color=#D3D3D3FF>Haunter</color>", 90f, 0f, 100f, 10f,
                PercentFormat);
            InvestigatorOn = new CustomNumberOption(true, num++, MultiMenu.crewmate, "<color=#00B3B3FF>Investigator</color>", 60f, 0f, 100f, 10f,
                PercentFormat);
            MysticOn = new CustomNumberOption(true, num++, MultiMenu.crewmate, "<color=#4D99E6FF>Mystic</color>", 60f, 0f, 100f, 10f,
                PercentFormat);
            SeerOn = new CustomNumberOption(true, num++, MultiMenu.crewmate, "<color=#FFCC80FF>Seer</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            SnitchOn = new CustomNumberOption(true, num++, MultiMenu.crewmate,"<color=#D4AF37FF>Snitch</color>", 80f, 0f, 100f, 10f,
                PercentFormat);
            SpyOn = new CustomNumberOption(true, num++, MultiMenu.crewmate,"<color=#CCA3CCFF>Spy</color>", 50f, 0f, 100f, 10f,
                PercentFormat);
            TrackerOn = new CustomNumberOption(true, num++, MultiMenu.crewmate,"<color=#009900FF>Tracker</color>", 50f, 0f, 100f, 10f,
                PercentFormat);
            TrapperOn = new CustomNumberOption(true, num++, MultiMenu.crewmate,"<color=#A7D1B3FF>Trapper</color>", 50f, 0f, 100f, 10f,
                PercentFormat);

            CrewKillingRoles = new CustomHeaderOption(num++, MultiMenu.crewmate,"Crewmate Killing Roles");
            SheriffOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#FFFF00FF>Sheriff</color>", 100f, 0f, 100f, 10f,
                PercentFormat);
            VeteranOn = new CustomNumberOption(true, num++, MultiMenu.crewmate,"<color=#998040FF>Veteran</color>", 100f, 0f, 100f, 10f,
                PercentFormat);

            CrewProtectiveRoles = new CustomHeaderOption(num++,MultiMenu.crewmate, "Crewmate Protective Roles");
            AltruistOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#660000FF>Altruist</color>", 40f, 0f, 100f, 10f,
                PercentFormat);
            MedicOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#006600FF>Medic</color>", 70f, 0f, 100f, 10f,
                PercentFormat);

            CrewSupportRoles = new CustomHeaderOption(num++,MultiMenu.crewmate, "Crewmate Support Roles");
            EngineerOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#FFA60AFF>Engineer</color>", 40f, 0f, 100f, 10f,
                PercentFormat);
            MayorOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#704FA8FF>Mayor</color>", 40f, 0f, 100f, 10f,
                PercentFormat);
            MediumOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#A680FFFF>Medium</color>", 70f, 0f, 100f, 10f,
                PercentFormat);
            SwapperOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#66E666FF>Swapper</color>", 30f, 0f, 100f, 10f,
                PercentFormat);
            TimeLordOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#0000FFFF>Time Lord</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            TransporterOn = new CustomNumberOption(true, num++,MultiMenu.crewmate, "<color=#00EEFFFF>Transporter</color>", 90f, 0f, 100f, 10f,
                PercentFormat);


            NeutralBenignRoles = new CustomHeaderOption(num++, MultiMenu.neutral, "Neutral Benign Roles");
            AmnesiacOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#80B2FFFF>Amnesiac</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            GuardianAngelOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#B3FFFFFF>Guardian Angel</color>", 50f, 0f, 100f, 10f,
                PercentFormat);
            SurvivorOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#FFE64DFF>Survivor</color>", 30f, 0f, 100f, 10f,
                PercentFormat);

            NeutralEvilRoles = new CustomHeaderOption(num++,MultiMenu.neutral, "Neutral Evil Roles");
            ExecutionerOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#8C4005FF>Executioner</color>", 90f, 0f, 100f, 10f,
                PercentFormat);
            JesterOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#FFBFCCFF>Jester</color>", 90f, 0f, 100f, 10f,
                PercentFormat);
            CannibalOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#1E300BFF>Cannibal</color>", 40f, 0f, 100f, 10f,
                PercentFormat);
            PhantomOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#662962FF>Phantom</color>", 90f, 0f, 100f, 10f,
                PercentFormat);

            NeutralKillingRoles = new CustomHeaderOption(num++,MultiMenu.neutral, "Neutral Killing Roles");
            ArsonistOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#FF4D00FF>Arsonist</color>", 40f, 0f, 100f, 10f,
                PercentFormat);
            PlaguebearerOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#E6FFB3FF>Plaguebearer</color>", 100f, 0f, 100f, 10f,
                PercentFormat);
            GlitchOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#00FF00FF>The Glitch</color>", 100f, 0f, 100f, 10f,
                PercentFormat);
            WerewolfOn = new CustomNumberOption(true, num++,MultiMenu.neutral, "<color=#A86629FF>Werewolf</color>", 100f, 0f, 100f, 10f,
                PercentFormat);

            ImpostorConcealingRoles = new CustomHeaderOption(num++,MultiMenu.imposter, "Impostor Concealing Roles");
            //CamouflagerOn = new CustomNumberOption(true, num++, MultiMenu.imposter, "<color=#378AC0FF>Camouflager</color>", 0f, 0f, 100f, 10f, PercentFormat);
            GrenadierOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Grenadier</color>", 60f, 0f, 100f, 10f,
                PercentFormat);
            MorphlingOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Morphling</color>", 40f, 0f, 100f, 10f,
                PercentFormat);
            SwooperOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Swooper</color>", 40f, 0f, 100f, 10f,
                PercentFormat);

            ImpostorKillingRoles = new CustomHeaderOption(num++,MultiMenu.imposter, "Impostor Killing Roles");
            PoisonerOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Poisoner</color>", 60f, 0f, 100f, 10f,
                PercentFormat);
            TraitorOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Traitor</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            UnderdogOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Underdog</color>", 30f, 0f, 100f, 10f,
                PercentFormat);
            LycanOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Lycan</color>", 50f, 0f, 100f, 10f,
                PercentFormat);

            ImpostorSupportRoles = new CustomHeaderOption(num++,MultiMenu.imposter, "Impostor Support Roles");
            BlackmailerOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Blackmailer</color>", 50f, 0f, 100f, 10f,
                PercentFormat);
            JanitorOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Janitor</color>", 50f, 0f, 100f, 10f,
                PercentFormat);
            MinerOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Miner</color>", 50f, 0f, 100f, 10f,
                PercentFormat);
            UndertakerOn = new CustomNumberOption(true, num++,MultiMenu.imposter, "<color=#FF0000FF>Undertaker</color>", 50f, 0f, 100f, 10f,
                PercentFormat);

            CrewmateModifiers = new CustomHeaderOption(num++, MultiMenu.modifiers, "Crewmate Modifiers");
            BaitOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#00B3B3FF>Bait</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            DiseasedOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#808080FF>Diseased</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            TorchOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#FFFF99FF>Torch</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            VoteCounterOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#3769FEFF>Vote Counter</color>", 20f, 0f, 100f, 10f,
                PercentFormat);

            GlobalModifiers = new CustomHeaderOption(num++,MultiMenu.modifiers, "Global Modifiers");
            BlindOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#AAAAAAFF>Blind</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            ButtonBarryOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#E600FFFF>Button Barry</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            FlashOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#FF8080FF>Flash</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            GiantOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#FFB34DFF>Giant</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            LoversOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#FF66CCFF>Lovers</color>", 40f, 0f, 100f, 10f,
                PercentFormat);
            SleuthOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#803333FF>Sleuth</color>", 20f, 0f, 100f, 10f,
                PercentFormat);
            TiebreakerOn = new CustomNumberOption(true, num++,MultiMenu.modifiers, "<color=#99E699FF>Tiebreaker</color>", 20f, 0f, 100f, 10f,
                PercentFormat);

            CustomGameSettings =
                new CustomHeaderOption(num++,MultiMenu.main, "Custom Game Settings");
            ColourblindComms = new CustomToggleOption(num++,MultiMenu.main, "Camouflaged Comms", true);
            ImpostorSeeRoles = new CustomToggleOption(num++,MultiMenu.main, "Impostors Can See The Roles Of Their Team", true);
            DeadSeeRoles =
                new CustomToggleOption(num++,MultiMenu.main, "Dead Can See Everyone's Roles/Votes", true);
            VanillaGame = new CustomNumberOption(num++,MultiMenu.main, "Probability Of A Completely Vanilla Game", 0f, 0f, 100f, 5f,
                PercentFormat);
            InitialCooldowns =
                new CustomNumberOption(num++,MultiMenu.main, "Game Start Cooldowns", 20, 5, 40, 2.5f, CooldownFormat);
            ParallelMedScans = new CustomToggleOption(num++,MultiMenu.main, "Parallel Medbay Scans", true);
            SkipButtonDisable = new CustomStringOption(num++,MultiMenu.main, "Disable Meeting Skip Button", new[] { "No", "Emergency", "Always" });
            DisableLevels = new CustomToggleOption(num++, MultiMenu.main,"Disable Level Icons", false);
            WhiteNameplates = new CustomToggleOption(num++,MultiMenu.main, "Disable Player Nameplates", false);
            TaskTrackingSettings =
                new CustomHeaderOption(num++,MultiMenu.main, "Task Tracking Settings");
            SeeTasksDuringRound = new CustomToggleOption(num++,MultiMenu.main, "See Tasks During Round", true);
            SeeTasksDuringMeeting = new CustomToggleOption(num++,MultiMenu.main, "See Tasks During Meetings", true);
            SeeTasksWhenDead = new CustomToggleOption(num++,MultiMenu.main, "See Tasks When Dead", true);

            Assassin = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Assassin Ability</color>");
            NumberOfAssassins = new CustomNumberOption(num++,MultiMenu.imposter, "Number Of Assassins", 6, 0, 6, 1);
            NeutralGuess = new CustomToggleOption(num++,MultiMenu.imposter, "Neutral Killing Roles Can Guess", true);
            NumberOfCrewAssassins = new CustomNumberOption(num++,MultiMenu.imposter, "Number Of <color=#8BFDFDFF>Crew</color> <color=#073763FF>Assassins</color>", 1, 0, 14, 1);
            AssassinProtection = new CustomToggleOption(num++,MultiMenu.imposter, "Assassin Protection From Missing Guess", true);
            WhoSeesFailedFlash = new CustomStringOption(num++,MultiMenu.imposter, "Who Sees Assassin Failed Notification", new[] { "Everyone","Impostors","Target + Impostors", "Target + Assassin","Assassin"});
            AmneTurnAssassin = new CustomToggleOption(num++,MultiMenu.imposter, "Amnesiac Turned Impostor Gets Ability", true);
            TraitorCanAssassin = new CustomToggleOption(num++,MultiMenu.imposter, "Traitor Gets Ability", true);
            AssassinKills = new CustomNumberOption(num++,MultiMenu.imposter, "Number Of Assassin Kills", 15, 1, 15, 1);
            AssassinMultiKill = new CustomToggleOption(num++,MultiMenu.imposter, "Assassin Can Kill More Than Once Per Meeting", true);
            AssassinCrewmateGuess = new CustomToggleOption(num++,MultiMenu.imposter, "Assassin Can Guess \"Crewmate\"", true);
            AssassinSnitchViaCrewmate = new CustomToggleOption(num++,MultiMenu.imposter, "Assassinate Snitch Via \"Crewmate\" Guess", true);
            AssassinGuessNeutralBenign = new CustomToggleOption(num++,MultiMenu.imposter, "Assassin Can Guess Neutral Benign Roles", true);
            AssassinGuessNeutralEvil = new CustomToggleOption(num++,MultiMenu.imposter, "Assassin Can Guess Neutral Evil Roles", true);
            AssassinGuessNeutralKilling = new CustomToggleOption(num++,MultiMenu.imposter, "Assassin Can Guess Neutral Killing Roles", true);
            AssassinGuessModifiers = new CustomToggleOption(num++,MultiMenu.imposter, "Assassin Can Guess Crewmate Modifiers", true);
            AssassinGuessLovers = new CustomToggleOption(num++,MultiMenu.imposter, "Assassin Can Guess Lovers", true);
            AssassinateAfterVoting = new CustomToggleOption(num++, MultiMenu.imposter,"Assassin Can Guess After Voting", true);

            Eraser = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Eraser Ability</color>");
            EraserSeperateAbility = new CustomToggleOption(num++,MultiMenu.imposter, "Eraser seperate ability from Assasin", false);
            EraserSeperateHeader = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#990000FF>If Eraser Is Seperate:</color>");
            EraserChance = new CustomNumberOption(num++,MultiMenu.imposter, "Chance of being Eraser instead of Assasin", 50f, 0f, 100f, 10f);
            SecondAssassinAlwaysEraser = new CustomToggleOption(num++,MultiMenu.imposter, "Second Assassin is always Eraser", true);
            DontReplaceFirstAssassin = new CustomToggleOption(num++,MultiMenu.imposter, "Don't replace first Assassin", true);
            AmneTurnEraser = new CustomToggleOption(num++,MultiMenu.imposter, "Amnesiac Turned Impostor Gets Ability", false);
            TraitorCanErase = new CustomToggleOption(num++,MultiMenu.imposter, "Traitor Gets Ability", false);
            EraserMod = new CustomNumberOption(num++,MultiMenu.imposter, "Number Of Erases", 1, 1, 15, 1);
            EraserMultiErase = new CustomToggleOption(num++,MultiMenu.imposter, "Eraser Can Erase More Than Once Per Meeting", false);

            Detective =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#4D4DFFFF>Detective</color>");
            InitialExamineCooldown =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Initial Examine Cooldown", 25f, 2.5f, 90f, 2.5f, CooldownFormat);
            ExamineCooldown =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Examine Cooldown", 20f, 1f, 40f, 1f, CooldownFormat);
            RecentKill =
                new CustomNumberOption(num++,MultiMenu.crewmate, "How Long Players Stay Bloody For", 10f, 2.5f, 90f, 2.5f, CooldownFormat);
            DetectiveReportOn = new CustomToggleOption(num++,MultiMenu.crewmate, "Show Detective Reports", true);
            DetectiveRoleDuration =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Time Where Detective Will Have Role", 7.5f, 0, 90, 0.5f,
                    CooldownFormat);
            DetectiveFactionDuration =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Time Where Detective Will Have Faction", 20, 0, 120, 0.5f,
                    CooldownFormat);

            Haunter =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#d3d3d3FF>Haunter</color>");
            HaunterTasksRemainingClicked =
                 new CustomNumberOption(num++,MultiMenu.crewmate, "Tasks Remaining When Haunter Can Be Clicked", 4, 0, 10, 1);
            HaunterTasksRemainingAlert =
                 new CustomNumberOption(num++,MultiMenu.crewmate, "Tasks Remaining When Alert Is Sent", 1, 0, 10, 1);
            HaunterRevealsNeutrals = new CustomToggleOption(num++,MultiMenu.crewmate, "Haunter Reveals Neutral Roles", false);
            HaunterCanBeClickedBy = new CustomStringOption(num++,MultiMenu.crewmate, "Who Can Click Haunter", new[] {"Imps Only", "All", "Non-Crew"});

            Investigator =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#00B3B3FF>Investigator</color>");
            FootprintSize = new CustomNumberOption(num++,MultiMenu.crewmate, "Footprint Size", 3f, 1f, 10f, 1f);
            FootprintInterval =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Footprint Interval", 0.5f, 0.05f, 5f, 0.05f, CooldownFormat);
            FootprintDuration = new CustomNumberOption(num++,MultiMenu.crewmate, "Footprint Duration", 3f, 1f, 50f, 0.5f, CooldownFormat);
            AnonymousFootPrint = new CustomToggleOption(num++,MultiMenu.crewmate, "Anonymous Footprint", false);
            VentFootprintVisible = new CustomToggleOption(num++,MultiMenu.crewmate, "Footprint Vent Visible", false);

            Mystic =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#4D99E6FF>Mystic</color>");
            MysticArrowDuration =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Dead Body Arrow Duration", 0.3f, 0f, 25f, 0.05f, CooldownFormat);

            Seer =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#FFCC80FF>Seer</color>");
            SeerCooldown =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Seer Cooldown", 45f, 10f, 100f, 2.5f, CooldownFormat);
            SeerInfo =
                new CustomStringOption(num++,MultiMenu.crewmate, "Info that Seer sees", new[] {"Team", "Role"});
            SeeReveal =
                new CustomStringOption(num++,MultiMenu.crewmate, "Who Sees That They Are Revealed",
                    new[] {"Imps+Neut", "Crew", "All", "Nobody"});
            NeutralRed =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Neutrals show up as Impostors", false);
            SeerCrewmateChance = new CustomNumberOption(num++,MultiMenu.crewmate,
                "Chance to successfully reveal a Crewmate", 80f, 0f, 100f, 10f, PercentFormat);
            SeerNeutralChance = new CustomNumberOption(num++,MultiMenu.crewmate,
                "Chance to successfully reveal a Neutral role", 80f, 0f, 100f, 10f, PercentFormat);
            SeerImpostorChance = new CustomNumberOption(num++,MultiMenu.crewmate,
                "Chance to successfully reveal an Impostor", 80f, 0f, 100f, 10f, PercentFormat);

            Snitch = new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#D4AF37FF>Snitch</color>");
            SnitchOnLaunch =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Snitch Knows Who They Are On Game Start", false);
            SnitchSeesNeutrals = new CustomToggleOption(num++,MultiMenu.crewmate, "Snitch Sees Neutral Roles", false);
            SnitchTasksRemaining =
                 new CustomNumberOption(num++,MultiMenu.crewmate, "Tasks Remaining When Revealed", 1, 0, 10, 1);
            SnitchSeesTraitor = new CustomToggleOption(num++,MultiMenu.crewmate, "Snitch Sees Traitor", true);
            SnitchSeesImpInMeeting = new CustomToggleOption(num++,MultiMenu.crewmate, "Snitch Sees Impostors In Meetings", true);

            Spy = new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#CCA3CCFF>Spy</color>");
            SpyCd =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Spy Cooldown", 30f, 2.5f, 90f, 2.5f, CooldownFormat);
            SpyDuration =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Spy Duration", 15, 1, 90, 1, CooldownFormat);
            SpyAdmin =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Spy See Color On Admin Table", true);
            SpyVitals =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Spy See Kill Timing On Vitals", true);

            Tracker =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#009900FF>Tracker</color>");
            UpdateInterval =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Arrow Update Interval", 5f, 0.5f, 50f, 0.5f, CooldownFormat);
            TrackCooldown =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Track Cooldown", 27.5f, 2.5f, 90f, 2.5f, CooldownFormat);
            ResetOnNewRound = new CustomToggleOption(num++,MultiMenu.crewmate, "Tracker Arrows Reset After Each Round", false);
            MaxTracks = new CustomNumberOption(num++,MultiMenu.crewmate, "Maximum Number Of Tracks Per Round", 3, 0, 20, 1);

            Trapper =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#A7D1B3FF>Trapper</color>");
            MinAmountOfTimeInTrap =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Min Amount Of Time In Trap To Register", 1.5f, 0f, 50f, 0.5f, CooldownFormat);
            TrapCooldown =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Trap Cooldown", 15f, 2.5f, 90f, 2.5f, CooldownFormat);
            TrapsRemoveOnNewRound =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Traps Removed After Each Round", true);
            MaxTraps =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Maximum Number Of Traps Per Game", 5, 0, 90, 1);
            TrapSize =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Trap Size", 1.5f, 0.5f, 10f, 0.5f, MultiplierFormat);
            MinAmountOfPlayersInTrap =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Minimum Number Of Roles Required To Trigger Trap", 3, 1, 10, 1);

            Sheriff =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#FFFF00FF>Sheriff</color>");
            SheriffKillOther =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Miskill Kills Crewmate", false);
            SheriffKillsJester =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Kills Jester", true);
            SheriffKillsCannibal =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Kills Cannibal", true);
            SheriffKillsGlitch =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Kills The Glitch", true);
            SheriffKillsExecutioner =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Kills Executioner", true);
            SheriffKillsArsonist =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Kills Arsonist", true);
            SheriffKillsWerewolf =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Kills Werewolf", true);
            SheriffKillsPlaguebearer =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Kills Plaguebearer", true);
            SheriffKillCd =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Sheriff Kill Cooldown", 25f, 2.5f, 90, 2.5f, CooldownFormat);
            SheriffBodyReport = new CustomToggleOption(num++,MultiMenu.crewmate, "Sheriff Can Report Who They've Killed",true);

            Veteran =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#998040FF>Veteran</color>");
            KilledOnAlert =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Can Be Killed On Alert", false);
            AlertCooldown =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Alert Cooldown", 25, 5, 90, 2.5f, CooldownFormat);
             AlertDuration =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Alert Duration", 10, 1, 90, 1f, CooldownFormat);
            MaxAlerts = new CustomNumberOption(num++,MultiMenu.crewmate, "Maximum Number Of Alerts", 5, 1, 90, 1);

            Altruist = new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#660000FF>Altruist</color>");
            ReviveDuration =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Altruist Revive Duration", 4, 1, 90, 1f, CooldownFormat);
            AltruistTargetBody =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Target's Body Disappears On Beginning Of Revive", false);

            Medic =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#006600FF>Medic</color>");
            ShowShielded =
                new CustomStringOption(num++,MultiMenu.crewmate, "Show Shielded Player",
                    new[] { "Medic","Self", "Self+Medic", "Everyone" });
            WhoGetsNotification =
                new CustomStringOption(num++,MultiMenu.crewmate, "Who Gets Murder Attempt Indicator",
                    new[] { "Medic", "Shielded", "Everyone", "Nobody" });
            ShieldBreaks = new CustomToggleOption(num++,MultiMenu.crewmate, "Shield Breaks On Murder Attempt", true);
            MedicReportSwitch = new CustomToggleOption(num++,MultiMenu.crewmate, "Show Medic Reports", true);
            MedicFlashReport = new CustomToggleOption(num++,MultiMenu.crewmate, "Medic Report Can't Have Name If Flashed By Grenadier", true);
            MedicReportNameDuration =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Time Where Medic Will Have Name", 3, 0, 90, 0.5f,
                    CooldownFormat);
            MedicReportColorDuration =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Time Where Medic Will Have Color Type", 15, 0, 150, 0.5f,
                    CooldownFormat);

            Engineer =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#FFA60AFF>Engineer</color>");
            EngiHasVentCooldown =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Engineer Has A Vent Cooldown & Duration", true);
            EngiVentCooldown = 
                new CustomNumberOption(num++,MultiMenu.crewmate, "Engineer Vent Cooldown", 15f, 5f, 100f, 2.5f);
            EngiVentDuration = 
                new CustomNumberOption(num++,MultiMenu.crewmate, "Engineer Vent Duration", 15f, 10f, 30f, 2.5f);
            EngineerPer =
                new CustomStringOption(num++,MultiMenu.crewmate, "Engineer Fix Per", new[] { "Custom", "Round", "Game" });
            EngiHasCooldown =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Engineer Has A Fix Cooldown", false);
            EngiCooldown = 
                new CustomNumberOption(num++,MultiMenu.crewmate, "Engineer Fix Cooldown", 32.5f, 32.5f, 100f, 2.5f);
            EngiFixPerRound = 
                new CustomNumberOption(num++,MultiMenu.crewmate, "Engineer Max Fix Per Round", 1, 0, 10, 1);
            EngiFixPerGame =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Engineer Max Fix Per Game", 2, 0, 100, 1);


            Mayor =
                new CustomHeaderOption(num++, MultiMenu.crewmate,"<color=#704FA8FF>Mayor</color>");
            MayorVoteBank =
                new CustomNumberOption(num++, MultiMenu.crewmate,"Initial Mayor Vote Bank", 4, 0, 15, 1);
            MayorAnonymous =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Mayor Votes Show Anonymous", true);

            Medium =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#A680FFFF>Medium</color>");
            MediateCooldown =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Mediate Cooldown", 18f, 1f, 90f, 1f, CooldownFormat);
            ShowMediatePlayer =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Reveal Appearance Of Mediate Target", true);
            ShowMediumToDead =
                new CustomToggleOption(num++, MultiMenu.crewmate,"Reveal The Medium To The Mediate Target", true);
            DeadRevealed =
                new CustomStringOption(num++, MultiMenu.crewmate,"Who Is Revealed With Mediate", new[] { "Oldest Dead", "Newest Dead", "All Dead" });

            Swapper =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#66E666FF>Swapper</color>");
            SwapAfterVoting =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Swapper Can Swap After Voting", true);
            SwapperButton =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Swapper Can Button", true);

            TimeLord =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#0000FFFF>Time Lord</color>");
            RewindRevive = new CustomToggleOption(num++,MultiMenu.crewmate, "Revive During Rewind", true);
            RewindDuration = new CustomNumberOption(num++,MultiMenu.crewmate, "Rewind Duration", 2f, 0.5f, 50f, 0.5f, CooldownFormat);
            RewindCooldown = new CustomNumberOption(num++,MultiMenu.crewmate, "Rewind Cooldown", 35f, 2.5f, 90f, 2.5f, CooldownFormat);
            RewindMaxUses =
                 new CustomNumberOption(num++,MultiMenu.crewmate, "Maximum Number Of Rewinds", 3, 0, 50, 1);
            TimeLordVitals =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Time Lord Can Use Vitals", false);

            Transporter =
                new CustomHeaderOption(num++,MultiMenu.crewmate, "<color=#00EEFFFF>Transporter</color>");
            TransportCooldown =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Transport Cooldown", 27.5f, 2.5f, 90f, 2.5f, CooldownFormat);
            TransportMaxUses =
                new CustomNumberOption(num++,MultiMenu.crewmate, "Maximum Number Of Transports", 5, 0, 50, 1);
            TransporterVitals =
                new CustomToggleOption(num++,MultiMenu.crewmate, "Transporter Can Use Vitals", false);

            Amnesiac = new CustomHeaderOption(num++, MultiMenu.neutral,"<color=#80B2FFFF>Amnesiac</color>");
            RememberArrows =
                new CustomToggleOption(num++,MultiMenu.neutral, "Amnesiac Gets Arrows Pointing To Dead Bodies", true);
            RememberArrowDelay =
                new CustomNumberOption(num++,MultiMenu.neutral, "Time After Death Arrow Appears", 7f, 0f, 90f, 1f, CooldownFormat);

            GuardianAngel =
                new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#B3FFFFFF>Guardian Angel</color>");
            ProtectCd =
                new CustomNumberOption(num++,MultiMenu.neutral, "Protect Cooldown", 25, 2.5f, 90, 2.5f, CooldownFormat);
            ProtectDuration =
                new CustomNumberOption(num++,MultiMenu.neutral, "Protect Duration", 10, 1, 50, 1f, CooldownFormat);
            ProtectKCReset =
                new CustomNumberOption(num++, MultiMenu.neutral,"Kill Cooldown Reset When Protected", 27.5f, 0f, 90f, 0.5f, CooldownFormat);
            MaxProtects =
                new CustomNumberOption(num++,MultiMenu.neutral, "Maximum Number Of Protects", 5, 0, 50, 1);
            ShowProtect =
                new CustomStringOption(num++,MultiMenu.neutral, "Show Protected Player",
                    new[] {"Guardian Angel", "Self+GA", "Self", "Everyone"});
            GaOnTargetDeath = new CustomStringOption(num++,MultiMenu.neutral, "GA Becomes On Target Dead",
                new[] { "Amnesiac", "Survivor", "Crew", "Jester" });
            GATargetKnows =
                new CustomToggleOption(num++,MultiMenu.neutral, "Target Knows GA Exists", false);
            GAKnowsTargetRole =
                new CustomToggleOption(num++,MultiMenu.neutral, "GA Knows Targets Role", true);

            Survivor =
                new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#FFE64DFF>Survivor</color>");
            VestCd =
                new CustomNumberOption(num++,MultiMenu.neutral, "Vest Cooldown", 25, 2.5f, 90, 2.5f, CooldownFormat);
            VestDuration =
                new CustomNumberOption(num++,MultiMenu.neutral, "Vest Duration", 10, 1, 90, 1f, CooldownFormat);
            VestKCReset =
                new CustomNumberOption(num++,MultiMenu.neutral, "Kill Cooldown Reset On Attack", 27.5f, 0f, 90f, 0.5f, CooldownFormat);
            MaxVests =
                new CustomNumberOption(num++,MultiMenu.neutral, "Maximum Number Of Vests", 7, 0, 90, 1);

            Arsonist = new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#FF4D00FF>Arsonist</color>");
            DouseCooldown = new CustomNumberOption(num++, MultiMenu.neutral, "Douse Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            IgniteCooldown = new CustomNumberOption(num++, MultiMenu.neutral, "Ignite Cooldown", 25f, 0f, 60f, 2.5f, CooldownFormat);
            MaxDoused = new CustomNumberOption(num++, MultiMenu.neutral, "Number Of Alive Players That Can Be Doused", 5, 1, 15, 1);

            Executioner =
                new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#8C4005FF>Executioner</color>");
            OnTargetDead = new CustomStringOption(num++,MultiMenu.neutral, "Executioner Becomes On Target Dead",
                new[] { "Jester", "Crew", "Amnesiac", "Survivor" });
            ExecutionerButton =
                new CustomToggleOption(num++,MultiMenu.neutral, "Executioner Can Button", true);

            Jester =
                new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#FFBFCCFF>Jester</color>");
            JesterButton =
                new CustomToggleOption(num++,MultiMenu.neutral, "Jester Can Button", true);
            JesterVent =
                new CustomToggleOption(num++,MultiMenu.neutral, "Jester Can Hide In Vents", true);
            JesterSwitchVent =
                new CustomToggleOption(num++,MultiMenu.neutral, "Jester Can Move In Vents", true);

            Cannibal =
                new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#1E300BFF>Cannibal</color>");
            EatNeeded =
                new CustomStringOption(num++,MultiMenu.neutral, "Number Of Bodies The Cannibal Must Eat", StrToNbr("Players/4", 6));
            CannibalCdOn =
                new CustomToggleOption(num++,MultiMenu.neutral, "Cannibal Have A Cooldown", true);
            CannibalCd =
                new CustomNumberOption(num++,MultiMenu.neutral, "Cannibal Cooldown", 15, 10, 60, 2.5f, CooldownFormat);

            Phantom =
                new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#662962FF>Phantom</color>");
            PhantomTasksRemaining =
                 new CustomNumberOption(num++,MultiMenu.neutral, "Tasks Remaining When Phantom Can Be Clicked", 4, 0, 15, 1);

            Plaguebearer = new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#E6FFB3FF>Plaguebearer</color>");
            InfectCooldown =
                new CustomNumberOption(num++,MultiMenu.neutral, "Infect Cooldown", 25f, 2.5f, 90f, 2.5f, CooldownFormat);
            PestKillCooldown =
                new CustomNumberOption(num++,MultiMenu.neutral, "Pestilence Kill Cooldown", 27.5f, 2.5f, 90f, 2.5f, CooldownFormat);
            PlaguebearerEndGame =
                new CustomToggleOption(num++,MultiMenu.neutral, "Game Keeps Going So Long As Plaguebearer Non Pestilence Is Alive", false);
            PestVent =
                new CustomToggleOption(num++,MultiMenu.neutral, "Pestilence Can Vent", true);

            TheGlitch =
                new CustomHeaderOption(num++, MultiMenu.neutral,"<color=#00FF00FF>The Glitch</color>");
            MimicCooldownOption = new CustomNumberOption(num++,MultiMenu.neutral, "Mimic Cooldown", 25f, 2.5f, 240f, 2.5f, CooldownFormat);
            MimicDurationOption = new CustomNumberOption(num++,MultiMenu.neutral, "Mimic Duration", 10f, 1f, 90f, 1f, CooldownFormat);
            HackCooldownOption = new CustomNumberOption(num++,MultiMenu.neutral, "Hack Cooldown", 25f, 2.5f, 240f, 2.5f, CooldownFormat);
            HackDurationOption = new CustomNumberOption(num++,MultiMenu.neutral, "Hack Duration", 10f, 1f, 90f, 1f, CooldownFormat);
            GlitchKillCooldownOption =
                new CustomNumberOption(num++,MultiMenu.neutral, "Glitch Kill Cooldown", 27.5f, 2.5f, 240f, 2.5f, CooldownFormat);
            GlitchHackDistanceOption =
                new CustomStringOption(num++,MultiMenu.neutral, "Glitch Hack Distance", new[] { "Short", "Normal", "Long" });
            GlitchVent =
                new CustomToggleOption(num++,MultiMenu.neutral, "Glitch Can Vent", true);
            GlitchMimicVent =
                new CustomToggleOption(num++,MultiMenu.neutral, "Glitch Can Vent When Using Mimic", false);

            Werewolf = new CustomHeaderOption(num++,MultiMenu.neutral, "<color=#A86629FF>Werewolf</color>");
            RampageCooldown =
                new CustomNumberOption(num++,MultiMenu.neutral, "Rampage Cooldown", 27.5f, 2.5f, 90f, 2.5f, CooldownFormat);
            RampageDuration =
                new CustomNumberOption(num++,MultiMenu.neutral, "Rampage Duration", 25f, 2.5f, 90f, 2.5f, CooldownFormat);
            RampageKillCooldown =
                new CustomNumberOption(num++,MultiMenu.neutral, "Rampage Kill Cooldown", 10f, 0.5f, 120f, 0.5f, CooldownFormat);
            WerewolfVent =
                new CustomToggleOption(num++,MultiMenu.neutral, "Werewolf Can Vent When Rampaged", true);

           /* Camouflager = new CustomHeaderOption(num++, MultiMenu.imposter, "<color=#378AC0FF>Camouflager</color>");
            CamouflagerCooldown = new CustomNumberOption(num++, MultiMenu.imposter, "Camouflage Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
            CamouflagerDuration = new CustomNumberOption(num++, MultiMenu.imposter, "Camouflage Duration", 10, 5, 15, 1f, CooldownFormat);*/

            Grenadier =
                new CustomHeaderOption(num++, MultiMenu.imposter,"<color=#FF0000FF>Grenadier</color>");
            GrenadeCooldown =
                new CustomNumberOption(num++,MultiMenu.imposter, "Flash Grenade Cooldown", 25, 2.5f, 90, 2.5f, CooldownFormat);
            GrenadeDuration =
                new CustomNumberOption(num++,MultiMenu.imposter, "Flash Grenade Duration", 4, 1, 90, 1f, CooldownFormat);
            FlashRadius =
                new CustomNumberOption(num++,MultiMenu.imposter, "Flash Radius", 1.25f, 0.25f, 10f, 0.25f, MultiplierFormat);
            GrenadierIndicators =
                new CustomToggleOption(num++,MultiMenu.imposter, "Indicate Flashed Crewmates", true);
            GrenadierVent =
                new CustomToggleOption(num++,MultiMenu.imposter, "Grenadier Can Vent", true);

            Morphling =
                new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Morphling</color>");
            MorphlingCooldown =
                new CustomNumberOption(num++,MultiMenu.imposter, "Morphling Cooldown", 25, 2.5f, 90, 2.5f, CooldownFormat);
            MorphlingDuration =
                new CustomNumberOption(num++,MultiMenu.imposter, "Morphling Duration", 10, 1, 90, 1f, CooldownFormat);
            MorphlingVent =
                new CustomToggleOption(num++,MultiMenu.imposter, "Morphling Can Vent", true);
            MorphlingMorphVent =
                new CustomToggleOption(num++,MultiMenu.imposter, "Morphling Can Vent When Morph", false);

            Swooper = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Swooper</color>");

            SwoopCooldown =
                new CustomNumberOption(num++,MultiMenu.imposter, "Swoop Cooldown", 25, 1, 90, 2.5f, CooldownFormat);
            SwoopDuration =
                new CustomNumberOption(num++,MultiMenu.imposter, "Swoop Duration", 10, 1, 90, 1f, CooldownFormat);
            SwooperVent =
                new CustomToggleOption(num++,MultiMenu.imposter, "Swooper Can Vent", false);

            Poisoner =
                new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Poisoner</color>");
            PoisonCooldown =
                new CustomNumberOption(num++,MultiMenu.imposter, "Poison Cooldown", 27.5f, 2.5f, 90, 2.5f, CooldownFormat);
            PoisonDuration =
                new CustomNumberOption(num++,MultiMenu.imposter, "Poison Kill Delay", 4, 1, 90, 1f, CooldownFormat);
            PoisonerVent =
                new CustomToggleOption(num++,MultiMenu.imposter, "Poisoner Can Vent", true);

            Traitor = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Traitor</color>");
            LatestSpawn = new CustomNumberOption(num++,MultiMenu.imposter, "Minimum People Alive When Traitor Can Spawn", 7, 2, 15, 1);
            NeutralKillingStopsTraitor =
                new CustomToggleOption(num++,MultiMenu.imposter, "Traitor Won't Spawn If Any Neutral Killing Is Alive", false);

            Underdog = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Underdog</color>");
            UnderdogKillBonus = new CustomNumberOption(num++,MultiMenu.imposter, "Kill Cooldown Bonus", 10, 2.5f, 90, 2.5f, CooldownFormat);
            UnderdogIncreasedKC = new CustomToggleOption(num++,MultiMenu.imposter, "Increased Kill Cooldown When 2+ Imps", true);

            Lycan = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Lycan</color>");
            WolfCooldown = new CustomNumberOption(num++, MultiMenu.imposter,"Lycanthropy Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
            WolfDuration = new CustomNumberOption(num++, MultiMenu.imposter,"Lycanthropy Duration", 10, 5, 15, 1f, CooldownFormat);

            Blackmailer = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Blackmailer</color>");
            BlackmailCooldown =
                new CustomNumberOption(num++,MultiMenu.imposter, "Initial Blackmail Cooldown", 20, 1, 90, 1f, CooldownFormat);

            Miner = new CustomHeaderOption(num++, MultiMenu.imposter,"<color=#FF0000FF>Miner</color>");
            MineCooldown =
                new CustomNumberOption(num++,MultiMenu.imposter, "Mine Cooldown", 25f, 2.5f, 90, 2.5f, CooldownFormat);

            Undertaker = new CustomHeaderOption(num++,MultiMenu.imposter, "<color=#FF0000FF>Undertaker</color>");
            DragCooldown = new CustomNumberOption(num++, MultiMenu.imposter,"Drag Cooldown", 25, 2.5f, 90, 2.5f, CooldownFormat);
            UndertakerVent =
                new CustomToggleOption(num++,MultiMenu.imposter, "Undertaker Can Vent", true);
            UndertakerVentWithBody =
                new CustomToggleOption(num++, MultiMenu.imposter,"Undertaker Can Vent While Dragging", false);

            Bait = new CustomHeaderOption(num++,MultiMenu.modifiers, "<color=#00B3B3FF>Bait</color>");
            BaitMinDelay = new CustomNumberOption(num++,MultiMenu.modifiers, "Minimum Delay for the Bait Report", 0.1f, 0.1f, 90f, 0.1f, CooldownFormat);
            BaitMaxDelay = new CustomNumberOption(num++, MultiMenu.modifiers,"Maximum Delay for the Bait Report", 2.2f, 0.1f, 90f, 0.1f, CooldownFormat);

            Diseased = new CustomHeaderOption(num++,MultiMenu.modifiers, "<color=#808080FF>Diseased</color>");
            DiseasedKillMultiplier = new CustomNumberOption(num++,MultiMenu.modifiers, "Diseased Kill Multiplier", 3f, 1f, 10f, 0.5f, MultiplierFormat);

            Flash = new CustomHeaderOption(num++,MultiMenu.modifiers, "<color=#FF8080FF>Flash</color>");
            FlashSpeed = new CustomNumberOption(num++,MultiMenu.modifiers, "Flash Speed", 1.25f, 1.05f, 10f, 0.05f, MultiplierFormat);

            Giant = new CustomHeaderOption(num++, MultiMenu.modifiers,"<color=#FFB34DFF>Giant</color>");
            GiantSlow = new CustomNumberOption(num++,MultiMenu.modifiers, "Giant Speed", 1f, 0.05f, 10f, 0.05f, MultiplierFormat);

            Lovers =
                new CustomHeaderOption(num++, MultiMenu.modifiers,"<color=#FF66CCFF>Lovers</color>");
            BothLoversDie = new CustomToggleOption(num++, MultiMenu.modifiers,"Both Lovers Die", true);
            LovingImpPercent = new CustomNumberOption(num++, MultiMenu.modifiers,"Loving Impostor Probability", 40f, 0f, 100f, 05f,
                PercentFormat);
            NeutralLovers = new CustomToggleOption(num++,MultiMenu.modifiers, "Neutral Roles Can Be Lovers", false);
        }

        private static string[] StrToNbr(string zero, int max)
        {
          List<string> result = new List<string>(){zero};
          for (int i = 0; i < max; i++) result.Add($"{i + 1}");
          return result.ToArray();
        }
    }
}