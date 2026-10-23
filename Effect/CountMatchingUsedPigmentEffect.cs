using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class CountMatchingUsedPigmentEffect : EffectSO
    {
        public ManaColorSO targetColor;
        public PigmentMatchType matchType;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = caster.Ext().PigmentUsedForAbility.Count(x => x.PigmentMatch(targetColor, matchType));

            return exitAmount > entryVariable;
        }

        public static EffectSO Create(ManaColorSO targetColor, PigmentMatchType matchType = PigmentMatchType.Exact)
        {
            var e = CreateScriptable<CountMatchingUsedPigmentEffect>();
            e.targetColor = targetColor;
            e.matchType = matchType;

            return e;
        }
    }
}
