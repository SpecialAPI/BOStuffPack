using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ExhaustMovementEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if (t == null || !t.HasUnit || t.Unit is not CharacterCombat cc)
                    continue;

                var previous = cc.CanSwapNoTrigger;

                cc.CanSwap = false;
                cc.SetVolatileUpdateUIAction(true);

                if (previous && !cc.CanSwapNoTrigger)
                    exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
