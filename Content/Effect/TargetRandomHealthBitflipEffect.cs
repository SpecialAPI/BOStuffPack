using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class TargetRandomHealthBitflipEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if (t == null || !t.HasUnit)
                    continue;

                var bitOffs = Random.Range(0, 31);
                var newHealth = FlipBit(t.Unit.CurrentHealth, bitOffs);

                if (newHealth <= 0)
                {
                    if (t.Unit.DirectDeath(caster, false, out _))
                        exitAmount++;
                }

                else
                {
                    if (t.Unit.SetHealthTo(newHealth))
                        exitAmount++;
                }
            }

            return exitAmount > 0;
        }

        public static int FlipBit(int num, int offset)
        {
            var mask = 1 << offset;
            var bit = (num & mask) >> offset;

            if (bit == 0)
                num |= mask;
            else
                num &= ~mask;

            return num;
        }
    }
}
