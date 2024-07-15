using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class TargettedEffectApplicationMatchesEffectEffectorCondition : EffectorConditionSO
    {
        public string status;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return args is TargettedStatusEffectApplicationInfo inf && inf.statusEffect != null && inf.statusEffect.IsStatus(status);
        }
    }
}
