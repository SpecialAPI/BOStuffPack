using BOStuffPack.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using BOStuffPack.Effect;
using BOStuffPack.StoredValues;
using Grimoire.Content.TriggerEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class UnnamedItem31
    {
        public static readonly string ID = "UnnamedItem31_SW".Prefix();
        public static readonly string StoredValueDB = "UnnamedItem31_USD".Prefix();
        public static readonly string StoredValueID = "UnnamedItem31".Prefix();

        public static void Init()
        {
            var name = "Unnamed Item 31";
            var flav = "\"WIP\"";
            var desc = "Upon this party member dealing damage, if this is the first time they've dealt that amount of damage this combat, gain 1 coin.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(8)
                .AddToShop();

            NewStoredValue<IntEnumerableStoredValue>(StoredValueDB, StoredValueID)
                .SetColor(StoredValueColor_Negative).SetFormat("Already dealt: {0}")
                .SetSortOrder(IntEnumerableStoredValue.IntSortOrder.Ascending);

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnAnyoneDamaged,
                    immediate = true,
                    doesPopup = true,

                    effect = new PerformEffectWithIntReferenceEntryTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<AddEntryToStoreDataHashSetEffect>(x =>
                        {
                            x.storedValue = StoredValueID;
                            x.usePreviousExit = true;
                        }), 1),
                        Effects.GenerateEffect(CreateScriptable<ExtraCurrencyEffect>(), 1)
                    }),
                    
                    conditions = new()
                    {
                        CreateScriptable<UnitValueMatchesSenderEffectorCondition>(x => x.unitValueIndex = 0),
                        CreateScriptable<IntValueInStoreDataHashSetCheckEffectorCondition>(x =>
                        {
                            x.intValueIndex = 0;
                            x.storedValue = StoredValueID;
                            x.needsToContain = false;
                        })
                    }
                }
            });
        }
    }
}
