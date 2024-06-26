using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class TheSquirrel
    {
        public static void Init()
        {
            var name = "The Squirrel";
            var flav = "\"Free Sacrifice\"";
            var desc = "The first wrong pigment used in an ability doesn't count as wrong pigment.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "TheSquirrel").AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = CustomEvents.MODIFY_WRONG_PIGMENT_AMOUNT,
                    doesPopup = false,
                    immediate = true,

                    effect = new ModifyIntegerReferenceTriggerEffect()
                    {
                        Operation = IntOperation.Subtract,
                        Value = 1,

                        UseStoredValue = false
                    },

                    conditions = new()
                    {
                        CreateScriptable<AdvancedIntegerReferenceCheckCondition>(x => x.valueCondition = IntCondition.Positive)
                    }
                }
            };
        }
    }
}
