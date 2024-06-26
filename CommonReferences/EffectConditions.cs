using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOStuffPack.CommonReferences
{
    public static class EffectConditions
    {
        public static EffectConditionSO Chance(int chance) => CreateScriptable<PercentageEffectCondition>(x => x.percentage = chance);

        public static EffectConditionSO Previous(int previousAmount, bool wasSuccessful) => CreateScriptable<PreviousEffectCondition>(x =>
        {
            x.previousAmount = previousAmount;
            x.wasSuccessful = wasSuccessful;
        });

        public static EffectConditionSO MultiPrevious(params (int previousAmount, bool wasSuccessful)[] prevs) => CreateScriptable<MultiPreviousEffectCondition>(x =>
        {
            x.previousAmount = prevs.Select(x => x.previousAmount).ToArray();
            x.wasSuccessful = prevs.Select(x => x.wasSuccessful).ToArray();
        });

        public static CurrentTurnIsSpecificTurnInRotationCondition TurnInRotation(int pos, int rot) => CreateScriptable<CurrentTurnIsSpecificTurnInRotationCondition>(x => { x.Rotation = rot; x.PositionInRotation = pos; });
    }
}
