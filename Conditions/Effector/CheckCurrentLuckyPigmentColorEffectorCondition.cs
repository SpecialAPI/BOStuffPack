using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class CheckCurrentLuckyPigmentColorEffectorCondition : EffectorConditionSO
    {
        public ManaColorSO targetColor;
        public bool shouldMatch;
        public PigmentMatchType matchType;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            var stats = CombatManager.Instance._stats;
            var currColor = stats.LuckyManaColorOptions[stats.SelectedLuckyColor];

            return currColor.PigmentMatch(targetColor, matchType) == shouldMatch;
        }

        public static EffectorConditionSO Create(ManaColorSO targetColor, bool shouldMatch, PigmentMatchType matchType = PigmentMatchType.Exact)
        {
            var ec = CreateScriptable<CheckCurrentLuckyPigmentColorEffectorCondition>();
            ec.targetColor = targetColor;
            ec.shouldMatch = shouldMatch;
            ec.matchType = matchType;

            return ec;
        }
    }
}
