using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class CasterReplaceExtraAbilityEffect : EffectSO
    {
        public AbilitySO abilityToReplace;
        public ExtraAbilityInfo replacement;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (abilityToReplace == null || replacement == null)
                return false;

            var success = false;

            var extraAb = caster.ExtraAbilities();
            var ab = caster.Abilities();

            for(var i = 0; i < extraAb.Count; i++)
            {
                var a = extraAb[i];

                if (a == null || a.ability != abilityToReplace)
                    continue;

                extraAb[i] = replacement;
                success = true;
            }

            for(var i = 0; i < ab.Count; i++)
            {
                var a = ab[i];

                if(a == null || a.ability != abilityToReplace)
                    continue;

                ab[i] = new(replacement.ability, replacement.cost) { rarity = replacement.rarity };
                success = true;
            }

            if (success)
            {
                if (caster.IsUnitCharacter)
                    CombatManager.Instance.AddUIAction(new CharacterUpdateAllAttacksUIAction(caster.ID, ab.ToArray()));
                else
                    CombatManager.Instance.AddUIAction(new EnemyUpdateAllAttacksUIAction(caster.ID, ab.ToArray()));
            }

            return success;
        }
    }
}
