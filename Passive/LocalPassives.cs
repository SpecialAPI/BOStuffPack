using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Passive
{
    public static class LocalPassives
    {
        public static BasePassiveAbilitySO Merged;
        public static BasePassiveAbilitySO ShapeShifter;

        public static void Init()
        {
            Merged = NewPassive<MultiCustomTriggerEffectPassive>(PassiveIDs.MergedDB, PassiveIDs.MergedID)
            .SetBasicInformation("Merged", "Merged")
            .SetEnemyDescription("This enemy will perform an additional ability for each enemy merged into it.")
            .SetStoredValue(LocalStoredValues.MergedCount)
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.AttacksPerTurn.ToString(),
                    immediate = true,
                    doesPopup = false,


                    effect = new ModifyIntegerReferenceTriggerEffect()
                    {
                        Operation = IntOperation.Add,
                        StoredValue = LocalStoredValues.MergedCount._UnitStoreDataID,
                        UseStoredValue = true
                    }
                }
            });

            var shapeshifter = NewPassive<MultiCustomTriggerEffectPassive>(PassiveIDs.ShapeShifterDB, PassiveIDs.ShapeShifterID)
            .SetBasicInformation("Shape-Shifter", "Shapeshifter")
            .SetCharacterDescription("At the start of each turn, unequip this party member's held item and equip a random treasure item. Attempt to trigger that item's on combat start effects.")
            .AddToGlossary("At the start of each turn, unequip this party member's held item and equip a random treasure item. Attempt to trigger that item's on combat start effects.")
            .AddToDatabase()
            .SetTriggerEffects(new()
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
        }
    }
}
