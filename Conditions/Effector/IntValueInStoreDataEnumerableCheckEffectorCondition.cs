using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class IntValueInStoreDataEnumerableCheckEffectorCondition : EffectorConditionSO
    {
        public string storedValue;
        public bool needsToContain;
        public int intValueIndex;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(!ValueReferenceTools.TryGetIntHolder(args, out var intHolder))
                return false;

            effector.TryGetStoredData(storedValue, out var holder);

            var contains = false;
            if (holder.m_ObjectData is IEnumerable<int> e)
                contains = e.Contains(intHolder[intValueIndex]);

            return contains == needsToContain;
        }

        public static EffectorConditionSO Create(string storedValue, bool needsToContain, int intValueIndex = 0)
        {
            var ec = CreateScriptable<IntValueInStoreDataEnumerableCheckEffectorCondition>();
            ec.storedValue = storedValue;
            ec.needsToContain = needsToContain;
            ec.intValueIndex = intValueIndex;

            return ec;
        }
    }
}
