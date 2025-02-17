using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class KingslayerDamageEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var offs = areTargetSlots ? t.SlotID - t.Unit.SlotID : -1;

                var inf = t.Unit.Damage(caster.WillApplyDamage(entryVariable, t.Unit), caster, DeathType_GameIDs.Basic.ToString(), offs);
                exitAmount += inf.damageAmount;

                if (inf.damageAmount <= 0 || t.Unit.Size <= 1)
                    continue;

                for(int i = 0; i < t.Unit.Size; i++)
                {
                    if (t.Unit.SlotID + i == t.SlotID)
                        continue;

                    offs = areTargetSlots ? i : -1;

                    exitAmount += t.Unit.Damage(entryVariable, null, DeathType_GameIDs.Basic.ToString(), offs, false, false, true).damageAmount;
                }
            }

            if(exitAmount > 0)
                caster.DidApplyDamage(exitAmount);

            return exitAmount > 0;
        }
    }
}
