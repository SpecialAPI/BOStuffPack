using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class RecursiveCloneWithPerformedEffectsEffect : EffectSO
    {
        public List<EffectInfo> effects = [];
        public bool effectsAreImmediate;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            CombatManager.Instance.AddSubAction(new RecursivelyCloneEnemiesDoEffectsOnClonesAction([..effects], effectsAreImmediate));

            return true;
        }

        public static EffectSO Create(List<EffectInfo> effects, bool effectsAreImmediate = false)
        {
            var e = CreateScriptable<RecursiveCloneWithPerformedEffectsEffect>();
            e.effects = effects ?? [];
            e.effectsAreImmediate = effectsAreImmediate;

            return e;
        }
    }
}
