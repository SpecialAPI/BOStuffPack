using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class Potential
    {
        public static void Init()
        {
            var name = "Potential";
            var flav = "\"There is potential\"";
            var desc = "This party member is 1 level higher than they would be otherwise.\nOn combat start, inflict 2 Weakened to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("Potential_TW")
                .SetBasicInformation(name, flav, desc, "Potential")
                .SetStaticModifiers(CreateScriptable<RankChange_Wearable_SMS>(x => x._rankAdditive = 1))
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = CustomStatusEffects.Weakened), 2, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
