using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class PlayAnimationOnAllTargetsFulfillingStoredValueConditionEffect : EffectSO
    {
        public string storedValueID;
        public IntCondition storedValueCondition;
        public AttackVisualsSO visuals;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            var realTargets = targets.Where(x => x != null && x.HasUnit && MeetsIntCondition(x.Unit.SimpleGetStoredValue(storedValueID), storedValueCondition)).ToArray();
            CombatManager.Instance.AddUIAction(new PlayAbilityAnimationAction(visuals, realTargets, areTargetSlots));

            return (exitAmount = realTargets.Length) > 0;
        }
    }
}
