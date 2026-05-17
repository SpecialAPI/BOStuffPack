using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class TriggerOverflowEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            return stats.TryTriggerOverflow();
        }
    }
}
