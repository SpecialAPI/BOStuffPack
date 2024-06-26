using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class HealRandomTargetBetweenPreviousAndEntry : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var hl = Random.Range(Mathf.Min(entryVariable, PreviousExitValue), Mathf.Max(entryVariable, PreviousExitValue) + 1);
            var tr = targets.Where(x => x.HasUnit && x.Unit.IsAlive && x.Unit.CurrentHealth < x.Unit.MaximumHealth && x.Unit.CanHeal(true, hl)).RandomElement();

            if (tr == null)
                return false;

            exitAmount += tr.Unit.Heal(hl, true);

            return exitAmount > 0;
        }
    }
}
