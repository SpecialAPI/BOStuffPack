using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ForceGeneratePigmentColorEffect : EffectSO
    {
        public bool usePreviousExitValue;
        public ManaColorSO pigmentColor;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (usePreviousExitValue)
            {
                entryVariable *= PreviousExitValue;
            }
            exitAmount = entryVariable;
            CombatManager.Instance.ProcessImmediateAction(new ForceAddPigmentAction(pigmentColor, entryVariable, caster.IsUnitCharacter, caster.ID));
            return true;
        }
    }
}
