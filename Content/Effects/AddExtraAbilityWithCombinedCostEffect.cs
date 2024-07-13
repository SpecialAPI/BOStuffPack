using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class AddExtraAbilityWithCombinedCostEffect : EffectSO
    {
        public ExtraAbilityInfo ability;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var abs = caster.Abilities();

            if (abs == null)
                return false;

            caster.AddExtraAbility(new()
            {
                ability = ability.ability,
                cost = abs.Select(x => x.cost).SelectMany(x => x).ToArray(),
                rarity = ability.rarity
            });

            return true;
        }
    }
}
