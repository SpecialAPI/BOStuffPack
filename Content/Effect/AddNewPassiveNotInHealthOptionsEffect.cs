using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class AddNewPassiveNotInHealthOptionsEffect : EffectSO
    {
        public List<(string pigment, BasePassiveAbilitySO passive)> passives;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (passives == null)
                return false;

            foreach (var target in targets)
            {
                if (target.HasUnit)
                {
                    var u = target.Unit;
                    var possiblePassives = passives.FindAll(x => x.passive != null && !target.Unit.ContainsPassiveAbility(x.passive.m_PassiveID) && !target.Unit.Ext().HealthColors.Exists(x2 => x2.pigmentID == x.pigment));

                    if (possiblePassives.Count > 0)
                    {
                        var (pigment, passive) = possiblePassives[Random.Range(0, possiblePassives.Count)];

                        if (u.AddPassiveAbility(passive))
                            exitAmount++;
                    }
                }
            }

            return exitAmount > 0;
        }
    }
}
