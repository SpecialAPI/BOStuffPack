using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ChangeMaxHealthByPreviousExitValueEffect : EffectSO
    {
        public bool increase = true;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var change = entryVariable * PreviousExitValue;

                if (t.Unit.MaximizeHealth(t.Unit.MaximumHealth + (increase ? change : -change)))
                    exitAmount += change;
            }

            return exitAmount > 0;
        }
    }
}
