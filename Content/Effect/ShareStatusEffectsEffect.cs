using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ShareStatusEffectsEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (targets.Length <= 1)
                return false;

            var target = targets[0];

            if(target == null || !target.HasUnit || target.Unit is not IStatusEffector effector || effector.StatusEffects == null)
                return false;

            var applications = new List<(StatusEffect_SO effect, int amount)>();

            foreach(var e in effector.StatusEffects)
            {
                if(e == null || e.StatusContent <= 0)
                    continue;

                if (string.IsNullOrEmpty(e.StatusID) || !LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(e.StatusID, out var st))
                    continue;

                applications.Add((st, Mathf.Max(Mathf.FloorToInt(e.StatusContent * (float)entryVariable / 100f), 1)));
            }

            if (applications.Count <= 0)
                return false;

            for(int i = 1; i < targets.Length; i++)
            {
                var t = targets[i];

                if(t == null || !t.HasUnit)
                    continue;

                foreach(var (effect, amount) in applications)
                {
                    if(t.Unit.ApplyStatusEffect(effect, amount, 0))
                        exitAmount += amount;
                }
            }

            return exitAmount > 0;
        }
    }
}
