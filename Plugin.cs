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
    [BepInPlugin(MODGUID, MODNAME, MODVERSION)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string MODGUID = "SpecialAPI.BOStuffPack";
        public const string MODNAME = "SpecialAPI's Stuff Pack";
        public const string MODVERSION = "1.0.0";
        public const string MODPREFIX = "BOStuffPack";

        public static Harmony HarmonyInstance;
        public static AssetBundle Bundle;
        public static Assembly ModAssembly;

        public static PostProcessResources PostProcessResources;

        public void Awake()
        {
            PostProcessResources = Resources.FindObjectsOfTypeAll<PostProcessResources>().FirstOrDefault();
            ModAssembly = Assembly.GetExecutingAssembly();

            if (AdvancedResourceLoader.TryReadFromResource("bostuffpack", out var ba))
                Bundle = AssetBundle.LoadFromMemory(ba);

            var profile = ProfileManager.RegisterMod(MODGUID, MODPREFIX);
            profile.SetAssetBundle(Bundle);

            AdvancedResourceLoader.LoadFMODBankFromResource("BOStuffPack");
            AdvancedResourceLoader.LoadFMODBankFromResource("BOStuffPack.strings");

            HarmonyInstance = new Harmony(MODGUID);
            HarmonyInstance.PatchAll();

            StuffPackStoredValues.Init();
            StuffPackPassives.Init();

            var eggEnemies = new List<string>()
            {
                "TaMaGoa_EN"
            };

            var eggItems = new List<string>()
            {
                "AsceticEgg_TW",
                "EggOfFirmament_TW",
                "EggOfIncubus_TW",
                "OpulentEgg_TW",
                "StillbornEgg_TW",
                "EggOfIncubusCracked_ExtraW"
            };

            foreach (var enm in eggEnemies)
                GetEnemy(enm).unitTypes.Add("Egg");

            foreach(var itm in eggItems)
            {
                var item = GetWearable(itm);
                item._ItemTypeIDs = item._ItemTypeIDs.AddToArray("Egg");
            }

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
            AlmightyBranch.Init(); // TODO: make it work lmao
            MindHouse.Init();
            MergingStones.Init();
            Nothing.Init();
            PetrifyItem.Init(); // TODO: come up with a name and sprite
            PaperCrown.Init(); // TODO: make a sprite
            BlueCrown.Init(); // TODO: make a sprite

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
