using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class ProducePigmentFromStoreDataEnumerableEffect : EffectSO
    {
        public string storedValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            caster.TryGetStoredData(storedValue, out var holder);
            if (holder.m_ObjectData is not IEnumerable<ManaColorSO> pigments)
                return false;

            foreach(var pigment in pigments)
            {
                CombatManager.Instance.ProcessImmediateAction(new ForceAddPigmentAction(pigment, 1, caster.IsUnitCharacter, caster.ID));
                exitAmount++;
            }

            return exitAmount > 0;
        }

        public static EffectSO Create(string storedValue)
        {
            var e = CreateScriptable<ProducePigmentFromStoreDataEnumerableEffect>();
            e.storedValue = storedValue;

            return e;
        }
    }
}
