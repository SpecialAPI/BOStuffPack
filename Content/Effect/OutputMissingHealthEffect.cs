using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class OutputMissingHealthEffect : EffectSO
    {
        public bool max = true;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var u = targets.Where(x => x != null && x.Unit != null).Select(x => x.Unit);

            if (!u.Any())
                return false;

            exitAmount = max ? u.Max(x => x.MaximumHealth - x.CurrentHealth) : u.Min(x => x.MaximumHealth - x.CurrentHealth);
            exitAmount = Mathf.Max(0, exitAmount);

            return exitAmount > 0;
        }
    }
}
