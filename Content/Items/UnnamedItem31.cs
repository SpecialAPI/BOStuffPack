using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.Content.StoredValues;
using BOStuffPack.CustomTrigger;
using Grimoire.Content.TriggerEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem31
    {
        public static void Init()
        {
            var name = "Unnamed Item 31";
            var flav = "\"WIP\"";
            var desc = "Upon this party member dealing damage, if this is the first time they've dealt that amount of damage this combat, gain 1 coin.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem31_SW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(6)
                .AddToShop();

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
                            x.storedValue = LocalStoredValues.StoredValue_UnnamedItem31._UnitStoreDataID;
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
                            x.storedValue = LocalStoredValues.StoredValue_UnnamedItem31._UnitStoreDataID;
                            x.needsToContain = false;
                        })
                    }
                }
            });
        }
    }
}
