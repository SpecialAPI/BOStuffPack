using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ConvertOverflowEffect : EffectSO
    {
        public ManaColorSO pigment;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (stats.overflowMana == null || stats.overflowMana.StoredSlots == null)
                return false;

            var slotsToConvert = new List<int>();

            for(int i = 0; i < stats.overflowMana.StoredSlots.Count; i++)
            {
                if (stats.overflowMana.StoredSlots[i] != pigment)
                    slotsToConvert.Add(i);
            }

            if (slotsToConvert.Count <= 0)
                return false;

            var changedSlots = new List<int>();
            var newPigments = new List<ManaColorSO>();

            var isLimited = entryVariable > 0;

            while(slotsToConvert.Count > 0 && (!isLimited || entryVariable > 0))
            {
                var idx = Random.Range(0, slotsToConvert.Count);

                var slot = slotsToConvert[idx];
                slotsToConvert.RemoveAt(idx);

                changedSlots.Add(slot);
                newPigments.Add(pigment);

                stats.overflowMana.StoredSlots[idx] = pigment;
                exitAmount++;

                if(isLimited)
                    entryVariable--;
            }

            CombatManager.Instance.AddUIAction(new ModifyOverflowUIAction(changedSlots, newPigments));

            return exitAmount > 0;
        }
    }
}
