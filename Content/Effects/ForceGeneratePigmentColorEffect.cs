using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
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

    public class ForceAddPigmentAction(ManaColorSO pigment, int amount, bool isGeneratorCharacter, int id) : IImmediateAction
    {
        public void Execute(CombatStats stats)
        {
            if (pigment != null)
            {
                stats.MainManaBar.AddManaAmount(pigment, amount, stats.GenerateUnitJumpInformation(id, isGeneratorCharacter));
            }
        }
    }
}
