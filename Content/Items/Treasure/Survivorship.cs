using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class Survivorship
    {
        public static void Init()
        {
            var name = "Survivorship";
            var flav = "\"Yet you stand\"";
            var desc = "On combat start, apply 1 Survive to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("Survivorship_TW")
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
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = CustomStatusEffects.Survive), 1, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
