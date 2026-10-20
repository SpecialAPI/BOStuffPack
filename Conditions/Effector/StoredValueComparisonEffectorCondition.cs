using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class StoredValueComparisonEffectorCondition : EffectorConditionSO
    {
        public string storedValue;
        public int compareTo;
        public IntComparison comparison;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (effector is IUnit u)
                return CompareInts(u.SimpleGetStoredValue(storedValue), compareTo, comparison);

            return false;
        }

        public static EffectorConditionSO Create(string storedValue, int compareTo, IntComparison comparison)
        {
            var c = CreateScriptable<StoredValueComparisonEffectorCondition>();
            c.storedValue = storedValue;
            c.compareTo = compareTo;
            c.comparison = comparison;

            return c;
        }
    }
}
