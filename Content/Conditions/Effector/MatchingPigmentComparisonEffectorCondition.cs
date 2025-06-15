using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class MatchingPigmentComparisonEffectorCondition : EffectorConditionSO
    {
        public ManaColorSO targetColor;
        public int compareTo;
        public IntComparison comparison;
        public PigmentMatchType matchType;
        public bool checkInBar = true;
        public bool checkInGenerator = false;
        public bool checkInOverflow = false;
        public bool checkedBarsShouldHaveAnyMana = false;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            var matchingAmount = 0;
            var totalAmount = 0;
            var stats = CombatManager.Instance._stats;

            if (checkInBar)
            {
                var slots = stats.MainManaBar.ManaBarSlots;

                matchingAmount += slots.Count(x => !x.IsEmpty && x.ManaColor.PigmentMatch(targetColor, matchType));
                totalAmount += slots.Count(x => !x.IsEmpty);
            }
            if (checkInGenerator)
            {
                var slots = stats.YellowManaBar.ManaBarSlots;

                matchingAmount += slots.Count(x => !x.IsEmpty && x.ManaColor.PigmentMatch(targetColor, matchType));
                totalAmount += slots.Count(x => !x.IsEmpty);
            }
            if (checkInOverflow)
            {
                var box = stats.MainManaBar._storedManaBox;

                matchingAmount += box.StoredSlots.Count(x => x.PigmentMatch(targetColor, matchType));
                totalAmount += box.OverflowManaAmount;
            }

            if (checkedBarsShouldHaveAnyMana && totalAmount <= 0)
                return false;

            return CompareInts(matchingAmount, compareTo, comparison);
        }
    }
}
