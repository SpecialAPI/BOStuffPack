using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class TheHumanCondition
    {
        public static void Init()
        {
            var name = "The Human Condition";
            var flav = "\"What it means to be human.\"";
            var desc = "At the start of combat, add Reborn to all enemies as a passive.\nThis item has no effect in boss encounters.";

            var evilReborn = NewPassive<MultiCustomTriggerEffectPassive>("EvilReborn_PA", PassiveType_GameIDs.Reborn.ToString())
                .SetBasicInformation("Reborn", Passives.RebornToInHerImage.passiveIcon)
                .SetEnemyDescription("Upon receiving direct damage, render this enemy female and reroll all of its abilities.")
                .AddToDatabase();

            evilReborn.SetConnectionEffects(new()
            {
                new()
                {
                    immediate = true,
                    doesPopup = true,

                    effect = null
                }
            });
            evilReborn.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnDirectDamaged.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<CasterTransformationEffect>(x =>
                        {
                            x._enemyTransformation = LoadEnemy("InHerImage_EN");
                            x._maintainMaxHealth = true;
                            x._maintainTimelineAbilities = true;
                            x._fullyHeal = false;
                        })),
                        Effects.GenerateEffect(CreateScriptable<ReRollTargetTimelineAbilityEffect>(), 157157157, Targeting.Slot_SelfSlot)
                    }),
                    conditions = [CreateScriptable<IsAliveEffectorCondition>(x => x.checkByCurrentHealth = true)]
                }
            });

            var item = NewItem<MultiCustomTriggerEffectWearable>("TheHumanCondition_TW")
                .SetBasicInformation(name, flav, desc, "TheHumanCondition")
                .SetPrice(15)
                .AddToTreasure();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<AddPassiveEffect>(x => x._passiveToAdd = evilReborn), 0, Targeting.Unit_AllOpponents)
                    }),
                    conditions = [CreateScriptable<CheckBundleDifficultyEffectorCondition>(x =>
                    {
                        x._bundleDifficulty = BundleDifficulty.Boss;
                        x._isEqual = false;
                    })]
                }
            });
        }
    }
}
