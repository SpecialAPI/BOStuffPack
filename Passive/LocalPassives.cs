using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Passive
{
    public static class LocalPassives
    {
        public static readonly string MergedDB = "Merged_PA".Prefix();
        public static readonly string MergedID = "Merged".Prefix();
        public static readonly string MergedCountSVDB = "MergedCount_USD".Prefix();
        public static readonly string MergedCountSVID = "MergedCount".Prefix();
        public static BasePassiveAbilitySO Merged;

        public static readonly string ShapeShifterDB = "ShapeShifter_PA".Prefix();
        public static readonly string ShapeShifterID = "ShapeShifter".Prefix();
        public static BasePassiveAbilitySO ShapeShifter;

        public static void Init()
        {
            var mergeCountSV = NewStoredValue<AdvancedStoredValueIntInfo>(MergedCountSVDB, MergedCountSVID)
                .SetColor(StoredValueColor_Negative)
                .SetFormat("Merged Enemies: {0}");
            Merged = NewPassive<MultiCustomTriggerEffectPassive>(MergedDB, MergedID)
            .SetBasicInformation("Merged", "Merged")
            .SetEnemyDescription("This enemy will perform an additional ability for each enemy merged into it.")
            .SetStoredValue(mergeCountSV)
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
                        StoredValue = MergedCountSVID,
                        UseStoredValue = true
                    }
                }
            });

            var shapeshifter = NewPassive<MultiCustomTriggerEffectPassive>(ShapeShifterDB, ShapeShifterID)
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
