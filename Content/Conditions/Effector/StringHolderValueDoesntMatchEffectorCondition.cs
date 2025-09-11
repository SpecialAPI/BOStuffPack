using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class StringHolderValueDoesntMatchEffectorCondition : EffectorConditionSO
    {
        public int stringValueIndex;
        public string value;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(!ValueReferenceTools.TryGetStringHolder(args, out var stringHolder))
                return false;

            return stringHolder[stringValueIndex] != value;
        }
    }
}
