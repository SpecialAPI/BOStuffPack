using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class CanProducePigmentColorCheckCondition : EffectorConditionSO
    {
        public string pigmentTypeToCheck;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return args is CanProducePigmentColorInfo inf && inf.pigment != null && inf.pigment.pigmentTypes.Contains(pigmentTypeToCheck);
        }
    }
}
