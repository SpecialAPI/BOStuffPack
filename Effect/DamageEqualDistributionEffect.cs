using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class DamageEqualDistributionEffect : CustomDamageEffectBase
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            ModifyEntryParameters(ref targets, ref areTargetSlots, ref entryVariable);

            var killed = false;
            var indirect = 0;
            var direct = 0;

            var unitTargets = new List<TargetSlotInfo>();
            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                if (!ShouldDamage(t.Unit, t, stats, caster, areTargetSlots, entryVariable))
                    continue;

                unitTargets.Add(t);
            }

            if (unitTargets.Count <= 0)
                return false;

            var remDamage = entryVariable;
            while(unitTargets.Count > 0 && remDamage > 0)
            {
                var dmg = remDamage / unitTargets.Count;
                remDamage -= dmg;
                var t = unitTargets.GetAndRemoveRandomElement();
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

            if (direct > 0)
                caster.DidApplyDamage(direct);

            return IsSuccessful(stats, caster, targets, areTargetSlots, entryVariable, exitAmount, killed, direct, indirect);
        }

        public static EffectSO Create(bool indirect = false, bool usePreviousExit = false, bool successOnKill = false, bool ignoreShield = false, string deathType = nameof(DeathType_GameIDs.Basic), string specialDamage = "")
        {
            var e = Create<DamageEqualDistributionEffect>(indirect, usePreviousExit, successOnKill, ignoreShield, deathType, specialDamage);

            return e;
        }
    }
}
