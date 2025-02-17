using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class CheckDuplicateEnemiesEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (targets.Length <= 1)
                return false;

            EnemySO en = null;
            var alreadyChecked = new List<int>();

            foreach (var target in targets)
            {
                if (target == null || !target.HasUnit || target.Unit is not EnemyCombat ec || !ec.IsAlive || ec.CurrentHealth <= 0)
                    return false;

                if (alreadyChecked.Contains(ec.ID))
                    return false;

                if (en == null)
                    en = ec.Enemy;

                else if (en != ec.Enemy)
                    return false;

                alreadyChecked.Add(ec.ID);
            }

            exitAmount = targets.Length;

            return true;
        }
    }
}
