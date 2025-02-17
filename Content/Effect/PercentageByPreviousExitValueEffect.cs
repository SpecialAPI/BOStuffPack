using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class PercentageByPreviousExitValueEffect : EffectSO
    {
        public int baseChance;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            return Random.Range(0, 100) < (PreviousExitValue * entryVariable + baseChance);
        }
    }
}
