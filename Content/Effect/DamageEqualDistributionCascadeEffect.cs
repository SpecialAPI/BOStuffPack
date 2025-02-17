using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class DamageEqualDistributionCascadeEffect : EffectSO
    {
        public bool consistent = true;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (targets.Length <= 0)
                return false;

            var initialTarget = targets[0];

            if (initialTarget == null || !initialTarget.HasUnit)
                return false;

            var firstGuy = initialTarget.Unit;

            var leftTargets = targets.Where(x => x != null && x.Unit != firstGuy && x.SlotID < initialTarget.SlotID).OrderByDescending(x => x.SlotID);
            var leftGuys = new HashSet<IUnit>();

            foreach (var t in leftTargets)
            {
                if (consistent && t.Unit == null)
                    break;

                leftGuys.Add(t.Unit);
            }

            var rightTargets = targets.Where(x => x != null && x.Unit != firstGuy && x.SlotID > initialTarget.SlotID).OrderBy(x => x.SlotID);
            var rightGuys = new HashSet<IUnit>();

            foreach (var t in rightTargets)
            {
                if (consistent && t.Unit == null)
                    break;

                rightGuys.Add(t.Unit);
            }

            var totalUnits = new List<IUnit>() { firstGuy };

            for(int i = 0; i < Mathf.Max(leftGuys.Count, rightGuys.Count); i++)
            {
                if (i < leftGuys.Count)
                    totalUnits.Add(leftGuys.ElementAt(i));

                if(i < rightGuys.Count)
                    totalUnits.Add(rightGuys.ElementAt(i));
            }

            for(int i = 0; i < totalUnits.Count && entryVariable > 0; i++)
            {
                var dmg = entryVariable / (totalUnits.Count - i);
                entryVariable -= dmg;

                var u = totalUnits[i];

                if (u == firstGuy)
                    exitAmount += u.Damage(caster.WillApplyDamage(dmg, u), caster, DeathType_GameIDs.Basic.ToString(), areTargetSlots ? initialTarget.SlotID - u.SlotID : -1).damageAmount;

                else
                    exitAmount += u.Damage(dmg, null, DeathType_GameIDs.Basic.ToString(), -1, false, false, true).damageAmount;
            }

            if (exitAmount > 0)
                caster.DidApplyDamage(exitAmount);

            return exitAmount > 0;
        }
    }
}
