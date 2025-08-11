using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class RandomSpecialDamageBetweenPreviousAndEntryEffect : EffectSO
    {
        public bool indirect;
        public SpecialDamageInfo specialDamage;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var amount = Random.Range(base.PreviousExitValue, entryVariable + 1);

                if (indirect)
                    exitAmount += t.Unit.SpecialDamage(amount, null, specialDamage, DeathType_GameIDs.Basic.ToString(), t.TargetOffset(areTargetSlots), false, false, true).damageAmount;
                else
                    exitAmount += t.Unit.SpecialDamage(caster.WillApplyDamage(amount, t.Unit), caster, specialDamage, DeathType_GameIDs.Basic.ToString(), t.TargetOffset(areTargetSlots), true, true, false).damageAmount;
            }

            if(exitAmount > 0 && !indirect)
                caster.DidApplyDamage(exitAmount);

            return exitAmount > 0;
        }
    }
}
