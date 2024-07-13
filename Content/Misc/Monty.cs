using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UIElements.UIR.Implementation.UIRStylePainter;

namespace BOStuffPack.Content.Misc
{
    public static class Monty
    {
        public static void Init()
        {
            var enm = CreateScriptable<EnemySO>();

            enm.name = "Monty_EN";

            enm._enemyName = "Monty".Colorize(new Color32(175, 42, 182, 255));

            enm.health = 12;
            enm.healthColor = Pigments.Purple;

            enm.priority = Priority.Normal;

            enm.abilitySelector = LoadedDBsHandler.MiscDB.RarityAbilitySelector;
            enm.abilities = new()
            {
                NewAbility("Transpile", "Produces 3 red and 3 blue pigment.\nInserts an Unpleasant Effect into a random party member ability.", null, new()
                {
                    Effect(null, CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Red), 3),
                    Effect(null, CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Blue), 3),

                    Effect(Enemies, CreateScriptable<TranspileEffectIntoAbilityEffect>(x => x.effect = Effect(Self, Damage, 8)))
                }, new()
                {
                    TargetIntent(Self, IntentType_GameIDs.Mana_Generate.ToString(), IntentType_GameIDs.Mana_Generate.ToString()),
                    TargetIntent(Enemies, IntentType_GameIDs.Misc.ToString())
                })
                .WithVisuals(Visuals_CutOffFace, Enemies)
                .Enemy(Rarity.Common, Priority.Normal),

                NewAbility("Incantations", "Apply 99 TargetSwap, 3 Fury and 3 Berserk to the opposing party member.\nApply 3 TargetSwap to the left and right party members", null, new()
                {
                    Effect(Opposing, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = TargetSwap), 99),
                    Effect(Opposing, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Fury), 3),
                    Effect(Opposing, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Berserk), 3),
                    
                    Effect(Relative(false, -1, 1), CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = TargetSwap), 3),
                }, new()
                {
                    TargetIntent(Relative(false, -1, 1), IntentForStatus<TargetSwapStatusEffect>()),
                    TargetIntent(Opposing, IntentForStatus<TargetSwapStatusEffect>(), IntentForStatus<FuryStatusEffect>(), IntentForStatus<BerserkStatusEffect>()),
                })
                .WithVisuals(Visuals_Genesis, Opposing)
                .Enemy(Rarity.Common, Priority.Normal),

                NewAbility("Horrors Beyond Comprehension", "\"I don't get it\"\nDoes nothing.", null, new()
                {
                    Effect(Enemies, CreateScriptable<DetectAndMoveToPartyMemberOfRankEffect>(x =>   { x.rankTarget = 3; x.rankComparison = IntComparison.GreaterThanOrEqual; })),

                    Effect(Opposing, CreateScriptable<AnimationVisualsOnCharacterOfRankEffect>(x => { x.rankTarget = 3; x.rankComparison = IntComparison.GreaterThanOrEqual; x.visuals = Visuals_ComeHome; })),
                    Effect(Opposing, CreateScriptable<DirectDeathToCharacterOfRankEffect>(x =>      { x.rankTarget = 3; x.rankComparison = IntComparison.GreaterThanOrEqual; x.obliterate = true; }))
                }, new()
                {
                    TargetIntent(Self, IntentType_GameIDs.Misc.ToString())
                })
                .Enemy(Rarity.Rare, Priority.Normal)
            };

            enm.passiveAbilities = new()
            {
                Passives.Enfeebled,
                Passives.Skittish
            };

            enm.size = 1;
            enm.unitTypes = [];

            enm.enemyTemplate = Bundle.LoadAsset<GameObject>("MontyEnemy").AddComponent<EnemyInFieldLayout>();
            enm.enemyTemplate.m_Data = enm.enemyTemplate.GetComponent<EnemyInFieldLayout_Data>();

            enm.damageSound = "event:/Monty_Damage";
            enm.deathSound = "event:/Monty_Death";

            enm.enemySprite = LoadSprite("MontyIcon2", new(0.5f, 0f));

            enm.enemyLoot = new([]);
            LoadedDBsHandler.EnemyDB.AddNewEnemy(enm.name, enm);

            EnemyUtils.AddEnemyToSpawnPool(enm, PoolList_GameIDs.Bronzo);
            //EnemyUtils.AddEnemyToHealthSpawnPool(enm, PoolList_GameIDs.Sepulchre);
        }
    }
}
