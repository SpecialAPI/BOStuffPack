using BOStuffPack.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class AdvancedAnimationVisualsEffect : EffectSO
    {
        public List<AdvancedAnimationData> animations;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = PreviousExitValue;
            CombatManager.Instance.AddUIAction(new AdvancedAnimationVisualsAction(animations, caster));

            return true;
        }
    }
}
