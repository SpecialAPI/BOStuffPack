using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Targets
{
    public class UnitFilterByStoredValueInRangeTargeting : UnitFilterTargetingBase
    {
        public string storedValue;
        public int? min;
        public int? max;

        protected override bool FilterUnit(IUnit unit, TargetSlotInfo target, SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            var sv = unit.SimpleGetStoredValue(storedValue);

            if (min is int min_ && sv < min_)
                return false;

            if(max is int max_ && sv > max_)
                return false;

            return true;
        }
    }
}
