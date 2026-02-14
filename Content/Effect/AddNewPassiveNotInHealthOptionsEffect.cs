using Grimoire.HealthColorOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class AddNewPassiveNotInHealthOptionsEffect : EffectSO
    {
        public List<(string pigment, BasePassiveAbilitySO passive)> passives;
        public bool addToRandomTarget;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (passives == null)
                return false;

            if (targets.Length <= 0)
                return false;

            if (addToRandomTarget)
            {
                var options = targets.Where(x => x != null && x.HasUnit).ToArray();

                if (options.Length <= 0)
                    return false;

                targets = [options.RandomElement()];
            }

            foreach (var target in targets)
            {
                if (target == null || !target.HasUnit)
                    continue;

                var u = target.Unit;
                var possiblePassives = passives.ToList();
                var added = 0;

                while (possiblePassives.Count > 0 && added < entryVariable)
                {
                    var idx = Random.Range(0, possiblePassives.Count);
                    var (pigment, passive) = possiblePassives[idx];
                    possiblePassives.RemoveAt(idx);

                    if (pigment == null || passive == null)
                        continue;

                    if (u.ContainsPassiveAbility(passive.m_PassiveID))
                        continue;

                    if (u.GetHealthColorOptions().Any(x => x.pigmentID == pigment))
                        continue;

                    u.AddPassiveAbility(passive);
                    added++;
                }

                exitAmount += added;
            }

            return exitAmount > 0;
        }
    }
}
