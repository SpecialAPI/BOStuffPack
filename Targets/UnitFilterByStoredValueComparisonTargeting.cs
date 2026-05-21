using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Targets
{
    public class UnitFilterByStoredValueComparisonTargeting : UnitFilterTargetingBase
    {
        public string storedValue;
        public int compareTo;
        public IntComparison comparison;

        protected override bool FilterUnit(IUnit unit, TargetSlotInfo target, SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            return CompareInts(unit.SimpleGetStoredValue(storedValue), compareTo, comparison);
        }
    }
}
