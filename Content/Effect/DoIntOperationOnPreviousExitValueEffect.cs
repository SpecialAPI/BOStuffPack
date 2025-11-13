using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class DoIntOperationOnPreviousExitValueEffect : EffectSO
    {
        public IntOperation operation;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = DoOperation(PreviousExitValue, entryVariable, operation);
            return true;
        }
    }
}
