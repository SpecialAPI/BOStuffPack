using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class SpecialDamageEffect : CustomDamageEffectBase
    {
        public SpecialDamageInfo damageInfo;

        public override DamageInfo DamageUnit(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, out bool indirect)
        {
            indirect = IsIndirect(unit, target, stats, caster, areTargetSlots, entryVariable);

            var amt = BaseDamageAmount(unit, target, stats, caster, areTargetSlots, entryVariable, indirect);
            amt = ModifyDamageAmount(amt, unit, target, stats, caster, areTargetSlots, entryVariable, indirect);

            var offs = target.TargetOffset(areTargetSlots);
            var ignoresShield = IgnoreShields(unit, target, stats, caster, areTargetSlots, entryVariable, indirect);
            var generatePigment = GeneratePigment(unit, target, stats, caster, areTargetSlots, entryVariable, indirect);

            var killer = indirect ? null : caster;

            return unit.SpecialDamage(amt, killer, damageInfo, deathType, offs, generatePigment, !indirect, ignoresShield, specialDamage);
        }

        public static EffectSO Create(SpecialDamageInfo damageInfo, bool indirect = false, bool usePreviousExit = false, bool successOnKill = false, bool ignoreShield = false, string deathType = nameof(DeathType_GameIDs.Basic), string specialDamage = "")
        {
            var e = Create<SpecialDamageEffect>(indirect, usePreviousExit, successOnKill, ignoreShield, deathType, specialDamage);
            e.damageInfo = damageInfo;

            return e;
        }
    }
}
