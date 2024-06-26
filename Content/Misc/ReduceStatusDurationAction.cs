using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class ReduceStatusDurationAction(StatusEffect_SO effect, StatusEffect_Holder hold, IStatusEffector effector) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            effect.ReduceDuration(hold, effector);
            yield break;
        }
    }
}
