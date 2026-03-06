using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class PercentageModifierSetterTriggerEffect(int percentage, bool increase) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (!ValueReferenceTools.TryGetValueChangeException(args, out var ex))
                return;

            ex.AddModifier(new PercentageValueModifier(ex.DamageDealt, percentage, increase));
        }
    }
}
