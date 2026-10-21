using BOStuffPack.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using BOStuffPack.Effect;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class RewardMechanism
    {
        public static readonly string ID = "RewardMechanism_SW".Prefix();
        public static readonly string AlreadyDealtSVID = "RewardMechanismAlreadyDealt".Prefix();

        public static void Init()
        {
            var name = "Reward Mechanism";
            var flav = "\"Good Job!\"";
            var desc = "Upon this party member dealing damage, if this is the first time they've dealt that amount of damage this combat, gain 1 coin.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(8)
                .AddToShop();

            NewStoredValue<IntEnumerableStoredValue>(AlreadyDealtSVID)
                .SetColor(StoredValueColor_Negative).SetFormat("Already dealt: {0}")
                .SetSortOrder(IntEnumerableStoredValue.IntSortOrder.Ascending);

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnTargetDamaged,
                    immediate = true,
                    doesPopup = true,

                    effect = new PerformEffectWithIntReferenceEntryTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<AddEntryToStoreDataHashSetEffect>(x =>
                        {
                            x.storedValue = AlreadyDealtSVID;
                            x.usePreviousExit = true;
                        }), 1),
                        Effects.GenerateEffect(CreateScriptable<ExtraCurrencyEffect>(), 1)
                    }),
                    
                    conditions = new()
                    {
                        CreateScriptable<IntValueInStoreDataEnumerableCheckEffectorCondition>(x =>
                        {
                            x.intValueIndex = 0;
                            x.storedValue = AlreadyDealtSVID;
                            x.needsToContain = false;
                        })
                    }
                }
            });
        }
    }
}
