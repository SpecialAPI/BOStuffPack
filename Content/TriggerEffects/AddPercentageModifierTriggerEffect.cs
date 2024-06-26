using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class AddPercentageModifierTriggerEffect(int percent, bool increase) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger)
        {
            if(args is DamageDealtValueChangeException ex)
                ex.AddModifier(new PercentageValueModifier(true, percent, increase));
        }
    }
}
