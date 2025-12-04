using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem11
    {
        public static void Init()
        {
            var name = "Unnamed Item 11";
            var flav = "\"WIP\"";
            var desc = "Upon this party member moving themself to a new position, move all field effects on the party member side to their new position.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem11_SW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(6)
                .AddToShop();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnSwapTo.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<MoveTargetFieldEffectsToCasterFirstSlotEffect>(), 0, Targeting.GenerateSlotTarget([-4, -3, -2, -1, 1, 2, 3, 4], true))
                    })
                }
            });
        }
    }
}
