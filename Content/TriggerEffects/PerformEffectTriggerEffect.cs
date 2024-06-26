using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class PerformEffectTriggerEffect(List<EffectInfo> effects) : TriggerEffect
    {
        public EffectInfo[] effects = [.. effects];

        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger)
        {
            if (effectsAndTrigger.immediate)
            {
                CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction(effects, sender, 0));
            }
            else
            {
                CombatManager.Instance.AddSubAction(new EffectAction(effects, sender, 0));
            }
        }
    }
}
