using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class CasterSetStoredValueToTurnEffect : EffectSO
    {
        public string storedValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            caster.SimpleSetStoredValue(storedValue, exitAmount = CombatManager.Instance._stats.TurnsPassed + 1 + entryVariable);

            return true;
        }

        public static EffectSO Create(string storedValue)
        {
            var e = CreateScriptable<CasterSetStoredValueToTurnEffect>();
            e.storedValue = storedValue;

            return e;
        }
    }
}
