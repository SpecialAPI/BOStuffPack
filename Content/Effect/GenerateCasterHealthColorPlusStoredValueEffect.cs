using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class GenerateCasterHealthColorPlusStoredValueEffect : EffectSO
    {
        public string storedValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            caster.GenerateHealthMana(exitAmount = entryVariable += caster.SimpleGetStoredValue(storedValue));

            return true;
        }
    }
}
