using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class DamageBySizeWithPreviousExitValueEffect : EffectSO
    {
        public int targetSize;
        public IntComparison sizeComparison;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var amt = entryVariable;

                if (CompareInts(t.Unit.Size, targetSize, sizeComparison))
                    amt = PreviousExitValue;

                exitAmount += t.Unit.Damage(caster.WillApplyDamage(amt, t.Unit), caster, DeathType_GameIDs.Basic.ToString(), areTargetSlots ? t.TargetOffset() : -1).damageAmount;
            }

            if(exitAmount > 0)
                caster.DidApplyDamage(exitAmount);

            return exitAmount > 0;
        }
    }
}
