using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class StoreDataEnumerableEmptyEffectorCondition : EffectorConditionSO
    {
        public string storedValue;
        public bool needsToBeEmpty;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            effector.TryGetStoredData(storedValue, out var holder);

            var empty = true;
            if (holder.m_ObjectData is IEnumerable e)
            {
                var en = e.GetEnumerator();
                try
                {
                    empty = !en.MoveNext();
                }
                finally
                {
                    if(en is IDisposable disp)
                        disp.Dispose();
                }
            }

            return empty == needsToBeEmpty;
        }

        public static EffectorConditionSO Create(string storedValue, bool needsToBeEmpty)
        {
            var ec = CreateScriptable<StoreDataEnumerableEmptyEffectorCondition>();
            ec.storedValue = storedValue;
            ec.needsToBeEmpty = needsToBeEmpty;

            return ec;
        }
    }
}
