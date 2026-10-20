using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class TargetSideCheckEffectorCondition : EffectorConditionSO
    {
        public UnitSide neededSide;
        public bool ignoreCaster = false;
        public int unitValueIndex;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(!ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder) || unitHolder[unitValueIndex] is not IUnit u)
                return false;

            if(ignoreCaster && u == effector)
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

        public static EffectorConditionSO Create(UnitSide neededSide, bool ignoreCaster = false, int unitValueIndex = 0)
        {
            var ec = CreateScriptable<TargetSideCheckEffectorCondition>();
            ec.neededSide = neededSide;
            ec.ignoreCaster = ignoreCaster;
            ec.unitValueIndex = unitValueIndex;

            return ec;
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
