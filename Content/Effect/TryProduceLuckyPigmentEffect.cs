using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class TryProduceLuckyPigmentEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            for(var i = 0; i < entryVariable; i++)
                exitAmount += stats.TriggerLuckyPigment();

            return exitAmount > 0;
        }
    }
}
