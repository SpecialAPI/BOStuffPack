using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class AddEntryToStoreDataHashSetEffect : EffectSO
    {
        public string storedValue;
        public bool usePreviousExit;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (usePreviousExit)
                entryVariable *= PreviousExitValue;

            caster.TryGetStoredData(storedValue, out var holder);
            if (holder.m_ObjectData is not HashSet<int> hs)
                holder.m_ObjectData = hs = [];

            hs.Add(entryVariable);
            exitAmount = entryVariable;

            return true;
        }
    }
}
