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

            if (AdvancedResourceLoader.TryReadFromResource("bostuffpack", out var ba))
                Bundle = AssetBundle.LoadFromMemory(ba);

            var profile = ProfileManager.RegisterMod(MOD_GUID, MOD_PREFIX);
            profile.SetAssetBundle(Bundle);

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
            Nothing.Init();
            PetrifyItem.Init(); // TODO: come up with a name and sprite
            PaperCrown.Init();
            BlueCrown.Init(); // TODO: make a sprite

            // Trash
            // TODO: add trash pool
            GitHubSmoker.Init();
            GotEgg.Init();
            BlueTrap.Init();
            GreyPills.Init();

            NewAbility("CeratophyllumDemersum_A")
                .SetBasicInformation("Ceratophyllum demersum", "Ceratophyllum demersum")
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<ShowSequentialAbilityPopupsEffect>(x =>
                    {
                        x.popups = new()
                        {
                            "From Wikipedia, the free encyclopedia",
                            "Ceratophyllum demersum, commonly known as hornwort",
                            "(a common name shared with the unrelated Anthocerotophyta)",
                            $"rigid hornwort,{"[2]".Superscript()} coontail, or coon's tail,{"[3]".Superscript()}",
                            "is a species of flowering plant in the genus Ceratophyllum.",
                            "It is a submerged, free-floating aquatic plant,",
                            "with a cosmopolitan distribution,",
                            "native to all continents except Antarctica.",
                            $"It is a harmful weed introduced in New Zealand.{"[3]".Superscript()}",
                            "It is also a popular aquarium plant.",
                            "Its genome has been sequencedc",
                            $"to study angiosperm evolution.{"[4]".Superscript()}",
                            "",
                            "",
                            "An aquatic plant, Ceratophyllum demersum has stems",
                            "that reach lengths of 1–3 m (3–10 ft),",
                            "with numerous side shoots",
                            "making a single specimen appear as a large, bushy mass.",
                            "The leaves are produced",
                            "in whorls of six to twelve, each leaf 8–40 mm long,",
                            "simple, or forked into two to eight thread-like segments",
                            "edged with spiny teeth; they are stiff and brittle.",
                            "It is monoecious,",
                            "with separate male and female flowers",
                            "produced on the same plant.",
                            "The flowers are small, 2 mm long,",
                            "with eight or more greenish-brown petals;",
                            "they are produced in the leaf axils.",
                            "The fruit is a small nut 4–5 mm long,",
                            "usually with three spines,",
                            "two basal and one apical, 1–12 mm long.",
                            "Plants with the two basal nut spines very short",
                            "are sometimes distinguished as",
                            "Ceratophyllum demersum var. apiculatum (Cham.) Asch.,",
                            "and those with no basal spines sometimes distinguished as",
                            $"Ceratophyllum demersum var. inerme Gay ex Radcl.-Sm.{"[5]".Superscript()}{"[6]".Superscript()}{"[7]".Superscript()}{"[8]".Superscript()}{"[9]".Superscript()}",
                            "It can form turions:",
                            "buds that sink to the bottom of the water",
                            "and stay there during the winter",
                            "before forming new plants in spring.",
                            "[citation needed]".Superscript(),
                            "",
                            "",
                            "Rigid hornwort can be easily confused with soft hornwort,",
                            "especially when there is young growth",
                            "with less stiff leaves.",
                            "A key feature to look out for",
                            "is the number of times a leaf is branched:",
                            "the leaves are only forked once or twice,",
                            "rather than 3-4 times in soft hornwort.",
                            $"They are also rather more roughly toothed.{"[10]".Superscript()}",
                            "Ceratophyllum demersum grows in lakes,",
                            "ponds, and quiet streams",
                            "with summer water temperatures of 15–30 °C",
                            "[citation needed]".Superscript(),
                            "and a rich nutrient status.",
                            "In North America, it occurs",
                            "in the entire US and Canada,",
                            $"except Newfoundland.{"[11]".Superscript()}",
                            "In Europe, it has been reported",
                            "as far north",
                            $"as at a latitude of 66 degrees in Norway.{"[12]".Superscript()}",
                            "Other reported occurrences include",
                            "China, Siberia (at 66 degrees North),",
                            "Burkina Faso and in the Volta River",
                            "in Ghana (Africa), Vietnam,",
                            $"and New Zealand (introduced).{"[13]".Superscript()}",
                            "Ceratophyllum demersum grows",
                            "in still or very slow-moving water.",
                            "[citation needed]".Superscript(),
                            "",
                            "Hornwort is a declared weed",
                            "under the Tasmanian Weed Management Act 1999",
                            $"in Tasmania, Australia,{"[14]".Superscript()}",
                            "and is classed as an unwanted organism",
                            $"in New Zealand.{"[15]".Superscript()}",
                            "",
                            "",
                            "C. demersum has allelopathic qualities",
                            "as it excretes substances that inhibit",
                            "the growth of phytoplankton",
                            $"and cyanobacteria (blue-green algae).{"[3]".Superscript()}{"[16]".Superscript()}",
                            "Its dense growth can outcompete",
                            "other underwater vegetation,",
                            "leading to loss of biodiversity.",
                            "In New Zealand,",
                            "it has caused problems",
                            $"with hydroelectric power plants.{"[3]".Superscript()}",
                            "",
                            "",
                            "This species is often used",
                            "as a floating freshwater plant",
                            "in both coldwater",
                            "and tropical aquaria.",
                            "Though without roots,",
                            "it may attach itself",
                            "to the substrate or objects in the aquarium.",
                            "Its fluffy, filamentous, bright-green leaves",
                            "provide excellent cover",
                            "for newly hatched fish.",
                            $"It is propagated by cuttings.{"[17]".Superscript()}",
                            "This plant appears to drop all its leaves",
                            "when exposed to products",
                            "designed to kill snails.",
                            "The stems can recover relatively quickly,",
                            "growing new leaves within a few weeks.",
                            "[citation needed]".Superscript(),
                            "",
                            "It is frequently used as a model organism",
                            $"for studies of plant physiology.{"[18]".Superscript()}",
                            "One of the reasons for this",
                            "is that it allows studies on shoot effects",
                            "without influence of a root,",
                            "which often makes",
                            "interpretation of nutrition and toxicity experiments",
                            "difficult in terrestrial plants.",
                            "As it is free floating",
                            "and therefore does not require a solid substrate,",
                            "it has been used successfully",
                            "in the biological life support systems",
                            "\"Aquarack/CEBAS\"",
                            "and",
                            "\"Omegahab\"",
                            $"on space flights.{"[19][20][21]".Superscript()}",
                            "",
                            "Hornwort plants",
                            "or the epiphytes they support",
                            "have been shown to degrade",
                            $"the herbicide atrazine.{"[22]".Superscript()}",
                            "",
                            "",
                            "From Wikipedia",
                            "Article \"Ceratophyllum demersum\"",
                            "https://en.wikipedia.org/wiki/Ceratophyllum_demersum",
                            "Permanent link:",
                            "https://en.wikipedia.org/w/index.php?title=Ceratophyllum_demersum&oldid=1297891477",
                            "Text is available under the",
                            "Creative Commons Attribution-ShareAlike 4.0 License",
                            "https://en.wikipedia.org/wiki/Wikipedia:Text_of_the_Creative_Commons_Attribution-ShareAlike_4.0_International_License"
                        };

                        x.delay = 1.25f;

                        x.doDelayAfterLastPopup = true;
                        x.doDelayBeforeFirstPopup = true;
                    }))
                })
                .AddToCharacterDatabase();

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
