using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class IntValueConditionCheckEffectorCondition : EffectorConditionSO
    {
        public IntCondition condition;
        public int intValueIndex;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return ValueReferenceTools.TryGetIntHolder(args, out var intRef) && MeetsIntCondition(intRef[intValueIndex], condition);
        }

        public static EffectorConditionSO Create(IntCondition condition, int intValueIndex = 0)
        {
            var ec = CreateScriptable<IntValueConditionCheckEffectorCondition>();
            ec.condition = condition;
            ec.intValueIndex = intValueIndex;

            return ec;
        }
    }
}
