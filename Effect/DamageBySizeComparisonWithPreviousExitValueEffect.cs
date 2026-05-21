using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class DamageBySizeComparisonWithPreviousExitValueEffect : CustomDamageEffectBase
    {
        public int compareTo;
        public IntComparison comparison;

        public override int BaseDamageAmount(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            if (CompareInts(unit.Size, compareTo, comparison))
                return PreviousExitValue;
            else
                return entryVariable;
        }

        public static EffectSO Create(int compareTo, IntComparison comparison, bool indirect = false, bool usePreviousExit = false, bool successOnKill = false, bool ignoreShield = false, string deathType = nameof(DeathType_GameIDs.Basic), string specialDamage = "")
        {
            var e = Create<DamageBySizeComparisonWithPreviousExitValueEffect>(indirect, usePreviousExit, successOnKill, ignoreShield, deathType, specialDamage);
            e.compareTo = compareTo;
            e.comparison = comparison;

            return e;
        }
    }
}
