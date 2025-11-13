using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ClampPreviousExitValueEffect : EffectSO
    {
        public int min;
        public int max;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = Mathf.Clamp(PreviousExitValue, min, max);
            return true;
        }
    }
}
