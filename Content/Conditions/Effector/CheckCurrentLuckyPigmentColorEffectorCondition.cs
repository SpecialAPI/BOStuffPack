using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class CheckCurrentLuckyPigmentColorEffectorCondition : EffectorConditionSO
    {
        public ManaColorSO targetColor;
        public PigmentMatchType matchType;
        public bool shouldMatch;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            var stats = CombatManager.Instance._stats;
            var currColor = stats.LuckyManaColorOptions[stats.SelectedLuckyColor];

            return currColor.PigmentMatch(targetColor, matchType) == shouldMatch;
        }
    }
}
