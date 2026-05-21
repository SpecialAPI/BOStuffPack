using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.TriggerEffects
{
    public class UnitValueStoredValueChangeTriggerEffect(string sv, int change, IntOperation operation = IntOperation.Add, int unitValueIndex = 0) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if(!ValueReferenceTools.TryGetUnitHolder(args, out var holder) || holder[unitValueIndex] is not IUnit u)
                return;

            u.TryGetStoredData(sv, out var svHolder);
            svHolder.m_MainData = DoOperation(svHolder.m_MainData, change, operation);
        }
    }
}
