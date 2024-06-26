using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class LoudPhone
    {
        public static void Init()
        {
            var name = "LoudPhone";
            var flav = "\"CAW CAW CAW\"";
            var desc = "60% chance to refresh this party member's abilities upon performing an ability. Inflict 1 Weakened to this party member if they get refreshed.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "LoudPhone").AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnAbilityUsed.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(Self, CreateScriptable<RefreshAbilityUseEffect>()),
                        Effect(Self, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Weakened), 1)
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
