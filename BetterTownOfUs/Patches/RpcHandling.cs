using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Hazel;
using Reactor;
using Reactor.Extensions;
using BetterTownOfUs.CrewmateRoles.AltruistMod;
using BetterTownOfUs.CrewmateRoles.MedicMod;
using BetterTownOfUs.CrewmateRoles.SeerMod;
using BetterTownOfUs.CrewmateRoles.SwapperMod;
using BetterTownOfUs.CrewmateRoles.TimeLordMod;
using BetterTownOfUs.CustomOption;
using BetterTownOfUs.Extensions;
using BetterTownOfUs.Modifiers.AssassinMod;
using BetterTownOfUs.Modifiers.EraserMod;
using PerformKill = BetterTownOfUs.ImpostorRoles.MinerMod.PerformKill;
using BetterTownOfUs.NeutralRoles.ExecutionerMod;
using BetterTownOfUs.NeutralRoles.GuardianAngelMod;
using BetterTownOfUs.NeutralRoles.PhantomMod;
using BetterTownOfUs.CrewmateRoles.HaunterMod;
using BetterTownOfUs.ImpostorRoles.TraitorMod;
using BetterTownOfUs.Roles;
using BetterTownOfUs.Roles.Modifiers;
using UnhollowerBaseLib;
using UnityEngine;
using Coroutine = BetterTownOfUs.ImpostorRoles.JanitorMod.Coroutine;
using Coroutine2 = BetterTownOfUs.NeutralRoles.CannibalMod.Coroutine;
using Coroutine3 = BetterTownOfUs.ImpostorRoles.LycanMod.Coroutine;
using Object = UnityEngine.Object;
using PerformKillButton = BetterTownOfUs.NeutralRoles.AmnesiacMod.PerformKillButton;
using Random = UnityEngine.Random; //using Il2CppSystem;

namespace BetterTownOfUs
{
    public static class RpcHandling
    {
        public static bool CheckImpostors;
        public static int NumImpostors;
        private static readonly List<(Type, CustomRPC, int)> CrewmateRoles = new List<(Type, CustomRPC, int)>();
        private static readonly List<(Type, CustomRPC, int)> NeutralNonKillingRoles = new List<(Type, CustomRPC, int)>();
        private static readonly List<(Type, CustomRPC, int)> NeutralKillingRoles = new List<(Type, CustomRPC, int)>();
        private static readonly List<(Type, CustomRPC, int)> ImpostorRoles = new List<(Type, CustomRPC, int)>();
        private static readonly List<(Type, CustomRPC, int)> CrewmateModifiers = new List<(Type, CustomRPC, int)>();
        private static readonly List<(Type, CustomRPC, int)> GlobalModifiers = new List<(Type, CustomRPC, int)>();
        private static readonly List<(Type, CustomRPC, int)> ButtonModifiers = new List<(Type, CustomRPC, int)>();
        private static readonly List<(Type, CustomRPC, int)> AssassinModifier = new List<(Type, CustomRPC, int)>();
        private static readonly List<(Type, CustomRPC, int)> BaitModifiers = new List<(Type, CustomRPC, int)>();
        private static bool PhantomOn;
        private static bool HaunterOn;
        private static bool TraitorOn;

        internal static bool Check(int probability)
        {
            if (probability == 0) return false;
            if (probability == 100) return true;
            var num = BetterTownOfUs.Random.Next(1, 101);
            return num <= probability;
        }
        internal static bool CheckJugg()
        {
            var num = BetterTownOfUs.Random.Next(1, 101);
            return num <= 10 * NeutralKillingRoles.Count;
        }
        internal static void ChooseOne(List<(Type, CustomRPC, int)> roles)
        {
            SortRoles(roles, 1, 1);
            switch (roles.FirstOrDefault().Item2)
            {
                case CustomRPC.SetJester:
                case CustomRPC.SetExecutioner:
                    NeutralNonKillingRoles.AddRange(roles);
                    break;
                case CustomRPC.SetJanitor:
                case CustomRPC.SetLycan:
                case CustomRPC.SetMiner:
                case CustomRPC.SetBlackmailer:
                case CustomRPC.SetUndertaker:
                    ImpostorRoles.AddRange(roles);
                    break;
                case CustomRPC.SetArsonist:
                case CustomRPC.SetPlaguebearer:
                    NeutralKillingRoles.AddRange(roles);
                    break;
            }
        }

        private static void SortRoles(List<(Type, CustomRPC, int)> roles, int max, int min, int add = 0)
        {
            roles.Shuffle();
            if (roles.Count < max) max = roles.Count;
            if (min > max) min = max;
            var amount = BetterTownOfUs.Random.Next(min, max + 1);
            amount += add;
            roles.Sort((a, b) =>
            {
                var a_ = a.Item3 == 100 ? 0 : 100;
                var b_ = b.Item3 == 100 ? 0 : 100;
                return a_.CompareTo(b_);
            });
            var certainRoles = 0;
            var odds = 0;
            foreach (var role in roles)
                if (role.Item3 == 100) certainRoles += 1;
                else odds += role.Item3;
            while (certainRoles < amount)
            {
                var num = certainRoles;
                var random = BetterTownOfUs.Random.Next(0, odds);
                var rolePicked = false;
                while (num < roles.Count && rolePicked == false)
                {
                    random -= roles[num].Item3;
                    if (random < 0)
                    {
                        odds -= roles[num].Item3;
                        var role = roles[num];
                        roles.Remove(role);
                        roles.Insert(certainRoles, role);
                        certainRoles += 1;
                        rolePicked = true;
                    }
                    num += 1;
                }
            }
            while (roles.Count > amount) roles.RemoveAt(roles.Count - 1);
        }

        private static void SortModifiers(List<(Type, CustomRPC, int)> roles, int max)
        {
            roles.Shuffle();
            roles.Sort((a, b) =>
            {
                var a_ = a.Item3 == 100 ? 0 : 100;
                var b_ = b.Item3 == 100 ? 0 : 100;
                return a_.CompareTo(b_);
            });
            while (roles.Count > max) roles.RemoveAt(roles.Count - 1);
        }

