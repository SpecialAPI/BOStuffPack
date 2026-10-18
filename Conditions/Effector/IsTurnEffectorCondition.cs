using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class IsTurnEffectorCondition : EffectorConditionSO
    {
        public int reqTurn;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return CombatManager.Instance._stats.CurrentPlayerTurn() == reqTurn;
        }

        public static EffectorConditionSO Create(int reqTurn)
        {
            var c = CreateScriptable<IsTurnEffectorCondition>();
            c.reqTurn = reqTurn;

            return c;
        }
    }
}
