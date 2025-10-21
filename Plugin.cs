using BepInEx;
using BepInEx.Bootstrap;
using BOStuffPack.Content.Items;
using BOStuffPack.Content.Items.Shop;
using BOStuffPack.Content.Items.Trash;
using BOStuffPack.Content.Items.Treasure;
using BOStuffPack.Content.Misc;
using BOStuffPack.Content.Passive;
using BOStuffPack.Content.StoredValues;
using Steamworks;
using System;
using UnityEngine.Rendering.PostProcessing;

namespace BOStuffPack
{
    [BepInDependency(BrutalAPI.BrutalAPI.GUID)]
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string MOD_GUID = "SpecialAPI.BOStuffPack";
        public const string MOD_NAME = "SpecialAPI's Stuff Pack";
        public const string MOD_VERSION = "1.0.0";
        public const string MOD_PREFIX = "BOStuffPack";

        public static Harmony HarmonyInstance;
        public static AssetBundle Bundle;
        public static Assembly ModAssembly;

        public static PostProcessResources PostProcessResources;

        public void Awake()
        {
            PostProcessResources = Resources.FindObjectsOfTypeAll<PostProcessResources>().FirstOrDefault();
            ModAssembly = Assembly.GetExecutingAssembly();

            var profile = ProfileManager.RegisterMod(MOD_GUID, MOD_PREFIX);
            Bundle = profile.LoadAssetBundle("bostuffpack");

            AdvancedResourceLoader.LoadFMODBankFromResource("BOStuffPack");
            AdvancedResourceLoader.LoadFMODBankFromResource("BOStuffPack.strings");

            HarmonyInstance = new Harmony(MOD_GUID);
            HarmonyInstance.PatchAll();

            StuffPackStoredValues.Init();
            StuffPackPassives.Init();

            //var eggEnemies = new List<string>()
            //{
            //    "TaMaGoa_EN"
            //};
            //
            //var eggItems = new List<string>()
            //{
            //    "AsceticEgg_TW",
            //    "EggOfFirmament_TW",
            //    "EggOfIncubus_TW",
            //    "OpulentEgg_TW",
            //    "StillbornEgg_TW",
            //    "EggOfIncubusCracked_ExtraW"
            //};
            //
            //foreach (var enm in eggEnemies)
            //    GetEnemy(enm).AddUnitTypes("Egg");
            //
            //foreach(var itm in eggItems)
            //    GetWearable(itm).AddItemTypes("Egg");

            TheTiderunner.Init();
            BloodyHacksaw.Init();
            ConjoinedFungi.Init();
            SchmuckleTicket.Init();
            CombatDice.Init();
            RipAndTear.Init();
            TheSquirrel.Init();
            Survivorship.Init();
            LoudPhone.Init();
            MagickalBleach.Init();
            Potential.Init();
            NewtonsApple.Init();
            InterdimensionalShapeshifter.Init();
            WorldShatter.Init();
            Keyring.Init();
            Pencil.Init();
            //AlmightyBranch.Init();
            MindHouse.Init();
            MergingStones.Init();
            PetrifyItem.Init(); // TODO: come up with a name and sprite
            PaperCrown.Init();
            BlueCrown.Init(); // TODO: make a sprite
            InstrumentsOfMurder.Init();

            // Trash
            // TODO: add trash pool
            GitHubSmoker.Init();
            GotEgg.Init();
            BlueTrap.Init();
            GreyPills.Init();

            // targeting modify items (hell)
            ////FailRounds.Init();
            ////Eyepatch.Init();
            ////NegativeTeapot.Init();
            ////MusicBox.Init();

            ////CorruptedChunk.Init();
            PurpleBoyle.Init();
        }
    }
}
