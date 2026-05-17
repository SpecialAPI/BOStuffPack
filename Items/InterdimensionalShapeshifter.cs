using BOStuffPack.Effect;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class InterdimensionalShapeshifter
    {
        public static void Init()
        {
            var name = "Inter-Dimensional Shape-Shifter";
            var flav = "\"Some see it as a pawn\"";
            var desc = "At the start of battle, add Shape-Shifter to this party member as a passive.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ItemIDs.InterdimensionalShapeshifter)
                .SetBasicInformation(name, flav, desc, "InterdimensionalShapeshifter")
                .SetPrice(6)
                .AddToTreasure()
                .AddItemTypes(ItemType_GameIDs.Magic.ToString())
                .SetStaticModifiers(ModdedDataModifier(new OverworldPassiveDisplayStaticModifier([LocalPassives.ShapeShifter])));

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeforeCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.AddPassive(LocalPassives.ShapeShifter), 0, Targeting.Slot_SelfSlot)
                    })
                }
            };
        }
    }
}
