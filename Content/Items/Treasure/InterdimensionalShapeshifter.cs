using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class InterdimensionalShapeshifter
    {
        public static void Init()
        {
            var name = "Inter-Dimensional Shape-Shifter";
            var flav = "\"Some see it as a pawn\"";
            var desc = "At the start of battle, add Shape-Shifter to this party member as a passive.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "InterdimensionalShapeshifter").AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeforeCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(Self, CreateScriptable<AddPassiveEffect>(x => x._passiveToAdd = Shapeshifter))
                    })
                }
            };
        }
    }
}
