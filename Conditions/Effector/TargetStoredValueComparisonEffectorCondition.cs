using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class TargetStoredValueComparisonEffectorCondition : EffectorConditionSO
    {
        public string storedValue;
        public int compareTo;
        public IntComparison comparison;
        public int unitValueIndex;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (!ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder) || unitHolder[unitValueIndex] is not IUnit u)
                return false;

            return CompareInts(u.SimpleGetStoredValue(storedValue), compareTo, comparison);
        }

        public static EffectorConditionSO Create(string storedValue, int compareTo, IntComparison comparison, int unitValueIndex = 0)
        {
            var c = CreateScriptable<TargetStoredValueComparisonEffectorCondition>();
            c.storedValue = storedValue;
            c.compareTo = compareTo;
            c.comparison = comparison;
            c.unitValueIndex = unitValueIndex;

            return c;
        }
    }
}
