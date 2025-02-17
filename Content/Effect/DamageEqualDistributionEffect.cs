using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class DamageEqualDistributionEffect : EffectSO
    {
        public bool indirect;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var unitTargets = new List<TargetSlotInfo>();

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                unitTargets.Add(t);
            }

            if (unitTargets.Count <= 0)
                return false;

            while(unitTargets.Count > 0 && entryVariable > 0)
            {
                var dmg = entryVariable / unitTargets.Count;
                entryVariable -= dmg;

                var idx = Random.Range(0, unitTargets.Count);

                var t = unitTargets[idx];
                unitTargets.RemoveAt(idx);

                var offs = areTargetSlots ? t.SlotID - t.Unit.SlotID : -1;

                if (indirect)
                    exitAmount += t.Unit.Damage(dmg, null, DeathType_GameIDs.Basic.ToString(), offs, false, false, true).damageAmount;

                else
                    exitAmount += t.Unit.Damage(caster.WillApplyDamage(dmg, t.Unit), caster, DeathType_GameIDs.Basic.ToString(), offs).damageAmount;
            }

            if(exitAmount > 0 && !indirect)
                caster.DidApplyDamage(exitAmount);

            return exitAmount > 0;
        }
    }
}
