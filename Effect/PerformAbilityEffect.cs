using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class PerformAbilityEffect : EffectSO
    {
        public AbilitySO ability;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (ability == null)
                return false;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                if (t.Unit.TryPerformRandomAbility(ability))
                    exitAmount++;
            }

            return exitAmount > 0;
        }

        public static EffectSO Create(AbilitySO ability)
        {
            var e = CreateScriptable<PerformAbilityEffect>();
            e.ability = ability;

            return e;
        }
    }
}
