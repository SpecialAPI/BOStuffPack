using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class StoredValueNonPositiveEffectorCondition : EffectorConditionSO
    {
        public string value;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (effector is IUnit u)
            {
                return u.SimpleGetStoredValue(value) <= 0;
            }
            return false;
        }
    }
}
