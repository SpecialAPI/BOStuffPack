using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class AdvancedIntegerReferenceCheckCondition : EffectorConditionSO
    {
        public IntCondition valueCondition;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return args.TryGetIntReference(out var intRef) && MeetsIntCondition(intRef.value, valueCondition);
        }
    }
}
