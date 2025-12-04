using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class TheSquirrel
    {
        public static void Init()
        {
            var name = "The Squirrel";
            var flav = "\"Free Sacrifice\"";
            var desc = "The first wrong pigment used in an ability doesn't count as wrong pigment.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("TheSquirrel_TW")
                .SetBasicInformation(name, flav, desc, "TheSquirrel")
                .SetPrice(7)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new TriggerEffectAndTriggersInfo()
                {
                    triggers = [CustomTriggers.ModifyWrongPigmentAmount, CustomTriggers.ModifyWrongPigmentAmount_UI],
                    doesPopup = false,
                    immediate = true,

                    effect = new ModifyIntegerReferenceTriggerEffect()
                    {
                        Operation = IntOperation.Subtract,
                        Value = 1
                    },
                }
            };
        }
    }
}
