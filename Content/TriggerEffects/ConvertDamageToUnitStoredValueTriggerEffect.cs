using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class ConvertDamageToUnitStoredValueTriggerEffect(int unitValueIndex, string storedValueID) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (!ValueReferenceTools.TryGetValueChangeException(args, out var ex) || !ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder))
                return;

            if (unitHolder[unitValueIndex] is not IUnit unit)
                return;

            ex.AddModifier(new ConvertValueToStoredValueIntModifier(unit, storedValueID));
        }
    }

    public class ConvertValueToStoredValueIntModifier(IUnit unit, string storedValueID) : IntValueModifier(-100)
    {
        public override int Modify(int value)
        {
            if (value <= 0)
                return value;

            if (unit == null)
                return value;

            unit.SimpleSetStoredValue(storedValueID, unit.SimpleGetStoredValue(storedValueID) + value);
            return 0;
        }
    }
}
