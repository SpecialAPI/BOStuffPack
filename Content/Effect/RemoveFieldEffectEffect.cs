using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class RemoveFieldEffectEffect : EffectSO
    {
        public FieldEffect_SO field;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (field == null)
                return false;

            foreach(var t in targets)
            {
                if(t == null || t.SlotID < 0)
                    continue;

                var slots = t.IsTargetCharacterSlot ? stats.combatSlots.CharacterSlots : stats.combatSlots.EnemySlots;

                if (slots == null || t.SlotID >= slots.Length)
                    continue;

                var slot = slots[t.SlotID];

                if (slot.RemoveFieldEffect(field.FieldID))
                    exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
