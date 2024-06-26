using BepInEx;
using BOStuffPack.Content.Items.Treasure;
using System;

namespace BOStuffPack
{
    [BepInDependency(BrutalAPI.BrutalAPI.GUID)]
    [BepInPlugin(GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "SpecialAPI.BOStuffPack";
        public const string PLUGIN_NAME = "SpecialAPI's Stuff Pack";
        public const string PLUGIN_VERSION = "1.0.0";

        public static Harmony HarmonyInstance;
        public static AssetBundle Bundle;

        public void Awake()
        {
            if (LoadedDBsHandler.ModdingDB.IsModDisabled(GUID))
            {
                InitModInfo();

                return;
            }

            ModAssembly = Assembly.GetExecutingAssembly();

            (HarmonyInstance = new Harmony(GUID)).PatchAll();

            if (TryReadFromResource("bostuffpack", out var ba))
                Bundle = AssetBundle.LoadFromMemory(ba);

            LoadFMODBankFromResource("BOStuffPack");
            LoadFMODBankFromResource("BOStuffPack.strings");

            Common.Init();

            CustomStoredValues.Init();
            CustomStatusEffects.Init();
            CustomPassives.Init();

            // Items - Initial beta release
            TheTiderunner.Init();
            BloodyHacksaw.Init();
            JesterHat.Init();
            ConjoinedFungi.Init();
            SchmuckleTicket.Init();
            CombatDice.Init();
            AlmightyBranch.Init();
            RipAndTear.Init();

            // Items - Beta update 1
            PetrifiedMedicine.Init();
            TheSquirrel.Init();
            Survivorship.Init();
            LoudPhone.Init();
            Potential.Init();
            NewtonsApple.Init();
            InterdimensionalShapeshifter.Init();
            StrangeDevice.Init();
            WorldShatter.Init();

            InitModInfo();
        }

        public void Start()
        {
            if (LoadedDBsHandler.ModdingDB.IsModDisabled(GUID))
                return;

            PostStart.OnPostStart();
        }

        public static void InitModInfo()
        {
            var info = ModConfiguration.PrepareAndAddMyModInformation(GUID);

            info.DisplayName = PLUGIN_NAME;

            var descriptionBuilder = new StringBuilder();
            var contentBuilder = new StringBuilder();

            if(Database.Shop.Count > 0)
                contentBuilder.AppendLine($" - {Database.Shop.Count} shop items.");

            if(Database.Treasures.Count > 0)
                contentBuilder.AppendLine($" - {Database.Treasures.Count} treasure items.");

            if(Database.Fish.Count > 0)
                contentBuilder.AppendLine($" - {Database.Fish.Count} fish");

            descriptionBuilder.AppendLine("A content mod focused on adding new items.");

            if (contentBuilder.Length > 0)
            {
                descriptionBuilder.AppendLine();
                descriptionBuilder.AppendLine("Currently adds:");
                descriptionBuilder.AppendLine(contentBuilder.ToString());
            }

            info.Description = descriptionBuilder.ToString();

            var creditsBuilder = new StringBuilder();

            creditsBuilder.AppendLine("Made by SpecialAPI");

            info.Credits = creditsBuilder.ToString();

            var lastItem = Database.AllItems.LastOrDefault();

            if(lastItem != null)
            {
                var itm = GetWearable(lastItem).wearableImage;

                var iconBgs = new string[]
                {
                    //"Unlock_Comedy",
                    "Unlock_Tragedy",
                    //"Unlock_Heaven",
                    //"Unlock_Osman",
                };
                var rng = new System.Random(Database.AllItems.Count * 19521);
                var usedBG = iconBgs[rng.Next(0, iconBgs.Length)];

                var bgTex = LoadTexture(usedBG);

                var iconScale = 1.1f;
                var iconTex = new Texture2D((int)(bgTex.width * iconScale), (int)(bgTex.height * iconScale), TextureFormat.ARGB32, false)
                {
                    anisoLevel = 1,
                    filterMode = 0
                };

                var iconXOffs = (iconTex.width - bgTex.width) / 2;
                var iconYOffs = (iconTex.height - bgTex.height) / 2;

                iconTex.SetPixels(new Color[iconTex.width * iconTex.height]);
                iconTex.SetPixels(iconXOffs, iconYOffs, bgTex.width, bgTex.height, bgTex.GetPixels());

                var itemWidth = Mathf.RoundToInt(bgTex.width * 0.75f);
                var itemHeight = Mathf.RoundToInt(bgTex.height * 0.75f);

                var itemXOffs = (iconTex.width - itemWidth) / 2;
                var itemYOffs = (iconTex.height - itemHeight) / 2;

                var xScale = itm.rect.width / itemWidth;
                var yScale = itm.rect.height / itemHeight;

                for(int x = itemXOffs; x < itemXOffs + itemWidth; x++)
                {
                    for(int y = itemYOffs; y < itemYOffs + itemHeight; y++)
                    {
                        var middleCoord = itm.rect.center;

                        var middleOffsetX = x - (iconTex.width / 2);
                        var middleOffsetY = y - (iconTex.height / 2);

                        var xCoord = (int)(middleCoord.x + middleOffsetX * xScale);
                        var yCoord = (int)(middleCoord.y + middleOffsetY * yScale);

                        var itemPixel = itm.texture.GetPixel(xCoord, yCoord);
                        var iconPixel = iconTex.GetPixel(x, y);

                        var combinedPixel = Color.Lerp(iconPixel, itemPixel, itemPixel.a);
                        combinedPixel.a = Mathf.Clamp01(iconPixel.a + itemPixel.a);

                        iconTex.SetPixel(x, y, combinedPixel);
                    }
                }

                iconTex.Apply();

                var iconSprite = Sprite.Create(iconTex, new(0, 0, iconTex.width, iconTex.height), new(0.5f, 0.5f), 32);

                info.Icon = iconSprite;
                info.ShowIconOnMainMenu = true;
            }
        }
    }
}
