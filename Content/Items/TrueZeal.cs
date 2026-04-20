using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class TrueZeal
    {
        public static void Init()
        {
            var name = "True Zeal";
            var flav = "\"Talia plagiarized my mod.\"";
            var desc = "At the start of combat, destroy all Sacred Shrubs, Royal Pines, Arachnid Aphrodisiacs, Wailing Whistles, Cloth Cocks, Gift Box!es, Burn-Bottle Batches, Coelacanths, Little Clown Dolls, Abused Clown Dolls and True Zeals and instantly kills all Shelly K.s, Formosuses and Cadaver Synods. Produce a shop item for each shop item destroyed, a treasure item for each treasure item destroyed and a fish item for each fish item destroyed. Spawn a permanent party member for each party member killed and an enemy for each enemy killed.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("TrueZeal_ExtraW")
                .SetBasicInformation(name, flav, desc, "TrueZeal")
                .SetPrice(20)
                .AddWithoutItemPools();

            var genTreasure = Effects.GenerateEffect(CreateScriptable<ExtraLootEffect>(x => x._isTreasure = true), 1);
            var genShop = Effects.GenerateEffect(CreateScriptable<ExtraLootEffect>(x => x._isTreasure = false), 1);
            var genFish = Effects.GenerateEffect(ItemUtils.GetLootPool(PoolList_GameIDs.CanOfWorms_WelsCatfish.ToString()), 1);
            var genBleach = Effects.GenerateEffect(CreateScriptable<ExtraLootOptionsEffect>(x => x._itemName = Profile.GetID("OldBleach_ExtraW")));
            var genDirt = Effects.GenerateEffect(CreateScriptable<ExtraLootOptionsEffect>(x => x._itemName = Profile.GetID("DirtBlock_ExtraW")), condition: Effects.ChanceCondition(1));
            var genZeal = Effects.GenerateEffect(CreateScriptable<ExtraLootOptionsEffect>(x => x._itemName = item.name));

            var spawnRandomChar = Effects.GenerateEffect(CreateScriptable<CopyAndSpawnRandomCharacterAnywhereEffect>(x =>
            {
                x._rank = 0;
                x._nameAddition = NameAdditionLocID.NameAdditionNone;
                x._permanentSpawn = true;
            }), 1);
            var spawnSepulchre = Effects.GenerateEffect(CreateScriptable<SpawnEnemyAnywhereEffect>(x =>
            {
                x.enemy = LoadEnemy("Sepulchre_EN");
                x.givesExperience = true;
                x._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            }));

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<DestroyItemsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.itemIDs = ["SacredShrub_TW", "RoyalPine_TW", "ArachnidAphrodisiac_TW"];
                            x.effects = [genTreasure];
                        }), 0, Targeting.AllUnits),
                        Effects.GenerateEffect(CreateScriptable<DestroyItemsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.itemIDs = ["WailingWhistle_SW"];
                            x.effects = [genBleach];
                        }), 0, Targeting.AllUnits),
                        Effects.GenerateEffect(CreateScriptable<DestroyItemsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.itemIDs = ["ClothCock_SW", "GiftBox_SW", "BurnBottleBatch_SW"];
                            x.effects = [genShop];
                        }), 0, Targeting.AllUnits),
                        Effects.GenerateEffect(CreateScriptable<DestroyItemsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.itemIDs = ["Coelacanth_ExtraW"];
                            x.effects = [genFish];
                        }), 0, Targeting.AllUnits),
                        Effects.GenerateEffect(CreateScriptable<DestroyItemsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.itemIDs = ["LittleClownDoll_ExtraW", "AbusedClownDoll_ExtraW"];
                            x.effects = [genDirt];
                        }), 0, Targeting.AllUnits),
                        Effects.GenerateEffect(CreateScriptable<DestroyItemsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.itemIDs = ["TrueZeal_TW", "TrueZeal_SW", "TrueZeal_ExtraW"];
                            x.effects = [genZeal];
                        }), 0, Targeting.AllUnits),
                        Effects.GenerateEffect(CreateScriptable<DestroyItemsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.itemIDs = [item.name];
                            x.effects = [genTreasure];
                        }), 0, Targeting.AllUnits),

                        Effects.GenerateEffect(CreateScriptable<KillTargetsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.entityIDs = [Entity_GameIDs.ShellyK.ToString(), Entity_GameIDs.Formosus.ToString()];
                            x.effects = [spawnRandomChar];
                        }), 0, Targeting.AllUnits),
                        Effects.GenerateEffect(CreateScriptable<KillTargetsWithIDPerformEffectOnSuccessEffect>(x =>
                        {
                            x.entityIDs = ["CadaverSynod_EN"];
                            x.effects = [spawnSepulchre];
                        }), 0, Targeting.AllUnits),

                        Effects.GenerateEffect(CreateScriptable<FleeAllTargetsWithIDEffect>(x => x.entityIDs = [Entity_GameIDs.Gospel.ToString()]), 0, Targeting.AllUnits)
                    })
                }
            });
        }
    }
}
