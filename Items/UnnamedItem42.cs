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
        public static readonly string DisableSVDB = "UnnamedItem42TempDisable_USD".Prefix();
        public static readonly string DisableSVID = "UnnamedItem42TempDisable".Prefix();

        public static void Init()
        {
            var name = "Unnamed Item 42";
            var flav = "\"WIP\"";
            var desc = "This party member now deals double damage. Before this party member deals damage, move the target to the left or right.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(13)
                .AddToTreasure();

            NewStoredValue<UnitStoreData_BasicSO>(DisableSVDB, DisableSVID);

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

                    effect = new DodgeDamageModifierSetterTriggerEffect(DisableSVID),
                    conditions = new()
                    {
                        StoredValueComparisonEffectorCondition.Create(DisableSVID, 0, IntComparison.Equal)
                    }
                }
            });
        }
    }
}
