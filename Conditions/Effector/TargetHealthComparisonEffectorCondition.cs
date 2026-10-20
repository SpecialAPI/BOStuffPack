using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class TargetHealthComparisonEffectorCondition : EffectorConditionSO
    {
        public int compareTo;
        public IntComparison comparison;
        public bool compareAsPercentage;
        public int unitValueIndex;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (!ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder) || unitHolder[unitValueIndex] is not IUnit u)
                return false;

            var comp = compareAsPercentage ?
                u.CalculatePercentualAmount(compareTo) :
                compareTo;

            return CompareInts(u.CurrentHealth, comp, comparison);
        }

        public static EffectorConditionSO Create(int compareTo, IntComparison comparison, bool compareAsPercentage = false, int unitValueIndex = 0)
        {
            var ec = CreateScriptable<TargetHealthComparisonEffectorCondition>();
            ec.compareTo = compareTo;
            ec.comparison = comparison;
            ec.compareAsPercentage = compareAsPercentage;
            ec.unitValueIndex = unitValueIndex;

            return ec;
        }
    }
}
