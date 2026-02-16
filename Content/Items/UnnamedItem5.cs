using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.Content.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem5
    {
        public static void Init()
        {
            var name = "Unnamed Item 5";
            var flav = "\"WIP\"";
            var desc = "This party member can now perform 2 abilities per turn. At the start of combat, double this party member's ability costs.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem5_TW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(11)
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
                        Effects.GenerateEffect(CreateScriptable<MultiplyTargetAbilityCostsEffect>(), 2, Targeting.Slot_SelfSlot)
                    })
                },
                new()
                {
                    trigger = TriggerCalls.OnTurnStart.ToString(),
                    immediate = false,
                    doesPopup = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<CasterStoreValueSetterEffect>(x => x.m_unitStoredDataID = LocalStoredValues.StoredValue_UnnamedItem5._UnitStoreDataID), 1)
                    })
                },
                new()
                {
                    trigger = TriggerCalls.OnAbilityUsed.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(CreateScriptable<CasterStoredValueChangeEffect>(x =>
                        {
                            x.m_unitStoredDataID = LocalStoredValues.StoredValue_UnnamedItem5._UnitStoreDataID;
                            x._minimumValue = 0;
                            x._increase = false;
                        }), 1, condition: Effects.CheckPreviousEffectCondition(true, 1))
                    }),
                    conditions = new()
                    {
                        CreateScriptable<StoredValueComparisonEffectorCondition>(x =>
                        {
                            x.value = LocalStoredValues.StoredValue_UnnamedItem5._UnitStoreDataID;
                            x.compareTo = 0;
                            x.comparison = IntComparison.GreaterThan;
                        })
                    }
                }
            });
        }
    }
}
