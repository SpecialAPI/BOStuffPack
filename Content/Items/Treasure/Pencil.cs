using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class Pencil
    {
        public static void Init()
        {
            var name = "Pencil";
            var flav = "\"What even is that thing?\"";
            var desc = "Small chance to deal 1 indirect damage to all enemies upon almost anything happening.";

            var item =
                NewItem<MultiCustomTriggerEffectWearable>("Pencil_TW")
                .SetBasicInformation(name, flav, desc, "Pencil")
                .SetPrice(2)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new EffectsAndTriggers()
                {
                    triggers = new()
                    {
                        TriggerCalls.OnDamaged.ToString(),
                        TriggerCalls.OnBeingDamaged.ToString(),
                        TriggerCalls.OnTurnFinished.ToString(),
                        TriggerCalls.OnMoved.ToString(),
                        TriggerCalls.OnDeath.ToString(),
                        TriggerCalls.OnWillBeHealed.ToString(),
                        TriggerCalls.OnDirectDamaged.ToString(),
                        TriggerCalls.OnDirectHealed.ToString(),
                        TriggerCalls.OnAbilityUsed.ToString(),
                        TriggerCalls.OnWillApplyDamage.ToString(),
                        TriggerCalls.OnTurnStart.ToString(),
                        TriggerCalls.CanApplyStatusEffect.ToString(),
                        TriggerCalls.OnSwapTo.ToString(),
                        TriggerCalls.OnSwappedTo.ToString(),
                        TriggerCalls.OnKill.ToString(),
                        TriggerCalls.OnCombatStart.ToString(),
                        TriggerCalls.OnCombatEnd.ToString(),
                        TriggerCalls.OnHealed.ToString(),
                        TriggerCalls.OnDidApplyDamage.ToString(),
                        TriggerCalls.OnAbilityWillBeUsed.ToString(),
                        TriggerCalls.OnFirstTurnStart.ToString(),
                        TriggerCalls.TimelineEndReached.ToString(),
                        TriggerCalls.OnWillReceiveCostDamage.ToString(),
                        TriggerCalls.OnFleeting.ToString(),
                        TriggerCalls.OnBeingHealed.ToString(),
                        TriggerCalls.OnStatusEffectApplied.ToString(),
                        TriggerCalls.OnMaxHealthChanged.ToString(),
                        TriggerCalls.OnOverflowAdded.ToString(),
                        TriggerCalls.OnMiscPlayerTurnStart.ToString(),
                        TriggerCalls.OnMiscCombatStartWithBigEnemies.ToString(),
                        TriggerCalls.OnRoundFinished.ToString(),
                        TriggerCalls.OnStatusEffectContentAdded.ToString(),
                        TriggerCalls.OnAnyAbilityUsed.ToString(),
                        TriggerCalls.OnBeforeCombatStart.ToString(),
                        TriggerCalls.OnWillApplyHeal.ToString(),
                        TriggerCalls.OnKilledButSurvived.ToString(),
                        TriggerCalls.OnItemWillBeConsumed.ToString(),
                        TriggerCalls.OnANYItemConsumed.ToString(),
                        TriggerCalls.OnAllyHasDied.ToString(),
                        TriggerCalls.OnOpponentHasDied.ToString(),
                        TriggerCalls.OnAllyHasSpawned.ToString(),
                        TriggerCalls.OnOpponentHasSpawned.ToString(),
                        TriggerCalls.OnIndirectDamaged.ToString(),
                        TriggerCalls.OnHealthColorChanged.ToString(),
                    },
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<DamageEffect>(x => x._indirect = true), 1, Targeting.Unit_AllOpponents)
                    }),
                    conditions = new()
                    {
                        CreateScriptable<PercentageEffectorCondition>(x => x.triggerPercentage = 15)
                    }
                }
            };
        }
    }
}
