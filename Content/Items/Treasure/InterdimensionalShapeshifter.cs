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

            var item = NewItem<MultiCustomTriggerEffectWearable>("InterdimensionalShapeShifter_TW")
                .SetBasicInformation(name, flav, desc, "InterdimensionalShapeshifter")
                .SetPrice(6)
                .AddToTreasure();

            var shapeshifter = NewPassive<MultiCustomTriggerEffectPassive>("ShapeShifter_PA", "ShapeShifter")
                .SetBasicInformation("Shape-Shifter", "Shapeshifter")
                .SetCharacterDescription("At the start of each turn, unequip this party member's held item and equip a random treasure item. Attempt to trigger that item's on combat start effects.")
                .AddToGlossary("At the start of each turn, unequip this party member's held item and equip a random treasure item. Attempt to trigger that item's on combat start effects.")
                .AddToDatabase();

            shapeshifter.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnTurnStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<EquipRandomTreasureEffect>(), 0, Targeting.Slot_SelfSlot)
                    })
                }
            });

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeforeCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<AddPassiveEffect>(x => x._passiveToAdd = shapeshifter), 0, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
