using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class PetrifiedMedicine
    {
        public static void Init()
        {
            var name = "Petrified Medicine";
            var flav = "\"More grey pigment. You probably shouldn't be taking these\"";
            var desc = "At the beginning of combat, produce 3 universal grey pigment.\nGrey pigment can now be normally produced.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "PetrifiedMedicine").AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnFirstTurnStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(null, CreateScriptable<ForceGeneratePigmentColorEffect>(x => x.pigmentColor = Pigments.Grey), 3)
                    })
                },

                new()
                {
                    trigger = CustomEvents.CAN_PRODUCE_PIGMENT_COLOR,
                    doesPopup = false,
                    immediate = true,

                    effect = new ModifyBooleanReferenceTriggerEffect()
                    {
                        value = true,
                        operation = BoolOperation.Set
                    },

                    conditions = new()
                    {
                        CreateScriptable<CanProducePigmentColorCheckCondition>(x => x.pigmentTypeToCheck = "Grey")
                    }
                }
            };
        }
    }
}
