using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effect
{
    public class CurrentTurnIsSpecificTurnInRotationCondition : EffectConditionSO
    {
        public int Rotation;
        public int PositionInRotation;

        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            return CombatManager._instance._stats.TurnsPassed % Rotation + 1 == PositionInRotation;
        }
    }
}
