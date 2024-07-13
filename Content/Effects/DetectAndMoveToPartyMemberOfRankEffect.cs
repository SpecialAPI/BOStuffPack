using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class DetectAndMoveToPartyMemberOfRankEffect : EffectSO
    {
        public int rankTarget;
        public IntComparison rankComparison;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var units = targets.Select(x => x.Unit).Where(x => x != null && x is CharacterCombat cc && CompareInts(cc.ClampedRank, rankTarget, rankComparison)).ToList();

            if (units.Count <= 0)
                return false;

            var nearestGuys = new List<IUnit>();
            var nearestDistance = int.MaxValue;

            foreach(var u in units)
            {
                var dist = caster.DistanceBetween(u);

                if (dist < nearestDistance)
                {
                    nearestDistance = dist;
                    nearestGuys.Clear();
                }

                if(dist <= nearestDistance)
                    nearestGuys.Add(u);
            }

            if (nearestGuys.Count <= 0)
                return false;

            var randomNearestGuy = nearestGuys.Count > 1 ? nearestGuys.RandomElement() : nearestGuys[0];

            var moveLeft = caster.IsRightOf(randomNearestGuy);
            var moveRight = caster.IsLeftOf(randomNearestGuy);

            bool right;

            if (moveLeft && !moveRight)
                right = false;

            else if (moveRight && !moveLeft)
                right = true;

            else
                return true;

            while (caster.TryMoveUnit(right))
            {
                exitAmount++;

                if (units.Any(caster.IsOpposing) || units.All(right ? caster.IsRightOf : caster.IsLeftOf))
                    break;
            }

            return true;
        }
    }
}
