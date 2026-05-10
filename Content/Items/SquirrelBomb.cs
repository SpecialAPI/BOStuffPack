using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class SquirrelBomb
    {
        public static void Init()
        {
            var name = "Squirrel Bomb";
            var flav = "\"Uh oh.\"";
            var desc = "The first wrong pigment used in an ability doesn't count as wrong pigment. Increase incoming wrong pigment damage by 200%.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("SquirrelBomb_TW")
                .SetBasicInformation(name, flav, desc, "SquirrelBomb")
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
                },

                new()
                {
                    trigger = TriggerCalls.OnWillReceiveCostDamage.ToString(),
                    doesPopup = true,
                    immediate = true,

                    effect = new PercentageModifierSetterTriggerEffect(200, true)
                }
            };
        }
    }
}
