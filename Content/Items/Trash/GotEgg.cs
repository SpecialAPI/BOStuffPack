using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Trash
{
    public static class GotEgg
    {
        public static void Init()
        {
            var name = "Got Egg?";
            var flav = "\"Use as you please.\"";
            var desc = "This party member deals additional damage equal to the binary logarithm of the amount of eggs, rounded down plus 1. This doesn't happen if there are no eggs.\nThis party member takes 2 less damage when injecting.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("GotEgg_TrashW")
                .SetBasicInformation(name, flav, desc, "GotEgg")
                .SetPrice(65)
                .AddWithoutItemPools(); // TODO: add to trash pool

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillApplyDamage.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new GotEggSetterTriggerEffect()
                },

                // TODO: add second effect
            };
        }
    }
}
