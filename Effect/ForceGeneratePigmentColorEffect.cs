using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class ForceGeneratePigmentColorEffect : EffectSO
    {
        public ManaColorSO pigment;
        public bool usePreviousExitValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (usePreviousExitValue)
            {
                entryVariable *= PreviousExitValue;
            }
            exitAmount = entryVariable;
            CombatManager.Instance.ProcessImmediateAction(new ForceAddPigmentAction(pigment, entryVariable, caster.IsUnitCharacter, caster.ID));
            return true;
        }

        public static EffectSO Create(ManaColorSO pigment, bool usePreviousExitValue = false)
        {
            var e = CreateScriptable<ForceGeneratePigmentColorEffect>();
            e.pigment = pigment;
            e.usePreviousExitValue = usePreviousExitValue;

            return e;
        }
    }
}
