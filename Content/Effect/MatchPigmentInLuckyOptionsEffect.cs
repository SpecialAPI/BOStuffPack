using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class MatchPigmentInLuckyOptionsEffect : EffectSO
    {
        public ManaColorSO pigment;
        public PigmentMatchType match;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = stats.LuckyManaColorOptions.Count(x => x.PigmentMatch(pigment, match));

            return exitAmount > 0;
        }
    }
}
