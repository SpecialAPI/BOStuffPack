using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class CheckOverflowEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (stats.overflowMana == null)
                return false;

            exitAmount = stats.overflowMana.OverflowManaAmount;

            return exitAmount > 0;
        }
    }
}
