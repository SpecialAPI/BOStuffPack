using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class RedMarker
    {
        public static void Init()
        {
            var name = "Red Marker";
            var flav = "\"I can tell you're trying to say something about capitalism, but do it with more loops.\"";
            var desc = "Upon this party member moving themself to a new position, inflict 2 Linked to the opposing enemy.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ItemIDs.RedMarker)
                .SetBasicInformation(name, flav, desc, "RedMarker")
                .SetPrice(11)
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
                        Effects.GenerateEffect(CommonEffects.ApplyLinked, 2, Targeting.Slot_Front)
                    })
                }
            });
        }
    }
}
