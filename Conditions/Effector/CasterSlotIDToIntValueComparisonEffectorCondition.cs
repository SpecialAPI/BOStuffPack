using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class CasterSlotIDToIntValueComparisonEffectorCondition : EffectorConditionSO
    {
        public IntComparison comparison;
        public int intValueIndex;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(!ValueReferenceTools.TryGetIntHolder(args, out var intHolder))
                return false;

            return CompareInts(effector.SlotID, intHolder[intValueIndex], comparison);
        }

        public static EffectorConditionSO Create(IntComparison comparison, int intValueIndex = 0)
        {
            var ec = CreateScriptable<CasterSlotIDToIntValueComparisonEffectorCondition>();
            ec.comparison = comparison;
            ec.intValueIndex = intValueIndex;

            return ec;
        }
    }
}
