using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class UnitFieldEffectCheckEffect : EffectSO
    {
        public string fieldEffect;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var target in targets)
            {
                if (!target.HasUnit)
                    continue;

                if (!target.Unit.ContainsFieldEffect(fieldEffect))
                    continue;

                exitAmount++;
            }

            return exitAmount > 0;
        }

        public static EffectSO Create(string fieldEffect)
        {
            var e = CreateScriptable<UnitFieldEffectCheckEffect>();
            e.fieldEffect = fieldEffect;

            return e;
        }
    }
}
