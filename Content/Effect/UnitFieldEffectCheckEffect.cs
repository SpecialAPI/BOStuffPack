using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class UnitFieldEffectCheckEffect : EffectSO
    {
        public string field;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var target in targets)
            {
                if (!target.HasUnit)
                    continue;

                if (!target.Unit.ContainsFieldEffect(field))
                    continue;

                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
