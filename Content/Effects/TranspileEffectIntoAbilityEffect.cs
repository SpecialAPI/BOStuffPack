using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class TranspileEffectIntoAbilityEffect : EffectSO
    {
        public EffectInfo effect;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var units = targets
                .Select(x => x.Unit)
                .Where(x =>
                        x != null &&

                        (x is not CharacterCombat cc ||

                        (cc.Character != null &&
                        cc.Character.HasRankedData &&
                        cc.Character.rankedData.Any(x => x.rankAbilities.Length > 0))))

                .ToList();

            if (units.Count <= 0)
                return false;

            var u = units.RandomElement();

            if(u is CharacterCombat cc)
            {
                var abs = new List<List<CharacterAbility>>();

                foreach (var rank in cc.Character.rankedData)
                    abs.Add(cc.UsedRankedAbilities(rank));

                if (abs.Count <= 0)
                    return false;

                var maxLen = abs.Min(x => x.Count);
                var randomIdx = Random.Range(0, maxLen);

                var targettedAbilities = abs.Select(x => x[randomIdx]).ToList();

                foreach(var ab in targettedAbilities)
                {
                    if (ab.ability == null)
                        continue;

                    ab.ability.effects = ab.ability.effects.AddToArray(effect);
                }
            }

            else
            {
                var abs = u.Abilities();

                if (abs == null || abs.Count <= 0)
                    return false;

                var randomAb = abs.RandomElement();

                if(randomAb == null || randomAb.ability == null)
                    return false;

                randomAb.ability.effects = randomAb.ability.effects.AddToArray(effect);
            }

            return true;
        }
    }
}
