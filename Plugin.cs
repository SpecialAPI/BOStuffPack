using BepInEx;
using BepInEx.Bootstrap;
using BOStuffPack.Items;
using BOStuffPack.Passive;
using BOStuffPack.ReversePatches;
using BOStuffPack.StoredValues;
using Grimoire;
using Steamworks;
using System;
using UnityEngine.Rendering.PostProcessing;

namespace BOStuffPack
{
    [BepInDependency(BrutalAPI.BrutalAPI.GUID)]
    [BepInDependency(PentaclePlugin.MOD_GUID)]
    [BepInDependency(GrimoirePlugin.MOD_GUID)]
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string MOD_GUID        = "157.Items";
        public const string MOD_NAME        = "157 ITEMS";
        public const string MOD_VERSION     = "0.0.2";
        public const string MOD_PREFIX      = "157Items";

        public static readonly Harmony HarmonyInstance   = new(MOD_GUID);
        public static readonly Assembly ModAssembly      = Assembly.GetExecutingAssembly();
        public static readonly ModProfile Profile        = GenerateProfile();

        public static bool ReversePatchesFinished;

        public void Awake()
        {
            HarmonyInstance.PatchAll();

            LocalPassives.Init();

            TheTideTurner.Init();
            BloodyHacksaw.Init(); // reference
            ConjoinedFungi.Init(); // reference
            RipAndTear.Init(); // reference
            SquirrelBomb.Init();
            Survivorship.Init(); // reference
            //LoudPhone.Init(); // reference
            BlankPortrait.Init();
            Potential.Init(); // reference
            NewtonsApple.Init();
            InterdimensionalShapeshifter.Init();
            //WorldShatter.Init();
            //Keyring.Init(); // reference
            Pencil.Init();
            AlmightyBranch.Init();
            Bait.Init();
            DuctTape.Init();
            //UnnamedItem17.Init();
            //PaperCrown.Init(); // reference
            RedButton.Init();
            InstrumentsOfMurder.Init();
            FramedE.Init();
            RedMarker.Init(); // reference
            Electromagnet.Init();
            Bookmark.Init();
            UnnamedItem1.Init();
            //TheHumanCondition.Init();
            BlankBook.Init();
            WrittenBook.Init();
            Blasphemy.Init();
            UnnamedItem31.Init();
            DiceBullets.Init();
            UnnamedItem5.Init();
            UnnamedItem35.Init();
            //UnnamedItem36.Init();
            UnnamedItem37.Init();
            ImaginaryGun.Init();
            UnnamedItem42.Init();
            DirtBlock.Init();
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

        private static ModProfile GenerateProfile() => ProfileManager.RegisterMod(MOD_GUID, "");
    }
}
