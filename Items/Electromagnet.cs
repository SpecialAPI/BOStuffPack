using BOStuffPack.Effect;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class Electromagnet
    {
        public static void Init()
        {
            var name = "Electromagnet";
            var flav = "\"WIP\"";
            var desc = "Upon this party member moving themself to a new position, move all field effects on the party member side to their new position.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("Electromagnet_SW")
                .SetBasicInformation(name, flav, desc, "ElectromagnetPlaceholder")
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