        private static void GenEachRole(List<GameData.PlayerInfo> infected)
        {
            var impostors = Utils.GetImpostors(infected); //SetImpostors(PlayerControl.AllPlayerControls.ToArray().ToList());
            var crewmates = Utils.GetCrewmates(impostors);
            crewmates.Shuffle();
            impostors.Shuffle();

            if (crewmates.Count > CustomGameOptions.MaxNeutralKillingRoles)
                SortRoles(NeutralKillingRoles, CustomGameOptions.MaxNeutralKillingRoles, CustomGameOptions.MinNeutralKillingRoles);
            else SortRoles(NeutralKillingRoles, crewmates.Count - 1, CustomGameOptions.MinNeutralKillingRoles);
            if (crewmates.Count - NeutralKillingRoles.Count > CustomGameOptions.MaxNeutralNonKillingRoles)
                SortRoles(NeutralNonKillingRoles, CustomGameOptions.MaxNeutralNonKillingRoles, CustomGameOptions.MinNeutralNonKillingRoles);
            else SortRoles(NeutralNonKillingRoles, crewmates.Count - NeutralKillingRoles.Count - 1, CustomGameOptions.MinNeutralKillingRoles);
            SortRoles(ImpostorRoles, impostors.Count, impostors.Count);

            if (impostors.Count() > 1 && NeutralKillingRoles.Count > 0 && CheckJugg())
            {
                NeutralKillingRoles.RemoveAt(NeutralKillingRoles.Count - 1);
                NeutralKillingRoles.Add((typeof(Juggernaut), CustomRPC.SetJuggernaut, 100));
                NeutralKillingRoles.Shuffle();
            }

            SortRoles(CrewmateRoles, crewmates.Count - NeutralNonKillingRoles.Count - NeutralKillingRoles.Count,
                crewmates.Count - NeutralNonKillingRoles.Count - NeutralKillingRoles.Count);

            SortModifiers(CrewmateModifiers, crewmates.Count);
            SortModifiers(GlobalModifiers, crewmates.Count + impostors.Count);
            SortModifiers(ButtonModifiers, crewmates.Count + impostors.Count);
            SortModifiers(BaitModifiers, crewmates.Count + impostors.Count);

            var crewAndNeutralRoles = new List<(Type, CustomRPC, int)>();
            crewAndNeutralRoles.AddRange(CrewmateRoles);
            crewAndNeutralRoles.AddRange(NeutralNonKillingRoles);
            crewAndNeutralRoles.AddRange(NeutralKillingRoles);

            if (Check(CustomGameOptions.VanillaGame))
            {
                CrewmateRoles.Clear();
                NeutralNonKillingRoles.Clear();
                NeutralKillingRoles.Clear();
                crewAndNeutralRoles.Clear();
                CrewmateModifiers.Clear();
                GlobalModifiers.Clear();
                ButtonModifiers.Clear();
                AssassinModifier.Clear();
                ImpostorRoles.Clear();
                PhantomOn = false;
                HaunterOn = false;
                TraitorOn = false;
            }
            foreach (var (type, rpc, _) in crewAndNeutralRoles)
            {
                Role.Gen<Role>(type, crewmates, rpc);
            }

            foreach (var (type, rpc, _) in ImpostorRoles)
            {
                Role.Gen<Role>(type, impostors, rpc);
            }

            foreach (var crewmate in crewmates)
                Role.Gen<Role>(typeof(Crewmate), crewmate, CustomRPC.SetCrewmate);

            foreach (var impostor in impostors)
                Role.Gen<Role>(typeof(Impostor), impostor, CustomRPC.SetImpostor);

            var canHaveModifier = PlayerControl.AllPlayerControls.ToArray().ToList();
            var canHaveModifier2 = PlayerControl.AllPlayerControls.ToArray().ToList();
            var canHaveAbility = PlayerControl.AllPlayerControls.ToArray().ToList();
            canHaveModifier.RemoveAll(player => player.Is(RoleEnum.Altruist) || player.Is(RoleEnum.Sheriff));
            canHaveModifier.Shuffle();
            canHaveAbility.RemoveAll(player => !player.Is(Faction.Impostors));
            var NeutralGuesser = PlayerControl.AllPlayerControls.ToArray().ToList();
            NeutralGuesser.RemoveAll(player => !player.Is(RoleEnum.Juggernaut) && !player.Is(RoleEnum.Glitch) && !player.Is(RoleEnum.Pestilence) && !player.Is(RoleEnum.Plaguebearer) && !player.Is(RoleEnum.Werewolf));
            if (CustomGameOptions.NeutralGuess == true)
            {
                canHaveAbility.AddRange(NeutralGuesser);
            }
            canHaveAbility.Shuffle();
            var assassins = CustomGameOptions.NumberOfAssassins;
            var canHaveAbility2 = PlayerControl.AllPlayerControls.ToArray().ToList();
            canHaveAbility2.RemoveAll(player => !player.Is(Faction.Crewmates));
            canHaveAbility2.Shuffle();
            var crewAssassins = CustomGameOptions.NumberOfCrewAssassins;


            
            foreach (var (type, rpc, _) in GlobalModifiers)
            {
                if (canHaveModifier.Count == 0) break;
                if(rpc == CustomRPC.SetCouple)
                {
                    if (canHaveModifier.Count == 1) continue;
                        Lover.Gen(canHaveModifier);
                }
                else
                {
                    Role.Gen<Modifier>(type, canHaveModifier, rpc);
                }
            }

            canHaveModifier.RemoveAll(player => player.Is(RoleEnum.Glitch));

            foreach (var (type, rpc, _) in ButtonModifiers)
            {
                if (canHaveModifier.Count == 0) break;
                Role.Gen<Modifier>(type, canHaveModifier, rpc);
            }

            canHaveModifier.RemoveAll(player => player.Is(RoleEnum.Juggernaut) || player.Is(RoleEnum.Werewolf) || player.Is(RoleEnum.Pestilence)
            || player.Is(RoleEnum.Plaguebearer) || player.Is(RoleEnum.Arsonist) || player.Is(Faction.Impostors));
            canHaveModifier.Shuffle();

            while (canHaveModifier.Count > 0 && CrewmateModifiers.Count > 0)
            {
                var (type, rpc, _) = CrewmateModifiers.TakeFirst();
                Role.Gen<Modifier>(type, canHaveModifier.TakeFirst(), rpc);
            }
            canHaveModifier2.RemoveAll(player => player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Altruist) || player.Is(RoleEnum.Altruist));
            canHaveModifier2.Shuffle();

            while (canHaveModifier2.Count > 0 && BaitModifiers.Count > 0)
            {
                var (type, rpc, _) = BaitModifiers.TakeFirst();
                Role.Gen<Modifier>(type, canHaveModifier2.TakeFirst(), rpc);
            }

            while (canHaveAbility.Count > 0 && assassins > 0)
            {
                if (assassins == CustomGameOptions.NumberOfAssassins && CustomGameOptions.DontReplaceFistAssassin && CustomGameOptions.EraserSeperate)
                {
                     //first assassin
                    Role.Gen<Ability>(typeof(Assassin), canHaveAbility.TakeFirst(), CustomRPC.SetAssassin);
                }
                else if (assassins == CustomGameOptions.NumberOfAssassins - 1 && CustomGameOptions.SecondAssassinAlwaysEraser && CustomGameOptions.EraserSeperate)
                {
                    //second assassin
                    Role.Gen<Ability>(typeof(Eraser), canHaveAbility.TakeFirst(), CustomRPC.SetEraser);
                }
                else if (CustomGameOptions.EraserSeperate)
                {
                    if (Random.Range(0f, 100f) <= CustomGameOptions.EraserChance)
                    {
                        Role.Gen<Ability>(typeof(Eraser), canHaveAbility.TakeFirst(), CustomRPC.SetEraser);
                    } else
                    {
                        Role.Gen<Ability>(typeof(Assassin), canHaveAbility.TakeFirst(), CustomRPC.SetAssassin);
                    }
                }
                else
                {
                    var (type, rpc, _) = AssassinModifier.Ability();
                    Role.Gen<Ability>(type, canHaveAbility.TakeFirst(), rpc);
                }
                
                assassins = assassins - 1;
            }
            
            while (canHaveAbility2.Count > 0 && crewAssassins > 0)
                {
                    var (type, rpc, _) = AssassinModifier.Ability();
                    Role.Gen<Ability>(type, canHaveAbility2.TakeFirst(), rpc);
                    crewAssassins -= 1;
                }
            

