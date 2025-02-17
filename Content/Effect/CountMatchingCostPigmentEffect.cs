using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class CountMatchingCostPigmentEffect : EffectSO
    {
        public ManaColorSO match;
        public PigmentMatchType matchType;

        public IntOperation operation;
        public bool multiplyByEntryVariable;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = DoOperation(PreviousExitValue, caster.Ext().PigmentUsedForAbility.Count(x => x.PigmentMatch(match, matchType)) * (multiplyByEntryVariable ? entryVariable : 1), operation);

            return exitAmount > (multiplyByEntryVariable ? 0 : entryVariable);
        }
    }
}
