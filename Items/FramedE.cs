using BOStuffPack.Conditions.Effector;
using BOStuffPack.StoredValues;
using BOStuffPack.TriggerEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class FramedE
    {
        public static readonly string ID = "FramedE_TW".Prefix();

        public static void Init()
        {
            var name = "Framed E";
            var flav = "\"EEEEE E EE EEEEEEEE EEEE EEEEEE.\"";
            var desc = "This party member can now move 2 times per turn. Upon this party member moving themself to a new postion, move all enemies in the opposite direction.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "FramedE")
                .SetPrice(6)
                .AddToTreasure();

            NewStoredValue<UnitStoreData_BasicSO>(StoredValueIDs.FramedEDB, StoredValueIDs.FramedEID);

            item.SetTriggerEffects(new()
            {
                CharacterMultiSwapTriggerEffect.RestoreSV(StoredValueIDs.FramedEID, 2),
                CharacterMultiSwapTriggerEffect.Refresh(StoredValueIDs.FramedEID, false),

                new()
                {
                    trigger = TriggerCalls.OnSwapTo.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.SwapRight, 0, Targeting.GenerateSlotTarget([4, 3, 2, 1, 0, -1, -2, -3, -4], false))
                    }),
                    conditions = new()
                    {
                        CreateScriptable<CasterSlotIDToIntHolderValueComparisonEffectorCondition>(x => x.comparison = IntComparison.LessThan)
                    }
                },
                new()
                {
                    trigger = TriggerCalls.OnSwapTo.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.SwapLeft, 0, Targeting.GenerateSlotTarget([-4, -3, -2, -1, 0, 1, 2, 3, 4], false))
                    }),
                    conditions = new()
                    {
                        CreateScriptable<CasterSlotIDToIntHolderValueComparisonEffectorCondition>(x => x.comparison = IntComparison.GreaterThan)
                    }
                }
            });
        }
    }
}
