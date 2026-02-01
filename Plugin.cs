using BepInEx;
using BepInEx.Bootstrap;
using BOStuffPack.Content.Items;
using BOStuffPack.Content.Misc;
using BOStuffPack.Content.Passive;
using BOStuffPack.Content.StoredValues;
using BOStuffPack.ReversePatches;
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
        public static ModProfile Profile;
        public static bool ReversePatchesFinished;

        public static PostProcessResources PostProcessResources;

        public void Awake()
        {
            PostProcessResources = Resources.FindObjectsOfTypeAll<PostProcessResources>().FirstOrDefault();
            ModAssembly = Assembly.GetExecutingAssembly();

            Profile = ProfileManager.RegisterMod(MOD_GUID, MOD_PREFIX);
            Bundle = Profile.LoadAssetBundle("bostuffpack");

            AdvancedResourceLoader.LoadFMODBankFromResource("BOStuffPack");
            AdvancedResourceLoader.LoadFMODBankFromResource("BOStuffPack.strings");

            HarmonyInstance = new Harmony(MOD_GUID);
            HarmonyInstance.PatchAll();

            LocalStoredValues.Init();
            LocalPassives.Init();

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
            AlmightyBranch.Init();
            UnnamedItem16.Init();
            MergingStones.Init();
            UnnamedItem17.Init(); // TODO: come up with a name and sprite
            PaperCrown.Init();
            BlueCrown.Init(); // TODO: make a sprite
            InstrumentsOfMurder.Init();
            FramedE.Init();
            RedMarker.Init();
            UnnamedItem11.Init();
            Bookmark.Init();
            UnnamedItem1.Init();
            UnnamedItem3.Init();
            BlankBook.Init();
            WrittenBook.Init();
            Blasphemy.Init();
            UnnamedItem31.Init();
        }

        public void Start()
        {
            var reversePatchTypes = new Type[]
            {
                typeof(AddNewEnemyOutputReversePatch)
            };

            foreach (var t in reversePatchTypes)
                HarmonyInstance.PatchAll(t);

            ReversePatchesFinished = true;
        }
    }
}
