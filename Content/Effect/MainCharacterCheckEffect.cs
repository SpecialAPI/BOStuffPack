using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class MainCharacterCheckEffect : EffectSO
    {
        public bool allTargetsMustBeMainCharacter;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                if (t.Unit is IEffectorChecks checks && checks.IsMainCharacter)
                    exitAmount++;
            }

            if (allTargetsMustBeMainCharacter)
                return exitAmount >= targets.Length;

            else
                return exitAmount > 0;
        }
    }
}
