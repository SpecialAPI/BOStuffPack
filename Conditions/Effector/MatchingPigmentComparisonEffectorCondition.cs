using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
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

        public static EffectorConditionSO Create(ManaColorSO targetColor, int compareTo, IntComparison comparison, bool checkInBar = true, bool checkInGenerator = false, bool checkInOverflow = false, bool shouldHaveAnyMana = false, PigmentMatchType matchType = PigmentMatchType.Exact)
        {
            var ec = CreateScriptable<MatchingPigmentComparisonEffectorCondition>();
            ec.targetColor = targetColor;
            ec.compareTo = compareTo;
            ec.comparison = comparison;
            ec.checkInBar = checkInBar;
            ec.checkInGenerator = checkInGenerator;
            ec.checkInOverflow = checkInOverflow;
            ec.checkedBarsShouldHaveAnyMana = shouldHaveAnyMana;
            ec.matchType = matchType;

            return ec;
        }
    }
}
