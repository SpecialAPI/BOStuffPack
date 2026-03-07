using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class DodgeDamageModifierSetterTriggerEffect(string tempDisableSV) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            if (ex.damagedUnit is not IUnit target)
                return;

            ex.AddModifier(new DodgeDamageValueModifier(target, ex, sender, tempDisableSV));
        }
    }
}
