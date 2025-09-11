using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class DamageByTargetStoredValueEffect : EffectSO
    {
        public string damageType = "";
        public string storedValueID;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var storedValue = t.Unit.SimpleGetStoredValue(storedValueID);

                if(storedValue <= 0)
                    continue;

                exitAmount += t.Unit.Damage(caster.WillApplyDamage(entryVariable * storedValue, t.Unit), caster, DeathType_GameIDs.Basic.ToString(), t.TargetOffset(areTargetSlots), true, true, false, damageType).damageAmount;
            }

            return exitAmount > 0;
        }
    }
}
