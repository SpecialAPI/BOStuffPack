using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class DamageByHealthComparisonWithPreviousExitValueEffect : CustomDamageEffectBase
    {
        public int compareTo;
        public IntComparison comparison;
        public bool compareToCasterHealth;

        public override int BaseDamageAmount(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            var compTo = compareTo;
            if (compareToCasterHealth)
                compTo = caster.CurrentHealth;

            if (CompareInts(unit.CurrentHealth, compTo, comparison))
                return PreviousExitValue;
            else
                return entryVariable;
        }

        public static EffectSO Create(int compareTo, IntComparison comparison, bool compareToCasterHealthInstead = false, bool indirect = false, bool usePreviousExit = false, bool successOnKill = false, bool ignoreShield = false, string deathType = nameof(DeathType_GameIDs.Basic), string specialDamage = "")
        {
            var e = Create<DamageByHealthComparisonWithPreviousExitValueEffect>(indirect, usePreviousExit, successOnKill, ignoreShield, deathType, specialDamage);
            e.compareTo = compareTo;
            e.comparison = comparison;
            e.compareToCasterHealth = compareToCasterHealthInstead;

            return e;
        }
    }
}
