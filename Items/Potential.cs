using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class Potential
    {
        public static readonly string ID = "Potential_SW".Prefix();

        public static void Init()
        {
            var name = "Potential";
            var flav = "\"You could've been so much more.\"";
            var desc = "This party member is 1 level higher than they would be otherwise.\nOn combat start, inflict 2 Weakened to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "Potential")
                .SetStaticModifiers(RankChangeModifier(1))
                .SetPrice(6)
                .AddToShop();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.ApplyStatus(GrimoireStatusField.Weakened), 2, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
