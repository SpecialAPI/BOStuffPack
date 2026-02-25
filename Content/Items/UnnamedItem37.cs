using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem37
    {
        public static void Init()
        {
            var name = "Unnamed Item 37";
            var flav = "\"WIP\"";
            var desc = "At the start of each turn, apply 2 Fury to the enemy(ies) with the highest health and 1 Fury to a random party member with the highest health.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem37")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(7)
                .AddToTreasure();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnTurnStart.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = CustomStatusEffects.Fury), 2, Targeting.Spec_Unit_AllOpponents_Strongest),
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x =>
                        {
                            x._Status = CustomStatusEffects.Fury;
                            x._JustOneRandomTarget = true;
                        }), 1, Targeting.Spec_Unit_AllAllies_Strongest),
                    })
                }
            });
        }
    }
}
