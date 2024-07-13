using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class AnimationVisualsOnCharacterOfRankEffect : AnimationVisualsOnEffectTargetsEffect
    {
        public int rankTarget;
        public IntComparison rankComparison;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            targets = targets.Where(x => x.HasUnit && x.Unit is CharacterCombat cc && CompareInts(cc.ClampedRank, rankTarget, rankComparison)).ToArray();

            if (targets.Length <= 0)
                return false;

            return base.PerformEffect(stats, caster, targets, areTargetSlots, entryVariable, out exitAmount);
        }
    }
}
