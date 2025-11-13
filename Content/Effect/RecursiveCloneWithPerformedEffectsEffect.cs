using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class RecursiveCloneWithPerformedEffectsEffect : EffectSO
    {
        public EffectInfo[] effects = [];
        public bool effectsAreImmediate = false;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            CombatManager.Instance.AddSubAction(new RecursivelyCloneEnemiesDoEffectsOnClonesAction(effects, effectsAreImmediate));

            return true;
        }
    }
}
