using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effect
{
    public class PercentageByStoredValueEffectCondition : EffectConditionSO
    {
        public string storedValue;
        public int storedValueMult = 1;
        public int basePercentage;

        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            return Random.Range(0, 100) < basePercentage + caster.SimpleGetStoredValue(storedValue) * storedValueMult;
        }

        public static EffectConditionSO Create(string storedValue, int basePercentage = 0, int storedValueMult = 1)
        {
            var ec = CreateScriptable<PercentageByStoredValueEffectCondition>();
            ec.storedValue = storedValue;
            ec.basePercentage = basePercentage;
            ec.storedValueMult = storedValueMult;

            return ec;
        }
    }
}
