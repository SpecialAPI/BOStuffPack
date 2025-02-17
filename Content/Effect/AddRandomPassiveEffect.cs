using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class AddRandomPassiveEffect : EffectSO
    {
        public List<BasePassiveAbilitySO> passives;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (passives == null || passives.Count <= 0)
                return false;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var options = new List<BasePassiveAbilitySO>(passives);

                while(options.Count > 0)
                {
                    var idx = Random.Range(0, options.Count);

                    var pa = options[idx];
                    options.RemoveAt(idx);

                    if (pa == null || t.Unit.ContainsPassiveAbility(pa.m_PassiveID) || !t.Unit.AddPassiveAbility(pa))
                        continue;

                    exitAmount++;
                    break;
                }
            }

            return exitAmount > 0;
        }
    }
}
