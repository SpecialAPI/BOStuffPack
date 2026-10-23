using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class MoveTargetFieldEffectsEffect : EffectSO
    {
        public BaseCombatTargettingSO destinationTargeting;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            var effectsToApply = new List<(FieldEffect_SO feSO, int amount)>();

            foreach(var t in targets)
            {
                if(t == null || !stats.combatSlots.TryGetSlot(t.SlotID, t.IsTargetCharacterSlot, out var slot))
                    continue;

                var fields = slot.FieldEffects.ToArray();
                foreach(var field in fields)
                {
                    if(field == null || field.FieldContent <= 0)
                        continue;

                    if(!LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect(field.FieldID, out var feSO))
                        continue;

                    effectsToApply.Add((feSO, field.FieldContent));
                    slot.TryRemoveFieldEffect(field.FieldID);
                }
            }

            if (effectsToApply.Count <= 0)
                return false;

            var destinationTargets = destinationTargeting.GetTargets(stats.combatSlots, caster.SlotID, caster.IsUnitCharacter);
            foreach(var t in destinationTargets)
            {
                exitAmount += effectsToApply.Count;

                foreach (var (feSO, amount) in effectsToApply)
                    stats.combatSlots.ApplyFieldEffect(t.SlotID, t.IsTargetCharacterSlot, feSO, amount);
            }

            return true;
        }

        public static EffectSO Create(BaseCombatTargettingSO destinationTargeting)
        {
            var e = CreateScriptable<MoveTargetFieldEffectsEffect>();
            e.destinationTargeting = destinationTargeting;

            return e;
        }
    }
}
