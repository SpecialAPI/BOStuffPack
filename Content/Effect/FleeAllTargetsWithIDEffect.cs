using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class FleeAllTargetsWithIDEffect : EffectSO
    {
        public List<string> entityIDs = [];

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (entityIDs == null || entityIDs.Count == 0)
                return false;

            foreach (var t in targets)
            {
                if (!t.HasUnit)
                    continue;

                var u = t.Unit;
                if (!entityIDs.Contains(u.EntityID))
                    continue;

                exitAmount++;
                u.UnitWillFlee();
                CombatManager.Instance.AddSubAction(new FleetingUnitAction(u.ID, u.IsUnitCharacter));
            }

            return exitAmount > 0;
        }
    }
}
