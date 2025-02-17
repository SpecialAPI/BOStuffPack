using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class HealRandomTargetBetweenPreviousAndEntry : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var hl = Random.Range(Mathf.Min(entryVariable, PreviousExitValue), Mathf.Max(entryVariable, PreviousExitValue) + 1);
            var tr = targets.Where(x => x.HasUnit && x.Unit.IsAlive && x.Unit.CurrentHealth < x.Unit.MaximumHealth && x.Unit.CanHeal(true, hl)).ToArray().RandomElement();

            if (tr == null)
                return false;

            exitAmount += tr.Unit.Heal(hl, caster, true);

            return exitAmount > 0;
        }
    }
}
