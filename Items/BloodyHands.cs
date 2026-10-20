using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class BloodyHands
    {
        public static readonly string ID = "BloodyHands_TW".Prefix();

        public static void Init()
        {
            var name = "Bloody Hands";
            var flav = "\"Makes it easier to hurt people.\"";
            var desc = "Upon killing an enemy, apply 1 Fury to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
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
                        Effects.GenerateEffect(CommonEffects.ApplyStatus(GrimoireStatusField.Fury), 1, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
