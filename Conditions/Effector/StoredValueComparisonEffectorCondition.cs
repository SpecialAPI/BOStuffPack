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

        public static EffectorConditionSO Create(string sv, int compareTo, IntComparison comparison)
        {
            var c = CreateScriptable<StoredValueComparisonEffectorCondition>();
            c.value = sv;
            c.compareTo = compareTo;
            c.comparison = comparison;

            return c;
        }
    }
}
