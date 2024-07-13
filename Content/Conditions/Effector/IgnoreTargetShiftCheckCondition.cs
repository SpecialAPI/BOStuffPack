using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class IgnoreTargetShiftCheckCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return args is ModifyTargettingInfo inf && (inf.ability is not AdvancedAbilitySO adv || adv.Flags == null || !adv.Flags.Contains("Ignore_TargetShift"));
        }
    }
}
