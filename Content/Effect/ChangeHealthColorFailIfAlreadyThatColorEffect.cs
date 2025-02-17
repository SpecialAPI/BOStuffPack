using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ChangeHealthColorFailIfAlreadyThatColorEffect : EffectSO
    {
        public ManaColorSO healthColor;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var u = t.Unit;

                if(u.HealthColor.pigmentID == healthColor.pigmentID)
                    continue;

                if(!u.ChangeHealthColor(healthColor))
                    continue;

                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
