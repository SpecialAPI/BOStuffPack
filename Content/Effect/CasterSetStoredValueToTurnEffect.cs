using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class CasterSetStoredValueToTurnEffect : EffectSO
    {
        public string value;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            caster.SimpleSetStoredValue(value, exitAmount = CombatManager.Instance._stats.TurnsPassed + 1 + entryVariable);

            return true;
        }
    }
}
