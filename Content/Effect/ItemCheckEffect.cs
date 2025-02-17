using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ItemCheckEffect : EffectSO
    {
        public string requiredItem;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                if(!t.Unit.HasUsableItem || (!string.IsNullOrEmpty(requiredItem) && t.Unit.HeldItem.name != requiredItem))
                    continue;

                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
