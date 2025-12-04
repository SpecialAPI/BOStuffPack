using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class LoudPhone
    {
        public static void Init()
        {
            var name = "LoudPhone";
            var flav = "\"CAW CAW CAW\"";
            var desc = "60% chance to refresh this party member's abilities upon performing an ability. Inflict 1 Weakened to this party member if they get refreshed.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("LoudPhone_TW")
                .SetBasicInformation(name, flav, desc, "LoudPhone")
                .SetPrice(4)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnAbilityUsed.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot),
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = CustomStatusEffects.Weakened), 1, Targeting.Slot_SelfSlot)
                    }),

                    conditions = new()
                    {
                        CreateScriptable<PercentageEffectorCondition>(x => x.triggerPercentage = 60)
                    }
                }
            };
        }
    }
}
