using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class MaximizeValueToPercentOfOriginalLateTriggerEffect(int percent) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (!ValueReferenceTools.TryGetValueChangeException(args, out var ex))
                return;

            if (ex.OriginalValue <= 0)
                return;

            var a = Mathf.Max(1, Mathf.FloorToInt(ex.OriginalValue * (percent / 100f)));
            ex.AddModifier(new LateMaximizationValueModifier(a));
        }
    }
}
