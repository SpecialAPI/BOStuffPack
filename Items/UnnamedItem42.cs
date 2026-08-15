using BOStuffPack.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class UnnamedItem42
    {
        public static readonly string ID = "UnnamedItem42_TW".Prefix();

        public static void Init()
        {
            var name = "Unnamed Item 42";
            var flav = "\"WIP\"";
            var desc = "This party member now deals double damage. Before this party member deals damage, move the target to the left or right.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(13)
                .AddToTreasure();

            NewStoredValue<UnitStoreData_BasicSO>(StoredValueIDs.UnnamedItem42TempDisableDB, StoredValueIDs.UnnamedItem42TempDisableID);

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

                    effect = new DodgeDamageModifierSetterTriggerEffect(StoredValueIDs.UnnamedItem42TempDisableID),
                    conditions = new()
                    {
                        StoredValueComparisonEffectorCondition.Create(StoredValueIDs.UnnamedItem42TempDisableID, 0, IntComparison.Equal)
                    }
                }
            });
        }
    }
}
