using BOStuffPack.Content.Conditions.Effector;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class FramedE
    {
        public static void Init()
        {
            var name = "Framed E";
            var flav = "\"EEEEE E EE EEEEEEEE EEEE EEEEEE.\"";
            var desc = "Upon this party member moving themself to a new postion, move all enemies in the opposite direction.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("FramedE_TW")
                .SetBasicInformation(name, flav, desc, "FramedE")
                .SetPrice(6)
                .AddToTreasure();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnSwapTo.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = true), 0, Targeting.GenerateSlotTarget([4, 3, 2, 1, 0, -1, -2, -3, -4], false))
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
                        Effects.GenerateEffect(CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = false), 0, Targeting.GenerateSlotTarget([-4, -3, -2, -1, 0, 1, 2, 3, 4], false))
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
