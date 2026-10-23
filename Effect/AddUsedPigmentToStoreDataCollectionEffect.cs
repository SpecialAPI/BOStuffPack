using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class AddUsedPigmentToStoreDataCollectionEffect<T> : EffectSO where T : ICollection<ManaColorSO>, new()
    {
        public string storedValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            caster.TryGetStoredData(storedValue, out var holder);
            if (holder.m_ObjectData is not T coll)
                holder.m_ObjectData = coll = new T();

            var usedPigment = caster.Ext().PigmentUsedForAbility;
            if(usedPigment == null)
                return false;

            foreach (var pigment in usedPigment)
            {
                usedPigment.Add(pigment);
                exitAmount++;
            }

            return exitAmount > 0;
        }

        protected static T2 Create<T2>(string storedValue) where T2 : AddUsedPigmentToStoreDataCollectionEffect<T>
        {
            var e = CreateScriptable<T2>();
            e.storedValue = storedValue;

            return e;
        }
    }
}
