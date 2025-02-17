using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ConsumeLiterallyAllPigmentEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            var jump = stats.GenerateUnitJumpInformation(caster.ID, caster.IsUnitCharacter);

            exitAmount = 0;

            if (stats.overflowMana != null)
                exitAmount += stats.overflowMana.DepleteAllStoredMana();

            if (stats.MainManaBar != null)
                exitAmount += stats.MainManaBar.ConsumeAllMana(jump);

            if(stats.YellowManaBar != null)
                exitAmount += stats.YellowManaBar.ConsumeAllMana(jump);

            return exitAmount > 0;
        }
    }
}
