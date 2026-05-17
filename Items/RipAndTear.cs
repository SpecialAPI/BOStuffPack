using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class RipAndTear
    {
        public static void Init()
        {
            var name = "Rip and Tear";
            var flav = "\"Enemies have a 20% cha- oh wait, wrong game\"";
            var desc = "Upon killing an enemy, apply 2 Fury to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ItemIDs.RipAndTear)
                .SetBasicInformation(name, flav, desc, "RipAndTear")
                .SetPrice(10)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnKill.ToString(),
                    doesPopup = true,
                    
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.ApplyStatus(CustomStatusEffects.Fury), 2, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
