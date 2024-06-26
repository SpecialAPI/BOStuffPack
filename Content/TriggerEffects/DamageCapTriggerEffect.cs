using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class DamageCapTriggerEffect(int max) : TriggerEffect
    {
        public int maxDamage = max;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger)
        {
            if(args is DamageReceivedValueChangeException ex)
            {
                ex.AddModifier(new MinimizationValueModifier(false, maxDamage));
            }
        }
    }
}
