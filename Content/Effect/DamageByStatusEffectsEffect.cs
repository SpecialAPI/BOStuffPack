using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class DamageByStatusEffectsEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var amt = entryVariable;

                if (t.Unit is IStatusEffector effector)
                    amt += effector.StatusEffects.Count * PreviousExitValue;

                exitAmount += t.Unit.Damage(caster.WillApplyDamage(amt, t.Unit), caster, DeathType_GameIDs.Basic.ToString(), areTargetSlots ? t.TargetOffset() : -1).damageAmount;
            }

            if (exitAmount > 0)
                caster.DidApplyDamage(exitAmount);

            return exitAmount > 0;
        }
    }
}
