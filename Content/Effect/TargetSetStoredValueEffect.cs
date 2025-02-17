using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class TargetSetStoredValueEffect : EffectSO
    {
        public string storedValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                t.Unit.SimpleSetStoredValue(storedValue, entryVariable);
                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
