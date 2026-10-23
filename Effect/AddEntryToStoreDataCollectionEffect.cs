using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public abstract class AddEntryToStoreDataCollectionEffect<T> : EffectSO where T : ICollection<int>, new()
    {
        public string storedValue;
        public bool usePreviousExit;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (usePreviousExit)
                entryVariable *= PreviousExitValue;

            exitAmount = entryVariable;

            caster.TryGetStoredData(storedValue, out var holder);
            if (holder.m_ObjectData is not T coll)
                holder.m_ObjectData = coll = new T();

            coll.Add(entryVariable);
            return true;
        }

        protected static T2 Create<T2>(string storedValue, bool usePreviousExit) where T2 : AddEntryToStoreDataCollectionEffect<T>
        {
            var e = CreateScriptable<T2>();
            e.storedValue = storedValue;
            e.usePreviousExit = usePreviousExit;

            return e;
        }
    }
}
