using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class DealAdditionalDamagePercentageAtFullHealthEffect : EffectSO
    {
        public int percentage;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if (t == null || !t.HasUnit)
                    continue;

                var offs = areTargetSlots ? t.SlotID - t.Unit.SlotID : -1;
                var amt = entryVariable;

                if (t.Unit.CurrentHealth >= t.Unit.MaximumHealth)
                    amt = Mathf.RoundToInt(amt * (percentage / 100f + 1f));

                exitAmount += t.Unit.Damage(caster.WillApplyDamage(amt, t.Unit), caster, DeathType_GameIDs.Basic.ToString(), offs).damageAmount;
            }

            if(exitAmount > 0)
                caster.DidApplyDamage(exitAmount);

            return exitAmount > 0;
        }
    }
}
