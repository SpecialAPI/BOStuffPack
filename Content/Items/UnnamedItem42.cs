using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem42
    {
        public static void Init()
        {
            var name = "Unnamed Item 42";
            var flav = "\"WIP\"";
            var desc = "This party member now deals double damage. Before this party member deals damage, move the target to the left or right.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem42_TW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(13)
                .AddToTreasure();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillApplyDamage.ToString(),
                    doesPopup = true,
                    immediate = true,

                    effect = new PercentageModifierSetterTriggerEffect(100, true)
                },
                new()
                {
                    trigger = LocalCustomTriggers.OnTargetBeingDamaged.ToString(),
                    doesPopup = false,
                    immediate = true,

                    effect = new DodgeDamageModifierSetterTriggerEffect(LocalStoredValues.StoredValue_UnnamedItem42TempDisable._UnitStoreDataID),
                    conditions = new()
                    {
                        CreateScriptable<StoredValueComparisonEffectorCondition>(x =>
                        {
                            x.value = LocalStoredValues.StoredValue_UnnamedItem42TempDisable._UnitStoreDataID;
                            x.compareTo = 0;
                            x.comparison = IntComparison.Equal;
                        })
                    }
                }
            });
        }
    }
}
