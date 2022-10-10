using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Extensions;
using BetterTownOfUs.CustomOption;
using BetterTownOfUs.Patches;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BetterTownOfUs
{
    [BepInPlugin(Id, "Better Town Of Us", ReleaseVersion)]
    [BepInDependency(ReactorPlugin.Id)]
    [BepInDependency(SubmergedCompatibility.SUBMERGED_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    public class BetterTownOfUs : BasePlugin
    {
        internal static BepInEx.Logging.ManualLogSource Logger;
        public const string Id = "com.slushiegoose.townofusre";
        public static string GetVersion() => typeof(BetterTownOfUs).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        public const string ReleaseVersion = "2.1.16";
        public static System.Version Version = System.Version.Parse(GetVersion().Remove(GetVersion().LastIndexOf(".")));
        public static string Beta = GetVersion().Substring(GetVersion().LastIndexOf(".") + 1);
        public static string DisplayVersion = Beta == "0"
            ? ReleaseVersion
            : ReleaseVersion + "-dev." + Beta;
        
        public static Sprite JanitorClean;
        public static Sprite LycanWolf;
        public static Sprite Camouflage;
        public static Sprite EngineerFix;
        public static Sprite SwapperSwitch;
        public static Sprite SwapperSwitchDisabled;
        public static Sprite Footprint;
        public static Sprite Rewind;
        public static Sprite NormalKill;
        public static Sprite MedicSprite;
        public static Sprite SeerSprite;
        public static Sprite SampleSprite;
        public static Sprite MorphSprite;
        public static Sprite Arrow;
        public static Sprite MineSprite;
        public static Sprite SwoopSprite;
        public static Sprite DouseSprite;
        public static Sprite IgniteSprite;
        public static Sprite ReviveSprite;
        public static Sprite ButtonSprite;
        public static Sprite CycleBackSprite;
        public static Sprite CycleForwardSprite;
        public static Sprite GuessSprite;
        public static Sprite DragSprite;
        public static Sprite DropSprite;
        public static Sprite FlashSprite;
        public static Sprite AlertSprite;
        public static Sprite RememberSprite;
        public static Sprite CannibalSprite;
        public static Sprite TrackSprite;
        public static Sprite PoisonSprite;
        public static Sprite PoisonedSprite;
        public static Sprite TransportSprite;
        public static Sprite MediateSprite;
        public static Sprite SpySprite;
        public static Sprite VestSprite;
        public static Sprite ProtectSprite;
        public static Sprite BlackmailSprite;
        public static Sprite BlackmailLetterSprite;
        public static Sprite BlackmailOverlaySprite;
        public static Sprite LighterSprite;
        public static Sprite DarkerSprite;
        public static Sprite InfectSprite;
        public static Sprite RampageSprite;
        public static Sprite TrapSprite;
        public static Sprite ExamineSprite;
        public static Sprite VoteCount;
        public static Sprite VoteCountDisabled;

        public static Sprite SettingsButtonSprite;

        public static Sprite ToUBanner;
        public static Sprite UpdateBTOUButton;
        public static Sprite UpdateSubmergedButton;
        public static Sprite CrewSettingsButtonSprite;
        public static Sprite NeutralSettingsButtonSprite;
        public static Sprite ImposterSettingsButtonSprite;
        public static Sprite ModifierSettingsButtonSprite;
        public static Sprite HorseEnabledImage;
        public static Sprite HorseDisabledImage;
        public static Sprite EraseToggleSprite;
        public static Sprite AssassinToggleSprite;
        public static Vector3 ButtonPosition { get; private set; } = new Vector3(2.6f, 0.7f, -9f);

        private static DLoadImage _iCallLoadImage;


        private Harmony _harmony;

        public ConfigEntry<string> Ip { get; set; }

        public ConfigEntry<ushort> Port { get; set; }

        public static System.Random Random = new System.Random();

        public override void Load()
        {
            Logger = Log;
            System.Console.WriteLine("000.000.000.000/000000000000000000");

            _harmony = new Harmony(Id);

            Generate.GenerateAll();

            JanitorClean = CreateSprite("BetterTownOfUs.Resources.Janitor.png");
            Camouflage = CreateSprite("BetterTownOfUs.Resources.Camouflage.png");
            EraseToggleSprite = CreateSprite("BetterTownOfUs.Resources.Erase.png");
            AssassinToggleSprite = CreateSprite("BetterTownOfUs.Resources.Assassin.png");
            LycanWolf = CreateSprite("BetterTownOfUs.Resources.Lycan.png");
            EngineerFix = CreateSprite("BetterTownOfUs.Resources.Engineer.png");
            SwapperSwitch = CreateSprite("BetterTownOfUs.Resources.SwapperSwitch.png");
            SwapperSwitchDisabled = CreateSprite("BetterTownOfUs.Resources.SwapperSwitchDisabled.png");
            Footprint = CreateSprite("BetterTownOfUs.Resources.Footprint.png");
            Rewind = CreateSprite("BetterTownOfUs.Resources.Rewind.png");
            NormalKill = CreateSprite("BetterTownOfUs.Resources.NormalKill.png");
            MedicSprite = CreateSprite("BetterTownOfUs.Resources.Medic.png");
            SeerSprite = CreateSprite("BetterTownOfUs.Resources.Seer.png");
            SampleSprite = CreateSprite("BetterTownOfUs.Resources.Sample.png");
            MorphSprite = CreateSprite("BetterTownOfUs.Resources.Morph.png");
            Arrow = CreateSprite("BetterTownOfUs.Resources.Arrow.png");
            MineSprite = CreateSprite("BetterTownOfUs.Resources.Mine.png");
            SwoopSprite = CreateSprite("BetterTownOfUs.Resources.Swoop.png");
            DouseSprite = CreateSprite("BetterTownOfUs.Resources.Douse.png");
            IgniteSprite = CreateSprite("BetterTownOfUs.Resources.Ignite.png");
            ReviveSprite = CreateSprite("BetterTownOfUs.Resources.Revive.png");
            ButtonSprite = CreateSprite("BetterTownOfUs.Resources.Button.png");
            DragSprite = CreateSprite("BetterTownOfUs.Resources.Drag.png");
            DropSprite = CreateSprite("BetterTownOfUs.Resources.Drop.png");
            CycleBackSprite = CreateSprite("BetterTownOfUs.Resources.CycleBack.png");
            CycleForwardSprite = CreateSprite("BetterTownOfUs.Resources.CycleForward.png");
            GuessSprite = CreateSprite("BetterTownOfUs.Resources.Guess.png");
            FlashSprite = CreateSprite("BetterTownOfUs.Resources.Flash.png");
            AlertSprite = CreateSprite("BetterTownOfUs.Resources.Alert.png");
            RememberSprite = CreateSprite("BetterTownOfUs.Resources.Remember.png");
            CannibalSprite = CreateSprite("BetterTownOfUs.Resources.Cannibal.png");
            TrackSprite = CreateSprite("BetterTownOfUs.Resources.Track.png");
            PoisonSprite = CreateSprite("BetterTownOfUs.Resources.Poison.png");
            PoisonedSprite = CreateSprite("BetterTownOfUs.Resources.Poisoned.png");
            TransportSprite = CreateSprite("BetterTownOfUs.Resources.Transport.png");
            MediateSprite = CreateSprite("BetterTownOfUs.Resources.Mediate.png");
            SpySprite = CreateSprite("BetterTownOfUs.Resources.Spy.png");
            VestSprite = CreateSprite("BetterTownOfUs.Resources.Vest.png");
            ProtectSprite = CreateSprite("BetterTownOfUs.Resources.Protect.png");
            BlackmailSprite = CreateSprite("BetterTownOfUs.Resources.Blackmail.png");
            BlackmailLetterSprite = CreateSprite("BetterTownOfUs.Resources.BlackmailLetter.png");
            BlackmailOverlaySprite = CreateSprite("BetterTownOfUs.Resources.BlackmailOverlay.png");
            LighterSprite = CreateSprite("BetterTownOfUs.Resources.Lighter.png");
            DarkerSprite = CreateSprite("BetterTownOfUs.Resources.Darker.png");
            InfectSprite = CreateSprite("BetterTownOfUs.Resources.Infect.png");
            RampageSprite = CreateSprite("BetterTownOfUs.Resources.Rampage.png");
            TrapSprite = CreateSprite("BetterTownOfUs.Resources.Trap.png");
            ExamineSprite = CreateSprite("BetterTownOfUs.Resources.Examine.png");
            VoteCount = CreateSprite("BetterTownOfUs.Resources.VoteCount.png");
            VoteCountDisabled = CreateSprite("BetterTownOfUs.Resources.VoteCountDisabled.png");

            SettingsButtonSprite = CreateSprite("BetterTownOfUs.Resources.SettingsButton.png");
            CrewSettingsButtonSprite = CreateSprite("BetterTownOfUs.Resources.Crewmate.png");
            NeutralSettingsButtonSprite = CreateSprite("BetterTownOfUs.Resources.Neutral.png");
            ImposterSettingsButtonSprite = CreateSprite("BetterTownOfUs.Resources.Impostor.png");
            ModifierSettingsButtonSprite = CreateSprite("BetterTownOfUs.Resources.Modifiers.png");
            ToUBanner = CreateSprite("BetterTownOfUs.Resources.BetterTownOfUsBanner.png");
            UpdateBTOUButton = CreateSprite("BetterTownOfUs.Resources.UpdateBToUButton.png");
            UpdateSubmergedButton = CreateSprite("BetterTownOfUs.Resources.UpdateSubmergedButton.png");

            HorseEnabledImage = CreateSprite("BetterTownOfUs.Resources.HorseOn.png");
            HorseDisabledImage = CreateSprite("BetterTownOfUs.Resources.HorseOff.png");

            PalettePatch.Load();

            // RegisterInIl2CppAttribute.Register();

            Ip = Config.Bind("Custom", "Ipv4 or Hostname", "127.0.0.1");
            Port = Config.Bind("Custom", "Port", (ushort) 22023);
            var defaultRegions = ServerManager.DefaultRegions.ToList();
            var ip = Ip.Value;
            if (Uri.CheckHostName(Ip.Value).ToString() == "Dns")
                foreach (var address in Dns.GetHostAddresses(Ip.Value))
                {
                    if (address.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    ip = address.ToString();
                    break;
                }

            ServerManager.DefaultRegions = defaultRegions.ToArray();

            SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>) ((scene, loadSceneMode) =>
            {
                if (scene.name == "MainMenu") ModManager.Instance.ShowModStamp();
            }));

            _harmony.PatchAll();
            SubmergedCompatibility.Initialize();
            Logger.LogMessage($"Town of Us RE v{GetVersion()} ");
        }

        public static Sprite CreateSprite(string name)
        {
            var pixelsPerUnit = 100f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = GUIExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }

        public static void LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
        {
            _iCallLoadImage ??= IL2CPP.ResolveICall<DLoadImage>("UnityEngine.ImageConversion::LoadImage");
            var il2CPPArray = (Il2CppStructArray<byte>) data;
            _iCallLoadImage.Invoke(tex.Pointer, il2CPPArray.Pointer, markNonReadable);
        }

        private delegate bool DLoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
    }
}
