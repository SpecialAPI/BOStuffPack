using BOStuffPack.Conditions.Effector;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class ColorSwitch
    {
        public static readonly string ID = "ColorSwitch_TW".Prefix();

        public static void Init()
        {
            var name = "Color Switch";
            var flav = "\"Beep Boop.\"";
            var desc = "After this party member performs an ability, turn all red pigment in the bar into random non-red pigment and all non-red pigment into red pigment.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "ColorSwitch")
                .SetPrice(8)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnAbilityUsed.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(MultiPigmentRandomizationEffect.Create(new()
                        {
                            [Pigments.Red] = [Pigments.Yellow, Pigments.Blue, Pigments.Purple]
                        }, [Pigments.Red]))
                    }),
                }
            };
        }
    }
}
