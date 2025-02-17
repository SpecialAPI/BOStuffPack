using BOStuffPack.Content.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Passive
{
    public static class StuffPackPassives
    {
        public static BasePassiveAbilitySO Merged;

        public static void Init()
        {
            Merged = NewPassive<MultiCustomTriggerEffectPassive>("Merged_PA", "Merged")
                .SetBasicInformation("Merged", "Merged")
                .SetEnemyDescription("This enemy will perform an additional ability for each enemy merged into it.")
                .SetStoredValue(StuffPackStoredValues.StoredValue_MergedCount)
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
                            StoredValue = StuffPackStoredValues.StoredValue_MergedCount._UnitStoreDataID,
                            UseStoredValue = true
                        }
                    }
                });
        }
    }
}
