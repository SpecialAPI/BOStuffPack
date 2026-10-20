using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.TriggerEffects
{
    public class TargetApplyStatusTriggerEffect(StatusEffect_SO status, int amount, int restrictor = 0, int unitValueIndex = 0) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (!ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder) || unitHolder[unitValueIndex] is not IUnit u)
                return;

            u.ApplyStatusEffect(status, amount, restrictor);
        }
    }
}
