using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class CasterSlotIDToIntHolderValueComparisonEffectorCondition : EffectorConditionSO
    {
        public int intValueIndex;
        public IntComparison comparison;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(!ValueReferenceTools.TryGetIntHolder(args, out var intHolder))
                return false;

            return CompareInts(effector.SlotID, intHolder[intValueIndex], comparison);
        }
    }
}
