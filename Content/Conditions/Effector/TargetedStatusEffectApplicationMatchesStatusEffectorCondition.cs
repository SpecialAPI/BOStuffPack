using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class TargetedStatusEffectApplicationMatchesStatusEffectorCondition : EffectorConditionSO
    {
        public StatusEffect_SO status;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return status != null && args is TargetedStatusEffectApplication appl && appl.statusEffect != null && appl.statusEffect.StatusID == status.StatusID;
        }
    }
}
