using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class Survivorship
    {
        public static readonly string ID = "Survivorship_TW".Prefix();

        public static void Init()
        {
            var name = "Survivorship";
            var flav = "\"Yet you stand\"";
            var desc = "On combat start, apply 1 Survive to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "Survivorship")
                .SetPrice(5)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.ApplyStatus(CustomStatusEffects.Survive), 1, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
