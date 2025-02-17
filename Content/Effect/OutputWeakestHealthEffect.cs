using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class OutputWeakestHealthEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var units = targets.Where(x => x != null && x.HasUnit).Select(x => x.Unit).Where(x => x != null && x.IsAlive);

            if (!units.Any())
                return false;

            return (exitAmount = units.Min(x => x.CurrentHealth)) > 0;
        }
    }
}
