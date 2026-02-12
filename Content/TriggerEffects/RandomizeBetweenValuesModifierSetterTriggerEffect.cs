using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class RandomizeBetweenValuesModifierSetterTriggerEffect(int value1, bool value1IsPercentage, int value2, bool value2IsPercentage) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (!ValueReferenceTools.TryGetValueChangeException(args, out var ex))
                return;

            ex.AddModifier(new RandomizeBetweenValuesIntValueModifier(value1, value1IsPercentage, value2, value2IsPercentage));
        }
    }

    public class RandomizeBetweenValuesIntValueModifier(int value1, bool value1IsPercentage, int value2, bool value2IsPercentage) : IntValueModifier(-10)
    {
        public override int Modify(int value)
        {
            var v1 = value1;
            if (value1IsPercentage)
                v1 = Mathf.FloorToInt(value * (float)v1 / 100f);

            var v2 = value2;
            if (value2IsPercentage)
                v2 = Mathf.FloorToInt(value * (float)v2 / 100f);

            var rangeMin = Mathf.Min(v1, v2);
            var rangeMax = Mathf.Max(v1, v2);

            if (rangeMin == rangeMax)
                return rangeMin;

            return Random.Range(rangeMin, rangeMax + 1);
        }
    }
}
