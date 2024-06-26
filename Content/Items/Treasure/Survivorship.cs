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

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "Survivorship").AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(Self, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Survive), 1)
                    })
                }
            };
        }
    }
}
