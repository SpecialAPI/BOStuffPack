using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class TargetStoredValueComparisonEffectorCondition : EffectorConditionSO
    {
        public int unitValueIndex = 0;
        public string value;
        public IntComparison comparison;
        public int compareTo;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (!ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder) || unitHolder[unitValueIndex] is not IUnit u)
                return false;

            return CompareInts(u.SimpleGetStoredValue(value), compareTo, comparison);
        }

        public static EffectorConditionSO Create(string sv, int compareTo, IntComparison comparison, int unitValueIndex = 0)
        {
            var c = CreateScriptable<TargetStoredValueComparisonEffectorCondition>();
            c.value = sv;
            c.compareTo = compareTo;
            c.comparison = comparison;
            c.unitValueIndex = unitValueIndex;

            return c;
        }
    }
}
