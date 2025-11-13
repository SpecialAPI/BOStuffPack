using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class UnnamedItem16
    {
        public static void Init()
        {
            var name = "Unnamed Item 16";
            var flav = "\"WIP\"";
            var desc = "At the start of combat, clone a random enemy. If this succeeds, gain coins equal to 20% of the source enemy's health (up to 4) and repeat this effect.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem16_TW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(11)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<RecursiveCloneWithPerformedEffectsEffect>(x =>
                        {
                            x.effects =
                            [
                                Effects.GenerateEffect(CreateScriptable<OutputTargetsCombinedHealthEffect>(), 0, Targeting.Slot_SelfSlot),
                                Effects.GenerateEffect(CreateScriptable<DoIntOperationOnPreviousExitValueEffect>(x => x.operation = IntOperation.Divide), 5),
                                Effects.GenerateEffect(CreateScriptable<ClampPreviousExitValueEffect>(x => { x.min = 1; x.max = 4; })),
                                Effects.GenerateEffect(CreateScriptable<ExtraCurrencyEffect>(x => x._usePreviousExitValue = true), 1)
                            ];
                            x.effectsAreImmediate = false;
                        }))
                    })
                }
            };
        }
    }
}
