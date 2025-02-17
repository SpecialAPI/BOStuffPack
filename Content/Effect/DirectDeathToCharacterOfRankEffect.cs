using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class DirectDeathToCharacterOfRankEffect : EffectSO
    {
        public int rankTarget;
        public IntComparison rankComparison;

        public bool obliterate;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(!t.HasUnit)
                    continue;

                if(t.Unit is not CharacterCombat cc || !CompareInts(cc.ClampedRank, rankTarget, rankComparison))
                    continue;

                t.Unit.DirectDeath(caster, obliterate);
            }

            return exitAmount > 0;
        }
    }
}
