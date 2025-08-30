using BOStuffPack.CustomTrigger;
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
                new TriggerEffectAndTriggersInfo()
                {
                    triggers = new()
                    {
                        // A unit got moved
                        CustomTriggers.OnAnyoneMoved,

                        // Misc
                        LocalCustomTriggers.OnAnyoneFleetingEnd,

                        // Pigment-related
                        //TriggerCalls.OnOverflowAdded.ToString(), // maybe
                        TriggerCalls.CanOverflowTrigger.ToString(), // Replace this with OnOverflowTriggered in the future (this will require it getting added to Pentacle)
                        CustomTriggers.OnLuckyPigmentSuccess,

                        // Health-related
                        LocalCustomTriggers.OnAnyoneDirectDamaged,
                        LocalCustomTriggers.OnAnyonesMaxHealthChanged,
                        LocalCustomTriggers.OnAnyoneHealed,

                        // An ability was used
                        TriggerCalls.OnAnyAbilityUsed.ToString(),

                        // Turn finished/started
                        TriggerCalls.OnTurnFinished.ToString(),
                        TriggerCalls.OnTurnStart.ToString(),

                        // Combat start/end (lol)
                        TriggerCalls.OnCombatStart.ToString(),
                        TriggerCalls.OnCombatEnd.ToString(),

                        // An item got consumed
                        TriggerCalls.OnANYItemConsumed.ToString(),

                        // A unit got spawned
                        TriggerCalls.OnAllyHasSpawned.ToString(),
                        TriggerCalls.OnOpponentHasSpawned.ToString(),

                        // A unit has died
                        TriggerCalls.OnAllyHasDied.ToString(),
                        TriggerCalls.OnDeath.ToString(),
                        TriggerCalls.OnOpponentHasDied.ToString(),

                        // Status effect applied/increased
                        CustomTriggers.StatusEffectFirstAppliedToAnyone,
                        CustomTriggers.StatusEffectIncreasedOnAnyone,

                        // For the future: add FieldEffectFirstAppliedAnywhere and FieldEffectIncreasedAnywhere
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
