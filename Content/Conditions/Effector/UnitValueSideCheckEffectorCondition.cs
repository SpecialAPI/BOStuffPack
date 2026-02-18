using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class UnitValueSideCheckEffectorCondition : EffectorConditionSO
    {
        public int unitValueIndex;
        public UnitSide neededSide;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(!ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder) || unitHolder[unitValueIndex] is not IUnit u)
                return false;

            var checkSide = neededSide switch
            {
                UnitSide.Character => true,
                UnitSide.Enemy => false,

                UnitSide.SameAsCaster => effector.IsUnitCharacter,
                UnitSide.OppositeOfCaster => !effector.IsUnitCharacter,

                _ => false
            };

            return u.IsUnitCharacter == checkSide;
        }

        public enum UnitSide
        {
            Character,
            Enemy,

            SameAsCaster,
            OppositeOfCaster
        }
    }
}
