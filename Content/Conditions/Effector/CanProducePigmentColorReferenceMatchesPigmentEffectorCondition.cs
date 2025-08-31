using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class CanProducePigmentColorReferenceMatchesPigmentEffectorCondition : EffectorConditionSO
    {
        public ManaColorSO pigment;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return pigment != null && args is CanProducePigmentColorReference canProduceRef && canProduceRef.pigment != null && canProduceRef.pigment.pigmentID == pigment.pigmentID;
        }
    }
}
