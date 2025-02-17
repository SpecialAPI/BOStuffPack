using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class PerformEffectOnRandomTargetEffect : EffectSO
    {
        public BaseCombatTargettingSO overrideTargetting;
        public int chanceToUseOverrideTargetting;

        public EffectInfo effect;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (overrideTargetting != null && Random.Range(0, 100) < chanceToUseOverrideTargetting)
                targets = overrideTargetting.GetTargets(stats.combatSlots, caster.SlotID, caster.IsUnitCharacter);

            if (targets.Length > 0)
            {
                exitAmount = effect.StartEffect(stats, caster, [targets[Random.Range(0, targets.Length)]], true, PreviousExitValue);

                return effect.EffectSuccess;
            }
            return false;
        }
    }
}
