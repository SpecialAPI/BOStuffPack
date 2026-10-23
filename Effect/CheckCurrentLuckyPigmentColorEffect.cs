using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class CheckCurrentLuckyPigmentColorEffect : EffectSO
    {
        public ManaColorSO targetColor;
        public bool shouldMatch;
        public PigmentMatchType matchType;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            var currColor = stats.LuckyManaColorOptions[stats.SelectedLuckyColor];

            return currColor.PigmentMatch(targetColor, matchType) == shouldMatch;
        }

        public static EffectSO Create(ManaColorSO targetColor, bool shouldMatch, PigmentMatchType matchType = PigmentMatchType.Exact)
        {
            var e = CreateScriptable<CheckCurrentLuckyPigmentColorEffect>();
            e.targetColor = targetColor;
            e.shouldMatch = shouldMatch;
            e.matchType = matchType;

            return e;
        }
    }
}
