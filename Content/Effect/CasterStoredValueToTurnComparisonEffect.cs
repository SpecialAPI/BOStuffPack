using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class CasterStoredValueToTurnComparisonEffect : EffectSO
    {
        public string value;
        public IntComparison comparison;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var val = caster.SimpleGetStoredValue(value);
            var turn = CombatManager.Instance._stats.TurnsPassed + 1;

            return CompareInts(val, turn, comparison);
        }
    }
}
