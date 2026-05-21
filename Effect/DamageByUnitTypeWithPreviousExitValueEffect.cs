using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class DamageByUnitTypeWithPreviousExitValueEffect : CustomDamageEffectBase
    {
        public string unitType;

        public override int BaseDamageAmount(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            if (Array.IndexOf(unit.UnitTypes, unitType) >= 0)
                return PreviousExitValue;
            else
                return entryVariable;
        }

        public static EffectSO Create(string unitType, bool indirect = false, bool usePreviousExit = false, bool successOnKill = false, bool ignoreShield = false, string deathType = nameof(DeathType_GameIDs.Basic), string specialDamage = "")
        {
            var e = Create<DamageByUnitTypeWithPreviousExitValueEffect>(indirect, usePreviousExit, successOnKill, ignoreShield, deathType, specialDamage);
            e.unitType = unitType;

            return e;
        }
    }
}
