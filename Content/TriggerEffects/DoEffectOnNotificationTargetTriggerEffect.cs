using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class DoEffectOnNotificationTargetTriggerEffect(TriggerEffect effect) : TriggerEffect
    {
        public TriggerEffect effect = effect;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger)
        {
            if (args is not ITargettedNotificationInfo inf)
                return;

            effect.DoEffect(inf.Target, args, effectsAndTrigger);
        }
    }
}
