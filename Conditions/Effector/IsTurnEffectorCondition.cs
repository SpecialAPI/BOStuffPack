using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class IsTurnEffectorCondition : EffectorConditionSO
    {
        public int targetTurn;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return CombatManager.Instance._stats.CurrentPlayerTurn() == targetTurn;
        }

        public static EffectorConditionSO Create(int targetTurn)
        {
            var c = CreateScriptable<IsTurnEffectorCondition>();
            c.targetTurn = targetTurn;

            return c;
        }
    }
}
