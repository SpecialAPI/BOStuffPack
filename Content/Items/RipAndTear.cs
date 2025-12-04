using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class RipAndTear
    {
        public static void Init()
        {
            var name = "Rip and Tear";
            var flav = "\"Enemies have a 20% cha- oh wait, wrong game\"";
            var desc = "Upon killing an enemy, apply 2 Fury to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("RipAndTear_TW")
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
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = CustomStatusEffects.Fury), 2, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
