using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public abstract class DamageEffectBase : EffectSO
    {
        public string specialDamage = string.Empty;
        public string deathType = DeathType_GameIDs.Basic.ToString();
        public bool usePreviousExitValue;
        public bool ignoreShield;
        public bool indirect;
        public bool returnKillAsSuccess;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var killed = false;
            var indirect = 0;
            var direct = 0;

            foreach(var t in targets)
            {
                if(!t.HasUnit)
                    continue;

                var u = t.Unit;

                if (!ShouldDamage(u, t, stats, caster, areTargetSlots, entryVariable))
                    continue;

                var dmgInfo = DamageUnit(u, t, stats, caster, areTargetSlots, entryVariable, out var isIndirect);

                exitAmount += dmgInfo.damageAmount;
                killed |= dmgInfo.beenKilled;

                if (isIndirect)
                    indirect += dmgInfo.damageAmount;
                else
                    direct += dmgInfo.damageAmount;
            }

            if(direct > 0)
                caster.DidApplyDamage(direct);

            return exitAmount > 0;
        }

        public virtual bool ShouldDamage(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable)
        {
            return true;
        }

        public virtual bool IsSuccessful(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, int exitAmount, bool killed, int directDamage, int indirectDamage)
        {
            if (returnKillAsSuccess)
                return killed;
            else
                return exitAmount > 0;
        }

        public virtual DamageInfo DamageUnit(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, out bool indirect)
        {
            indirect = IsIndirect(unit, target, stats, caster, areTargetSlots, entryVariable);

            var amt = BaseDamageAmount(unit, target, stats, caster, areTargetSlots, entryVariable, indirect);
            amt = ModifyDamageAmount(amt, unit, target, stats, caster, areTargetSlots, entryVariable, indirect);

            var offs = target.TargetOffset(areTargetSlots);
            var ignoresShield = IgnoreShields(unit, target, stats, caster, areTargetSlots, entryVariable, indirect);
            var generatePigment = GeneratePigment(unit, target, stats, caster, areTargetSlots, entryVariable, indirect);

            return unit.Damage(amt, indirect ? null : caster, deathType, offs, generatePigment, indirect, ignoresShield, specialDamage);
        }

        public virtual bool IsIndirect(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable)
        {
            return indirect;
        }

        public virtual int BaseDamageAmount(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            return entryVariable;
        }

        public virtual int ModifyDamageAmount(int amount, IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            return indirect ? amount : caster.WillApplyDamage(amount, unit);
        }

        public virtual bool IgnoreShields(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            return indirect || ignoreShield;
        }

        public virtual bool GeneratePigment(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            return !indirect;
        }
    }
}
