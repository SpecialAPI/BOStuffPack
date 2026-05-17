using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class UnnamedItem35
    {
        public static void Init()
        {
            var name = "Unnamed Item 35";
            var flav = "\"WIP\"";
            var desc = "At the start of combat, apply 1 Fury to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ItemIDs.UnnamedItem35)
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(1)
                .AddToTreasure();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,
                    
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.ApplyStatus(CustomStatusEffects.Fury), 1, Targeting.Slot_SelfSlot)
                    })
                }
            });
        }
    }
}
