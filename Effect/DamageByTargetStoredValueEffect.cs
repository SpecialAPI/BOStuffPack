using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class DamageByTargetStoredValueEffect : CustomDamageEffectBase
    {
        public string storedValue;

        public override int BaseDamageAmount(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            return unit.SimpleGetStoredValue(storedValue) * entryVariable;
        }

        public static EffectSO Create(string storedValue, bool indirect = false, bool usePreviousExit = false, bool successOnKill = false, bool ignoreShield = false, string deathType = nameof(DeathType_GameIDs.Basic), string specialDamage = "")
        {
            var e = Create<DamageByTargetStoredValueEffect>(indirect, usePreviousExit, successOnKill, ignoreShield, deathType, specialDamage);
            e.storedValue = storedValue;

            return e;
        }
    }
}
