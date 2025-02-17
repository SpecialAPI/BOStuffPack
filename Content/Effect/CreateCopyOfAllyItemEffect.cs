using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class CreateCopyOfAllyItemEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach (var t in targets)
            {
                if (!t.HasUnit || !t.Unit.HasUsableItem)
                    continue;

                stats.AddExtraLootAddition(t.Unit.HeldItem.name);
                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
