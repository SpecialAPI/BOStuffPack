using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class IntValueInStoreDataHashSetCheckEffectorCondition : EffectorConditionSO
    {
        public int intValueIndex;
        public string storedValue;
        public bool needsToContain = true;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(!ValueReferenceTools.TryGetIntHolder(args, out var intHolder))
                return false;

            effector.TryGetStoredData(storedValue, out var holder);
            if (holder.m_ObjectData is not HashSet<int> hs)
                holder.m_ObjectData = hs = [];

            return hs.Contains(intHolder[intValueIndex]) == needsToContain;
        }
    }
}
