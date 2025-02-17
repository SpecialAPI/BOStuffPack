using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effect
{
    public class PercentageByStoredValueEffectCondition : EffectConditionSO
    {
        public int basePercentage;

        public string storedValue;
        public int storedValueMult = 1;

        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            return Random.Range(0, 100) < (basePercentage + caster.SimpleGetStoredValue(storedValue) * storedValueMult);
        }
    }
}
