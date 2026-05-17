using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class StoredValueComparisonEffectorCondition : EffectorConditionSO
    {
        public string value;

        public IntComparison comparison;
        public int compareTo;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (effector is IUnit u)
                return CompareInts(u.SimpleGetStoredValue(value), compareTo, comparison);

            return false;
        }
    }
}