            var toChooseFromCrew = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Crewmates) && !x.Is(ModifierEnum.Lover)).ToList();
            if (TraitorOn && toChooseFromCrew.Count != 0)
            {
                var rand = BetterTownOfUs.Random.Next(0, toChooseFromCrew.Count);
                var pc = toChooseFromCrew[rand];

                SetTraitor.WillBeTraitor = pc;

                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.SetTraitor, SendOption.Reliable, -1);
                writer.Write(pc.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }
            else
            {
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.SetTraitor, SendOption.Reliable, -1);
                writer.Write(byte.MaxValue);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }

            var toChooseFromNeut = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Neutral) && !x.Is(ModifierEnum.Lover)).ToList();
            if (PhantomOn && toChooseFromNeut.Count != 0)
            {
                var rand = BetterTownOfUs.Random.Next(0, toChooseFromNeut.Count);
                var pc = toChooseFromNeut[rand];

                SetPhantom.WillBePhantom = pc;

                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.SetPhantom, SendOption.Reliable, -1);
                writer.Write(pc.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }
            else
            {
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.SetPhantom, SendOption.Reliable, -1);
                writer.Write(byte.MaxValue);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }

            toChooseFromCrew = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Crewmates) && !x.Is(ModifierEnum.Lover) && x != SetTraitor.WillBeTraitor).ToList();
            if (HaunterOn && toChooseFromCrew.Count != 0)
            {
                var rand = BetterTownOfUs.Random.Next(0, toChooseFromCrew.Count);
                var pc = toChooseFromCrew[rand];

                SetHaunter.WillBeHaunter = pc;

                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.SetHaunter, SendOption.Reliable, -1);
                writer.Write(pc.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }
            else
            {
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.SetHaunter, SendOption.Reliable, -1);
                writer.Write(byte.MaxValue);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }

            var exeTargets = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Is(Faction.Crewmates) && !x.Is(ModifierEnum.Lover) && !x.Is(RoleEnum.Mayor) && !x.Is(RoleEnum.Sheriff) && !x.Is(RoleEnum.Veteran) && x != SetTraitor.WillBeTraitor).ToList();
            foreach (var role in Role.GetRoles(RoleEnum.Executioner))
            {
                var exe = (Executioner)role;
                if (exeTargets.Count > 0)
                {
                    exe.target = exeTargets[BetterTownOfUs.Random.Next(0, exeTargets.Count)];

                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte)CustomRPC.SetTarget, SendOption.Reliable, -1);
                    writer.Write(role.Player.PlayerId);
                    writer.Write(exe.target.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                }
            }

            var gaTargets = PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Is(Faction.Neutral) && !x.Is(ModifierEnum.Lover)).ToList();
            foreach (var role in Role.GetRoles(RoleEnum.GuardianAngel))
            {
                var ga = (GuardianAngel)role;
                if (gaTargets.Count > 0)
                {
                    ga.target = gaTargets[BetterTownOfUs.Random.Next(0, gaTargets.Count)];

                    var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte)CustomRPC.SetGATarget, SendOption.Reliable, -1);
                    writer.Write(role.Player.PlayerId);
                    writer.Write(ga.target.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                }
            }
        }


        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
        public static class HandleRpc
        {
            public static void Postfix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
            {
                //if (callId >= 43) //System.Console.WriteLine("Received " + callId);
                byte readByte, readByte1, readByte2;
                sbyte readSByte, readSByte2;
                switch ((CustomRPC) callId)
                {
                    case CustomRPC.SetMayor:
                        readByte = reader.ReadByte();
                        new Mayor(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetJester:
                        readByte = reader.ReadByte();
                        new Jester(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetCannibal:
                        readByte = reader.ReadByte();
                        new Cannibal(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetSheriff:
                        readByte = reader.ReadByte();
                        new Sheriff(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetEngineer:
                        readByte = reader.ReadByte();
                        new Engineer(Utils.PlayerById(readByte));
                        break;


                    case CustomRPC.SetJanitor:
                        new Janitor(Utils.PlayerById(reader.ReadByte()));

                        break;

                    case CustomRPC.SetSwapper:
                        readByte = reader.ReadByte();
                        new Swapper(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetAmnesiac:
                        readByte = reader.ReadByte();
                        new Amnesiac(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetInvestigator:
                        readByte = reader.ReadByte();
                        new Investigator(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetTimeLord:
                        readByte = reader.ReadByte();
                        new TimeLord(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetTorch:
                        readByte = reader.ReadByte();
                        new Torch(Utils.PlayerById(readByte));
                        break;
                    case CustomRPC.SetDiseased:
                        readByte = reader.ReadByte();
                        new Diseased(Utils.PlayerById(readByte));
                        break;
                    case CustomRPC.SetBait:
                        readByte = reader.ReadByte();
                        new Bait(Utils.PlayerById(readByte));
                        break;
                    case CustomRPC.SetVoteCounter:
                        readByte = reader.ReadByte();
                        new VoteCounter(Utils.PlayerById(readByte));
                        break;
                    case CustomRPC.SetFlash:
                        readByte = reader.ReadByte();
                        new Flash(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.SetMedic:
                        readByte = reader.ReadByte();
                        new Medic(Utils.PlayerById(readByte));
                        break;
                    case CustomRPC.SetMorphling:
                        readByte = reader.ReadByte();
                        new Morphling(Utils.PlayerById(readByte));
                        break;
                    case CustomRPC.SetPoisoner:
                        readByte = reader.ReadByte();
                        new Poisoner(Utils.PlayerById(readByte));
                        break;

                    case CustomRPC.LoveWin:
                        var winnerlover = Utils.PlayerById(reader.ReadByte());
                        Modifier.GetModifier<Lover>(winnerlover).Win();
                        break;


                    case CustomRPC.JesterLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Jester)
                                ((Jester) role).Loses();

                        break;

                    case CustomRPC.CannibalWin:
                        var theCannibal = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Cannibal);
                        ((Cannibal) theCannibal)?.Wins();
                        break;

                    case CustomRPC.CannibalLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Cannibal)
                                ((Cannibal) role).Loses();

                        break;
                    case CustomRPC.PhantomLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Phantom)
                                ((Phantom) role).Loses();

                        break;


                    case CustomRPC.GlitchLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Glitch)
                                ((Glitch) role).Loses();

                        break;


                    case CustomRPC.JuggernautLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Juggernaut)
                                ((Juggernaut)role).Loses();

                        break;

                    /*case CustomRPC.ShifterLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Shifter)
                                ((Shifter) role).Loses();

                        break;*/

                    case CustomRPC.AmnesiacLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Amnesiac)
                                ((Amnesiac)role).Loses();

                        break;

                    case CustomRPC.ExecutionerLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Executioner)
                                ((Executioner) role).Loses();

                        break;

                    case CustomRPC.NobodyWins:
                        Role.NobodyWinsFunc();
                        break;

                    case CustomRPC.SurvivorOnlyWin:
                        Role.SurvOnlyWin();
                        break;

                    case CustomRPC.SetCouple:
                        var id = reader.ReadByte();
                        var id2 = reader.ReadByte();
                        var lover1 = Utils.PlayerById(id);
                        var lover2 = Utils.PlayerById(id2);

                        var modifierLover1 = new Lover(lover1);
                        var modifierLover2 = new Lover(lover2);

                        modifierLover1.OtherLover = modifierLover2;
                        modifierLover2.OtherLover = modifierLover1;

                        break;

                    case CustomRPC.Start:
                        /*
                        EngineerMod.PerformKill.UsedThisRound = false;
                        EngineerMod.PerformKill.SabotageTime = DateTime.UtcNow.AddSeconds(-100);
                        */
                        Utils.ShowDeadBodies = false;
                        Murder.KilledPlayers.Clear();
                        Role.NobodyWins = false;
                        Role.SurvOnlyWins = false;
                        RecordRewind.points.Clear();
                        KillButtonTarget.DontRevive = byte.MaxValue;
                        AssassinKill.HaveFailedGuess = false;
                        break;

                    case CustomRPC.JanitorClean:
                        readByte1 = reader.ReadByte();
                        var janitorPlayer = Utils.PlayerById(readByte1);
                        var janitorRole = Role.GetRole<Janitor>(janitorPlayer);
                        readByte = reader.ReadByte();
                        var deadBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in deadBodies)
                            if (body.ParentId == readByte)
                                Coroutines.Start(Coroutine.CleanCoroutine(body, janitorRole));

                        break;

                    case CustomRPC.CannibalClean:
                        readByte1 = reader.ReadByte();
                        var cannibalPlayer = Utils.PlayerById(readByte1);
                        var cannibalRole = Role.GetRole<Cannibal>(cannibalPlayer);
                        readByte = reader.ReadByte();
                        var deadBodies2 = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in deadBodies2)
                            if (body.ParentId == readByte)
                                Coroutines.Start(Coroutine2.CleanCoroutine(body, cannibalRole));

                        break;

                    case CustomRPC.WolfClean:
                        readByte1 = reader.ReadByte();
                        var lycanPlayer = Utils.PlayerById(readByte1);
                        var lycanRole = Role.GetRole<Lycan>(lycanPlayer);
                        var bodyId = reader.ReadByte();
                        lycanRole.Eaten = byte.MaxValue;
                        Coroutines.Start(Coroutine3.CleanCoroutine(bodyId, lycanRole));

                        break;
                    case CustomRPC.EngineerFix:
                        var engineer = Utils.PlayerById(reader.ReadByte());
                        var EngineerRole = Role.GetRole<Engineer>(engineer);
                        EngineerRole.EngiFixPerRound--;
                        EngineerRole.EngiFixPerGame--;
                        break;


                    case CustomRPC.FixLights:
                        var lights = ShipStatus.Instance.Systems[SystemTypes.Electrical].Cast<SwitchSystem>();
                        lights.ActualSwitches = lights.ExpectedSwitches;
                        break;

                    case CustomRPC.SetExtraVotes:

                        var mayor = Utils.PlayerById(reader.ReadByte());
                        var mayorRole = Role.GetRole<Mayor>(mayor);
                        mayorRole.ExtraVotes = reader.ReadBytesAndSize().ToList();
                        if (!mayor.Is(RoleEnum.Mayor)) mayorRole.VoteBank -= mayorRole.ExtraVotes.Count;

                        break;

                    case CustomRPC.SetSwaps:
                        readSByte = reader.ReadSByte();
                        SwapVotes.Swap1 =
                            MeetingHud.Instance.playerStates.FirstOrDefault(x => x.TargetPlayerId == readSByte);
                        readSByte2 = reader.ReadSByte();
                        SwapVotes.Swap2 =
                            MeetingHud.Instance.playerStates.FirstOrDefault(x => x.TargetPlayerId == readSByte2);
                        break;
                    case CustomRPC.Remember:
                        readByte1 = reader.ReadByte();
                        readByte2 = reader.ReadByte();
                        var amnesiac = Utils.PlayerById(readByte1);
                        var other = Utils.PlayerById(readByte2);
                        PerformKillButton.Remember(Role.GetRole<Amnesiac>(amnesiac), other);
                        break;
                    case CustomRPC.Rewind:
                        readByte = reader.ReadByte();
                        var TimeLordPlayer = Utils.PlayerById(readByte);
                        var TimeLordRole = Role.GetRole<TimeLord>(TimeLordPlayer);
                        StartStop.StartRewind(TimeLordRole);
                        break;
                    case CustomRPC.Protect:
                        readByte1 = reader.ReadByte();
                        readByte2 = reader.ReadByte();

                        var medic = Utils.PlayerById(readByte1);
                        var shield = Utils.PlayerById(readByte2);
                        Role.GetRole<Medic>(medic).ShieldedPlayer = shield;
                        Role.GetRole<Medic>(medic).UsedAbility = true;
                        break;
                    case CustomRPC.RewindRevive:
                        readByte = reader.ReadByte();
                        RecordRewind.ReviveBody(Utils.PlayerById(readByte));
                        break;
                    case CustomRPC.AttemptSound:
                        var medicId = reader.ReadByte();
                        readByte = reader.ReadByte();
                        StopKill.BreakShield(medicId, readByte, CustomGameOptions.ShieldBreaks);
                        break;
                    case CustomRPC.SetGlitch:
                        var GlitchId = reader.ReadByte();
                        var GlitchPlayer = Utils.PlayerById(GlitchId);
                        new Glitch(GlitchPlayer);
                        break;
                    case CustomRPC.SetJuggernaut:
                        var JuggernautId = reader.ReadByte();
                        var JuggernautPlayer = Utils.PlayerById(JuggernautId);
                        new Juggernaut(JuggernautPlayer);
                        break;
                    case CustomRPC.BypassKill:
                        var killer = Utils.PlayerById(reader.ReadByte());
                        var target = Utils.PlayerById(reader.ReadByte());

                        Utils.MurderPlayer(killer, target);
                        break;
                    case CustomRPC.AssassinKill:
                        var toDie = Utils.PlayerById(reader.ReadByte());
                        AssassinKill.MurderPlayer(toDie);
                        break;
                    case CustomRPC.Erase:
                        var toErase = Utils.PlayerById(reader.ReadByte());
                        AssassinKill.ErasePlayer(toErase);
                        break;
                    case CustomRPC.AssassinFail:
                        var AssassinId = reader.ReadByte();
                        var targetIdFail = reader.ReadByte();
                        AssassinKill.AssassinFail(AssassinId, targetIdFail);
                        break;
                    case CustomRPC.SetMimic:
                        var glitchPlayer = Utils.PlayerById(reader.ReadByte());
                        var mimicPlayer = Utils.PlayerById(reader.ReadByte());
                        var glitchRole = Role.GetRole<Glitch>(glitchPlayer);
                        glitchRole.MimicTarget = mimicPlayer;
                        glitchRole.IsUsingMimic = true;
                        Utils.Morph(glitchPlayer, mimicPlayer);
                        break;
                    case CustomRPC.RpcResetAnim:
                        var animPlayer = Utils.PlayerById(reader.ReadByte());
                        var theGlitchRole = Role.GetRole<Glitch>(animPlayer);
                        theGlitchRole.MimicTarget = null;
                        theGlitchRole.IsUsingMimic = false;
                        Utils.Unmorph(theGlitchRole.Player);
                        break;
                    case CustomRPC.GlitchWin:
                        var theGlitch = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Glitch);
                        ((Glitch) theGlitch)?.Wins();
                        break;
                    case CustomRPC.JuggernautWin:
                        var juggernaut = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Juggernaut);
                        ((Juggernaut)juggernaut)?.Wins();
                        break;
                    case CustomRPC.SetHacked:
                        var hackPlayer = Utils.PlayerById(reader.ReadByte());
                        if (hackPlayer.PlayerId == PlayerControl.LocalPlayer.PlayerId)
                        {
                            var glitch = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Glitch);
                            ((Glitch) glitch)?.SetHacked(hackPlayer);
                        }

                        break;
                    case CustomRPC.Investigate:
                        var seer = Utils.PlayerById(reader.ReadByte());
                        var otherPlayer = Utils.PlayerById(reader.ReadByte());
                        bool successfulSee = reader.ReadByte() == 1;
                        var seerRole = Role.GetRole<Seer>(seer);
                        seerRole.LastInvestigated = DateTime.UtcNow;
                        if (!successfulSee) break;
                        seerRole.Investigated.Add(otherPlayer.PlayerId);
                        if (!otherPlayer.AmOwner) break;
                        var otherRole = Role.GetRole(otherPlayer);
                        var flash = false;
                        switch (otherRole.Faction)
                        {
                            case Faction.Crewmates:
                                if (CustomGameOptions.SeeReveal == SeeReveal.All || CustomGameOptions.SeeReveal == SeeReveal.Crew) flash = true;
                                break;
                            case Faction.Neutral:
                            case Faction.Impostors:
                                if (CustomGameOptions.SeeReveal == SeeReveal.All || CustomGameOptions.SeeReveal == SeeReveal.ImpsAndNeut) flash = true;
                                break;
                        }
                        if (flash) Coroutines.Start(Utils.FlashCoroutine(seerRole.Color));
                        break;
                    case CustomRPC.SetSeer:
                        new Seer(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.Morph:
                        var morphling = Utils.PlayerById(reader.ReadByte());
                        var morphTarget = Utils.PlayerById(reader.ReadByte());
                        var morphRole = Role.GetRole<Morphling>(morphling);
                        morphRole.TimeRemaining = CustomGameOptions.MorphlingDuration;
                        morphRole.MorphedPlayer = morphTarget;
                        break;
                    case CustomRPC.Wolf:
                        var lycan = Utils.PlayerById(reader.ReadByte());
                        var wolfRole = Role.GetRole<Lycan>(lycan);
                        wolfRole.TimeRemaining = CustomGameOptions.WolfDuration;
                        wolfRole.Wolfed = true;
                        break;
                    case CustomRPC.Poison:
                        var poisoner = Utils.PlayerById(reader.ReadByte());
                        var poisoned = Utils.PlayerById(reader.ReadByte());
                        var poisonerRole = Role.GetRole<Poisoner>(poisoner);
                        poisonerRole.PoisonedPlayer = poisoned;
                        break;
                    case CustomRPC.SetExecutioner:
                        new Executioner(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetGuardianAngel:
                        new GuardianAngel(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetTarget:
                        var exe = Utils.PlayerById(reader.ReadByte());
                        var exeTarget = Utils.PlayerById(reader.ReadByte());
                        var exeRole = Role.GetRole<Executioner>(exe);
                        exeRole.target = exeTarget;
                        break;
                    case CustomRPC.SetGATarget:
                        var ga = Utils.PlayerById(reader.ReadByte());
                        var gaTarget = Utils.PlayerById(reader.ReadByte());
                        var gaRole = Role.GetRole<GuardianAngel>(ga);
                        gaRole.target = gaTarget;
                        break;
                    case CustomRPC.SetBlackmailer:
                        new Blackmailer(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.Blackmail:
                        var blackmailer = Role.GetRole<Blackmailer>(Utils.PlayerById(reader.ReadByte()));
                        blackmailer.Blackmailed = Utils.PlayerById(reader.ReadByte());
                        break;
                    case CustomRPC.SetSpy:
                        new Spy(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.ExecutionerToJester:
                        TargetColor.ExeToJes(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.GAToSurv:
                        GATargetColor.GAToSurv(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetSnitch:
                        new Snitch(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetTracker:
                        new Tracker(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetDetective:
                        new Detective(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetSurvivor:
                        new Survivor(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetMiner:
                        new Miner(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.Mine:
                        var ventId = reader.ReadInt32();
                        var miner = Utils.PlayerById(reader.ReadByte());
                        var minerRole = Role.GetRole<Miner>(miner);
                        var pos = reader.ReadVector2();
                        var zAxis = reader.ReadSingle();
                        PerformKill.SpawnVent(ventId, minerRole, pos, zAxis);
                        break;
                    case CustomRPC.SetSwooper:
                        new Swooper(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.Swoop:
                        var swooper = Utils.PlayerById(reader.ReadByte());
                        var swooperRole = Role.GetRole<Swooper>(swooper);
                        swooperRole.TimeRemaining = CustomGameOptions.SwoopDuration;
                        swooperRole.Swoop();
                        break;
                    case CustomRPC.Alert:
                        var veteran = Utils.PlayerById(reader.ReadByte());
                        var veteranRole = Role.GetRole<Veteran>(veteran);
                        veteranRole.TimeRemaining = CustomGameOptions.AlertDuration;
                        veteranRole.Alert();
                        break;
                    case CustomRPC.Vest:
                        var surv = Utils.PlayerById(reader.ReadByte());
                        var survRole = Role.GetRole<Survivor>(surv);
                        survRole.TimeRemaining = CustomGameOptions.VestDuration;
                        survRole.Vest();
                        break;
                    case CustomRPC.GAProtect:
                        var ga2 = Utils.PlayerById(reader.ReadByte());
                        var ga2Role = Role.GetRole<GuardianAngel>(ga2);
                        ga2Role.TimeRemaining = CustomGameOptions.ProtectDuration;
                        ga2Role.Protect();
                        break;
                    case CustomRPC.Transport:
                        Coroutines.Start(Transporter.TransportPlayers(reader.ReadByte(), reader.ReadByte(), reader.ReadBoolean()));
                        break;
                    case CustomRPC.SetUntransportable:
                        if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
                        {
                            Role.GetRole<Transporter>(PlayerControl.LocalPlayer).UntransportablePlayers.Add(reader.ReadByte(), DateTime.UtcNow);
                        }
                        break;
                    case CustomRPC.Mediate:
                        var mediatedPlayer = Utils.PlayerById(reader.ReadByte());
                        var medium = Role.GetRole<Medium>(Utils.PlayerById(reader.ReadByte()));
                        if (PlayerControl.LocalPlayer.PlayerId != mediatedPlayer.PlayerId) break;
                        medium.AddMediatePlayer(mediatedPlayer.PlayerId);
                        break;
                    case CustomRPC.SetGrenadier:
                        new Grenadier(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.FlashGrenade:
                        var grenadier = Utils.PlayerById(reader.ReadByte());
                        var grenadierRole = Role.GetRole<Grenadier>(grenadier);
                        grenadierRole.TimeRemaining = CustomGameOptions.GrenadeDuration;
                        grenadierRole.Flash();
                        break;
                    case CustomRPC.SetTiebreaker:
                        new Tiebreaker(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetBlind:
                        new Blind(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetSleuth:
                        new Sleuth(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetArsonist:
                        new Arsonist(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetWerewolf:
                        new Werewolf(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.Douse:
                        var arsonist = Utils.PlayerById(reader.ReadByte());
                        var douseTarget = Utils.PlayerById(reader.ReadByte());
                        var arsonistRole = Role.GetRole<Arsonist>(arsonist);
                        arsonistRole.DousedPlayers.Add(douseTarget.PlayerId);
                        arsonistRole.LastDoused = DateTime.UtcNow;
                        break;

                    case CustomRPC.Ignite:
                        var theArsonist = Utils.PlayerById(reader.ReadByte());
                        var theArsonistRole = Role.GetRole<Arsonist>(theArsonist);
                        theArsonistRole.Ignite();
                        break;

                    case CustomRPC.ArsonistWin:
                        var theArsonistTheRole = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Arsonist);
                        ((Arsonist)theArsonistTheRole)?.Wins();
                        break;

                    case CustomRPC.ArsonistLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Arsonist) ((Arsonist)role).Loses();
                        break;
                    case CustomRPC.WerewolfWin:
                        var theWerewolfTheRole = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Werewolf);
                        ((Werewolf)theWerewolfTheRole)?.Wins();
                        break;
                    case CustomRPC.WerewolfLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Werewolf)
                                ((Werewolf)role).Loses();
                        break;
                    case CustomRPC.SurvivorImpWin:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Survivor && !role.Player.Data.IsDead && !role.Player.Data.Disconnected)
                            {
                                ((Survivor)role).AliveImpWin();
                            }
                        break;
                    case CustomRPC.SurvivorCrewWin:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Survivor && (role.Player.Data.IsDead || role.Player.Data.Disconnected))
                            {
                                ((Survivor)role).DeadCrewWin();
                            }
                        break;
                    case CustomRPC.GAImpWin:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.GuardianAngel && ((GuardianAngel)role).target.Is(Faction.Impostors))
                            {
                                ((GuardianAngel)role).ImpTargetWin();
                            }
                        break;
                    case CustomRPC.GAImpLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.GuardianAngel && ((GuardianAngel)role).target.Is(Faction.Impostors))
                            {
                                ((GuardianAngel)role).ImpTargetLose();
                            }
                        break;
                    case CustomRPC.PlaguebearerWin:
                        var thePlaguebearerTheRole = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Plaguebearer);
                        ((Plaguebearer)thePlaguebearerTheRole)?.Wins();
                        break;
                    case CustomRPC.PlaguebearerLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Plaguebearer)
                                ((Plaguebearer)role).Loses();
                        break;
                    case CustomRPC.Infect:
                        Role.GetRole<Plaguebearer>(Utils.PlayerById(reader.ReadByte())).InfectedPlayers.Add(reader.ReadByte());
                        break;
                    case CustomRPC.TurnPestilence:
                        Role.GetRole<Plaguebearer>(Utils.PlayerById(reader.ReadByte())).TurnPestilence();
                        break;
                    case CustomRPC.PestilenceWin:
                        var thePestilenceTheRole = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Pestilence);
                        ((Pestilence)thePestilenceTheRole)?.Wins();
                        break;
                    case CustomRPC.PestilenceLose:
                        foreach (var role in Role.AllRoles)
                            if (role.RoleType == RoleEnum.Pestilence)
                                ((Pestilence)role).Loses();
                        break;
                    case CustomRPC.SetImpostor:
                        new Impostor(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetCrewmate:
                        new Crewmate(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SyncCustomSettings:
                        Rpc.ReceiveRpc(reader);
                        break;
                    case CustomRPC.SetAltruist:
                        new Altruist(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetGiant:
                        new Giant(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.AltruistRevive:
                        readByte1 = reader.ReadByte();
                        var altruistPlayer = Utils.PlayerById(readByte1);
                        var altruistRole = Role.GetRole<Altruist>(altruistPlayer);
                        readByte = reader.ReadByte();
                        var theDeadBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in theDeadBodies)
                            if (body.ParentId == readByte)
                            {
                                if (body.ParentId == PlayerControl.LocalPlayer.PlayerId)
                                    Coroutines.Start(Utils.FlashCoroutine(altruistRole.Color,
                                        CustomGameOptions.ReviveDuration, 0.5f));

                                Coroutines.Start(
                                    global::BetterTownOfUs.CrewmateRoles.AltruistMod.Coroutine.AltruistRevive(body,
                                        altruistRole));
                            }

                        break;
                    case CustomRPC.FixAnimation:
                        var player = Utils.PlayerById(reader.ReadByte());
                        player.MyPhysics.ResetMoveState();
                        player.Collider.enabled = true;
                        player.moveable = true;
                        player.NetTransform.enabled = true;
                        break;
                    case CustomRPC.SetButtonBarry:
                        new ButtonBarry(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.BarryButton:
                        var buttonBarry = Utils.PlayerById(reader.ReadByte());

                        if (AmongUsClient.Instance.AmHost)
                        {
                            MeetingRoomManager.Instance.reporter = buttonBarry;
                            MeetingRoomManager.Instance.target = null;
                            AmongUsClient.Instance.DisconnectHandlers.AddUnique(MeetingRoomManager.Instance
                                .Cast<IDisconnectHandler>());
                            if (ShipStatus.Instance.CheckTaskCompletion()) return;

                            DestroyableSingleton<HudManager>.Instance.OpenMeetingRoom(buttonBarry);
                            buttonBarry.RpcStartMeeting(null);
                        }
                        break;
                    case CustomRPC.BaitReport:
                        var baitKiller = Utils.PlayerById(reader.ReadByte());
                        var bait = Utils.PlayerById(reader.ReadByte());
                        baitKiller.ReportDeadBody(bait.Data);
                        break;
                    case CustomRPC.CheckMurder:
                        var murderKiller = Utils.PlayerById(reader.ReadByte());
                        var murderTarget = Utils.PlayerById(reader.ReadByte());
                        murderKiller.CheckMurder(murderTarget);
                        break;
                    case CustomRPC.SetUndertaker:
                        new Undertaker(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.Drag:
                        readByte1 = reader.ReadByte();
                        var dienerPlayer = Utils.PlayerById(readByte1);
                        var dienerRole = Role.GetRole<Undertaker>(dienerPlayer);
                        readByte = reader.ReadByte();
                        var dienerBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in dienerBodies)
                            if (body.ParentId == readByte)
                                dienerRole.CurrentlyDragging = body;

                        break;
                    case CustomRPC.Drop:
                        readByte1 = reader.ReadByte();
                        var v2 = reader.ReadVector2();
                        var v2z = reader.ReadSingle();
                        var dienerPlayer2 = Utils.PlayerById(readByte1);
                        var dienerRole2 = Role.GetRole<Undertaker>(dienerPlayer2);
                        var body2 = dienerRole2.CurrentlyDragging;
                        dienerRole2.CurrentlyDragging = null;

                        body2.transform.position = new Vector3(v2.x, v2.y, v2z);


                        break;
                    case CustomRPC.SetAssassin:
                        new Assassin(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetEraser:
                        new Eraser(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetVeteran:
                        new Veteran(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetTransporter:
                        new Transporter(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetMedium:
                        new Medium(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetMystic:
                        new Mystic(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetUnderdog:
                        new Underdog(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetLycan:
                        new Lycan(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetPhantom:
                        readByte = reader.ReadByte();
                        SetPhantom.WillBePhantom = readByte == byte.MaxValue ? null : Utils.PlayerById(readByte);
                        break;
                    case CustomRPC.PhantomDied:
                        var phantom = Utils.PlayerById(reader.ReadByte());
                        Role.RoleDictionary.Remove(phantom.PlayerId);
                        var phantomRole = new Phantom(phantom);
                        phantomRole.RegenTask();
                        phantom.gameObject.layer = LayerMask.NameToLayer("Players");
                        SetPhantom.RemoveTasks(phantom);
                        SetPhantom.AddCollider(phantomRole);
                        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Haunter))
                        {
                            PlayerControl.LocalPlayer.MyPhysics.ResetMoveState();
                        }
                        System.Console.WriteLine("Become Phantom - Users");
                        break;
                    case CustomRPC.CatchPhantom:
                        var phantomPlayer = Utils.PlayerById(reader.ReadByte());
                        Role.GetRole<Phantom>(phantomPlayer).Caught = true;
                        break;
                    case CustomRPC.PhantomWin:
                        Role.GetRole<Phantom>(Utils.PlayerById(reader.ReadByte())).CompletedTasks = true;
                        break;
                    case CustomRPC.SetHaunter:
                        readByte = reader.ReadByte();
                        SetHaunter.WillBeHaunter = readByte == byte.MaxValue ? null : Utils.PlayerById(readByte);
                        break;
                    case CustomRPC.HaunterDied:
                        var haunter = Utils.PlayerById(reader.ReadByte());
                        Role.RoleDictionary.Remove(haunter.PlayerId);
                        var haunterRole = new Haunter(haunter);
                        haunterRole.RegenTask();
                        haunter.gameObject.layer = LayerMask.NameToLayer("Players");
                        SetHaunter.RemoveTasks(haunter);
                        SetHaunter.AddCollider(haunterRole);
                        if (!PlayerControl.LocalPlayer.Is(RoleEnum.Phantom))
                        {
                            PlayerControl.LocalPlayer.MyPhysics.ResetMoveState();
                        }
                        System.Console.WriteLine("Become Haunter - Users");
                        break;
                    case CustomRPC.CatchHaunter:
                        var haunterPlayer = Utils.PlayerById(reader.ReadByte());
                        Role.GetRole<Haunter>(haunterPlayer).Caught = true;
                        break;
                    case CustomRPC.HaunterFinished:
                        HighlightImpostors.UpdateMeeting(MeetingHud.Instance);
                        break;
                    case CustomRPC.SetTraitor:
                        readByte = reader.ReadByte();
                        SetTraitor.WillBeTraitor = readByte == byte.MaxValue ? null : Utils.PlayerById(readByte);
                        break;
                    case CustomRPC.TraitorSpawn:
                        var traitor = SetTraitor.WillBeTraitor;
                        var oldRole = Role.GetRole(traitor).RoleType;
                        Role.RoleDictionary.Remove(traitor.PlayerId);
                        var traitorRole = new Traitor(traitor);
                        traitorRole.formerRole = oldRole;
                        traitorRole.RegenTask();
                        SetTraitor.TurnImp(traitor);
                        break;
                    case CustomRPC.SetPlaguebearer:
                        new Plaguebearer(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.SetTrapper:
                        new Trapper(Utils.PlayerById(reader.ReadByte()));
                        break;
                    case CustomRPC.AddMayorVoteBank:
                        Role.GetRole<Mayor>(Utils.PlayerById(reader.ReadByte())).VoteBank += reader.ReadInt32();
                        break;
                    case CustomRPC.RemoveAllBodies:
                        var buggedBodies = Object.FindObjectsOfType<DeadBody>();
                        foreach (var body in buggedBodies)
                            body.gameObject.Destroy();
                        break;
                    case CustomRPC.SubmergedFixOxygen:
                        Patches.SubmergedCompatibility.RepairOxygen();
                        break;
                    case CustomRPC.SetPos:
                        var setplayer = Utils.PlayerById(reader.ReadByte());
                        setplayer.transform.position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), setplayer.transform.position.z);
                        break;
                }
            }
        }

        [HarmonyPatch(typeof(RoleManager), nameof(RoleManager.SelectRoles))]
        public static class RpcSetRole
        {
            public static void Postfix()
            {
                var infected = GameData.Instance.AllPlayers.ToArray().Where(o => o.IsImpostor());
                NumImpostors = infected.Count();

                Utils.ShowDeadBodies = false;
                Role.NobodyWins = false;
                Role.SurvOnlyWins = false;
                AssassinKill.HaveFailedGuess = false;
                CrewmateRoles.Clear();
                NeutralNonKillingRoles.Clear();
                NeutralKillingRoles.Clear();
                ImpostorRoles.Clear();
                CrewmateModifiers.Clear();
                BaitModifiers.Clear();
                GlobalModifiers.Clear();
                ButtonModifiers.Clear();
                AssassinModifier.Clear();

                RecordRewind.points.Clear();
                Murder.KilledPlayers.Clear();
                KillButtonTarget.DontRevive = byte.MaxValue;

                var startWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.Start, SendOption.Reliable, -1);
                AmongUsClient.Instance.FinishRpcImmediately(startWriter);


                PhantomOn = Check(CustomGameOptions.PhantomOn);
                HaunterOn = Check(CustomGameOptions.HaunterOn);
                TraitorOn = Check(CustomGameOptions.TraitorOn);

                var supportImpRoles = new List<(Type, CustomRPC, int)>();
                var jesterExecRoles = new List<(Type, CustomRPC, int)>();
                var douseRoles = new List<(Type, CustomRPC, int)>();

                #region Crewmate Roles
                if (CustomGameOptions.MayorOn > 0)
                    CrewmateRoles.Add((typeof(Mayor), CustomRPC.SetMayor, CustomGameOptions.MayorOn));

                if (CustomGameOptions.SheriffOn > 0)
                    CrewmateRoles.Add((typeof(Sheriff), CustomRPC.SetSheriff, CustomGameOptions.SheriffOn));

                if (CustomGameOptions.EngineerOn > 0)
                    CrewmateRoles.Add((typeof(Engineer), CustomRPC.SetEngineer, CustomGameOptions.EngineerOn));

                if (CustomGameOptions.SwapperOn > 0)
                    CrewmateRoles.Add((typeof(Swapper), CustomRPC.SetSwapper, CustomGameOptions.SwapperOn));

                if (CustomGameOptions.InvestigatorOn > 0)
                    CrewmateRoles.Add((typeof(Investigator), CustomRPC.SetInvestigator, CustomGameOptions.InvestigatorOn));

                if (CustomGameOptions.TimeLordOn > 0)
                    CrewmateRoles.Add((typeof(TimeLord), CustomRPC.SetTimeLord, CustomGameOptions.TimeLordOn));

                if (CustomGameOptions.MedicOn > 0)
                    CrewmateRoles.Add((typeof(Medic), CustomRPC.SetMedic, CustomGameOptions.MedicOn));

                if (CustomGameOptions.SeerOn > 0)
                    CrewmateRoles.Add((typeof(Seer), CustomRPC.SetSeer, CustomGameOptions.SeerOn));

                if (CustomGameOptions.SpyOn > 0)
                    CrewmateRoles.Add((typeof(Spy), CustomRPC.SetSpy, CustomGameOptions.SpyOn));

                if (CustomGameOptions.SnitchOn > 0)
                    CrewmateRoles.Add((typeof(Snitch), CustomRPC.SetSnitch, CustomGameOptions.SnitchOn));

                if (CustomGameOptions.AltruistOn > 0)
                    CrewmateRoles.Add((typeof(Altruist), CustomRPC.SetAltruist, CustomGameOptions.AltruistOn));

                if (CustomGameOptions.VeteranOn > 0)
                    CrewmateRoles.Add((typeof(Veteran), CustomRPC.SetVeteran, CustomGameOptions.VeteranOn));

                if (CustomGameOptions.TrackerOn > 0)
                    CrewmateRoles.Add((typeof(Tracker), CustomRPC.SetTracker, CustomGameOptions.TrackerOn));

                if (CustomGameOptions.TransporterOn > 0)
                    CrewmateRoles.Add((typeof(Transporter), CustomRPC.SetTransporter, CustomGameOptions.TransporterOn));

                if (CustomGameOptions.MediumOn > 0)
                    CrewmateRoles.Add((typeof(Medium), CustomRPC.SetMedium, CustomGameOptions.MediumOn));

                if (CustomGameOptions.MysticOn > 0)
                    CrewmateRoles.Add((typeof(Mystic), CustomRPC.SetMystic, CustomGameOptions.MysticOn));

                if (CustomGameOptions.TrapperOn > 0)
                    CrewmateRoles.Add((typeof(Trapper), CustomRPC.SetTrapper, CustomGameOptions.TrapperOn));

                if (CustomGameOptions.DetectiveOn > 0)
                    CrewmateRoles.Add((typeof(Detective), CustomRPC.SetDetective, CustomGameOptions.DetectiveOn));
                #endregion
                #region Neutral Roles
                if (CustomGameOptions.ArsonistOn > 0)
                    douseRoles.Add((typeof(Arsonist), CustomRPC.SetArsonist, CustomGameOptions.ArsonistOn));

                if (CustomGameOptions.JesterOn > 0)
                    jesterExecRoles.Add((typeof(Jester), CustomRPC.SetJester, CustomGameOptions.JesterOn));

                if (CustomGameOptions.CannibalOn > 0)
                    NeutralNonKillingRoles.Add((typeof(Cannibal), CustomRPC.SetCannibal, CustomGameOptions.CannibalOn));

                if (CustomGameOptions.AmnesiacOn > 0)
                    NeutralNonKillingRoles.Add((typeof(Amnesiac), CustomRPC.SetAmnesiac, CustomGameOptions.AmnesiacOn));

                if (CustomGameOptions.ExecutionerOn > 0)
                    jesterExecRoles.Add((typeof(Executioner), CustomRPC.SetExecutioner, CustomGameOptions.ExecutionerOn));

                if (CustomGameOptions.SurvivorOn > 0)
                    NeutralNonKillingRoles.Add((typeof(Survivor), CustomRPC.SetSurvivor, CustomGameOptions.SurvivorOn));

                if (CustomGameOptions.GuardianAngelOn > 0)
                    NeutralNonKillingRoles.Add((typeof(GuardianAngel), CustomRPC.SetGuardianAngel, CustomGameOptions.GuardianAngelOn));

                if (CustomGameOptions.GlitchOn > 0)
                    NeutralKillingRoles.Add((typeof(Glitch), CustomRPC.SetGlitch, CustomGameOptions.GlitchOn));

                if (CustomGameOptions.PlaguebearerOn > 0)
                    douseRoles.Add((typeof(Plaguebearer), CustomRPC.SetPlaguebearer, CustomGameOptions.PlaguebearerOn));

                if (NumImpostors > 1 && CustomGameOptions.WerewolfOn > 0)
                    NeutralKillingRoles.Add((typeof(Werewolf), CustomRPC.SetWerewolf, CustomGameOptions.WerewolfOn));
                #endregion
                #region Impostor Roles
                if (CustomGameOptions.UndertakerOn > 0)
                    supportImpRoles.Add((typeof(Undertaker), CustomRPC.SetUndertaker, CustomGameOptions.UndertakerOn));

                if (NumImpostors > 1 && CustomGameOptions.UnderdogOn > 0)
                    ImpostorRoles.Add((typeof(Underdog), CustomRPC.SetUnderdog, CustomGameOptions.UnderdogOn));

                if (CustomGameOptions.LycanOn > 0)
                    supportImpRoles.Add((typeof(Lycan), CustomRPC.SetLycan, CustomGameOptions.LycanOn));

                if (CustomGameOptions.MorphlingOn > 0)
                    ImpostorRoles.Add((typeof(Morphling), CustomRPC.SetMorphling, CustomGameOptions.MorphlingOn));

                if (CustomGameOptions.BlackmailerOn > 0)
                    supportImpRoles.Add((typeof(Blackmailer), CustomRPC.SetBlackmailer, CustomGameOptions.BlackmailerOn));

                if (CustomGameOptions.MinerOn > 0)
                    supportImpRoles.Add((typeof(Miner), CustomRPC.SetMiner, CustomGameOptions.MinerOn));

                if (CustomGameOptions.SwooperOn > 0)
                    ImpostorRoles.Add((typeof(Swooper), CustomRPC.SetSwooper, CustomGameOptions.SwooperOn));

                if (NumImpostors > 1 && CustomGameOptions.JanitorOn > 0)
                    supportImpRoles.Add((typeof(Janitor), CustomRPC.SetJanitor, CustomGameOptions.JanitorOn));

                if (CustomGameOptions.GrenadierOn > 0)
                    ImpostorRoles.Add((typeof(Grenadier), CustomRPC.SetGrenadier, CustomGameOptions.GrenadierOn));

                if (CustomGameOptions.PoisonerOn > 0)
                    ImpostorRoles.Add((typeof(Poisoner), CustomRPC.SetPoisoner, CustomGameOptions.PoisonerOn));
                #endregion
                #region Crewmate Modifiers
                if (Check(CustomGameOptions.TorchOn))
                    CrewmateModifiers.Add((typeof(Torch), CustomRPC.SetTorch, CustomGameOptions.TorchOn));

                if (Check(CustomGameOptions.DiseasedOn))
                    CrewmateModifiers.Add((typeof(Diseased), CustomRPC.SetDiseased, CustomGameOptions.DiseasedOn));

                if (Check(CustomGameOptions.BaitOn))
                    BaitModifiers.Add((typeof(Bait), CustomRPC.SetBait, CustomGameOptions.BaitOn));

                if (PlayerControl.GameOptions.AnonymousVotes && Check(CustomGameOptions.VoteCounterOn))
                    CrewmateModifiers.Add((typeof(VoteCounter), CustomRPC.SetVoteCounter, CustomGameOptions.VoteCounterOn));
                #endregion
                #region Global Modifiers
                if (Check(CustomGameOptions.TiebreakerOn))
                    GlobalModifiers.Add((typeof(Tiebreaker), CustomRPC.SetTiebreaker, CustomGameOptions.TiebreakerOn));

                if (Check(CustomGameOptions.FlashOn))
                    GlobalModifiers.Add((typeof(Flash), CustomRPC.SetFlash, CustomGameOptions.FlashOn));

                if (Check(CustomGameOptions.BlindOn))
                    GlobalModifiers.Add((typeof(Blind), CustomRPC.SetBlind, CustomGameOptions.BlindOn));

                if (Check(CustomGameOptions.GiantOn))
                    GlobalModifiers.Add((typeof(Giant), CustomRPC.SetGiant, CustomGameOptions.GiantOn));

                if (Check(CustomGameOptions.ButtonBarryOn))
                    ButtonModifiers.Add((typeof(ButtonBarry), CustomRPC.SetButtonBarry, CustomGameOptions.ButtonBarryOn));

                if (Check(CustomGameOptions.LoversOn))
                    GlobalModifiers.Add((typeof(Lover), CustomRPC.SetCouple, CustomGameOptions.LoversOn));
                    
                if (Check(CustomGameOptions.SleuthOn))
                    GlobalModifiers.Add((typeof(Sleuth), CustomRPC.SetSleuth, CustomGameOptions.SleuthOn));
                #endregion
                #region Assassin Modifier
                AssassinModifier.Add((typeof(Assassin), CustomRPC.SetAssassin, 100));
                #endregion
                if (supportImpRoles.Count() > 0) ChooseOne(supportImpRoles);
                if (jesterExecRoles.Count() > 0) ChooseOne(jesterExecRoles);
                if (douseRoles.Count() > 0) ChooseOne(douseRoles);
                GenEachRole(infected.ToList());
            }
        }
    }
}
