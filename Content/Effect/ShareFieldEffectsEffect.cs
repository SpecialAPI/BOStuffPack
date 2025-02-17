using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ShareFieldEffectsEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (targets.Length <= 1)
                return false;

            var target = targets[0];

            if(target == null || !stats.combatSlots.TryGetSlot(target.SlotID, target.IsTargetCharacterSlot, out var slot) || slot == null || slot.FieldEffects == null)
                return false;

            var applications = new List<(FieldEffect_SO effect, int amount)>();

            foreach(var e in slot.FieldEffects)
            {
                if(e == null)
                    continue;

                var content = 1;

                if (e is FieldEffect_Holder hold)
                    content = hold.m_ContentMain;

                if(content <= 0)
                    continue;

                if (string.IsNullOrEmpty(e.FieldID) || !LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect(e.FieldID, out var f))
                    continue;

                applications.Add((f, Mathf.Max(Mathf.FloorToInt(content * (float)entryVariable / 100f), 1)));
            }

            if (applications.Count <= 0)
                return false;

            for(int i = 1; i < targets.Length; i++)
            {
                var t = targets[i];

                if (t == null)
                    continue;

                foreach (var (field, amount) in applications)
                {
                    if(stats.combatSlots.ApplyFieldEffect(t.SlotID, t.IsTargetCharacterSlot, field, amount))
                        exitAmount += amount;
                }
            }

            return exitAmount > 0;
        }
    }
}
