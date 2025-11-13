using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class OutputTargetsCombinedHealthEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                exitAmount += t.Unit.CurrentHealth;
            }

            return exitAmount > 0;
        }
    }
}
