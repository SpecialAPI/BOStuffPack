using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effect
{
    public class CurrentTurnIsSpecificTurnInRotationEffectCondition : EffectConditionSO
    {
        public int positionInRotation;
        public int rotation;

        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            return CombatManager._instance._stats.TurnsPassed % rotation + 1 == positionInRotation;
        }

        public static EffectConditionSO Create(int positionInRotation, int rotation)
        {
            var ec = CreateScriptable<CurrentTurnIsSpecificTurnInRotationEffectCondition>();
            ec.positionInRotation = positionInRotation;
            ec.rotation = rotation;

            return ec;
        }
    }
}
