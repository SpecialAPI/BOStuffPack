using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class MoveSenderTowardsUnitValueTriggerEffect(int valueIndex = 0) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (!ValueReferenceTools.TryGetUnitHolder(sender, out var unitHolder) || unitHolder[valueIndex] is not IUnit target)
                return;

            var leftOf = sender.IsLeftOf(target);
            var rightOf = sender.IsRightOf(target);

            if (leftOf) // move right
                sender.TryMoveUnit(toRight: true);
            else if (rightOf) // move left
                sender.TryMoveUnit(toRight: false);
        }
    }
}
