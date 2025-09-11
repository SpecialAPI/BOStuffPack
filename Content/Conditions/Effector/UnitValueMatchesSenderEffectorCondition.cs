using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class UnitValueMatchesSenderEffectorCondition : EffectorConditionSO
    {
        public int unitValueIndex;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(!ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder) || unitHolder[unitValueIndex] is not IUnit unit)
                return false;

            return effector == unit;
        }
    }
}
