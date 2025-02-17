using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class PerformRandomItemAbilityEffect : EffectSO
    {
        public List<BasicAbilityChange_Wearable_SMS> slapAbilities;
        public List<ExtraAbility_Wearable_SMS> extraAbilities;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var ab = new List<AbilitySO>();
            var usedAb = new List<AbilitySO>();

            if (slapAbilities != null)
                ab.AddRange(slapAbilities.Select(x => x.BasicAbility.ability));

            if(extraAbilities != null)
                ab.AddRange(extraAbilities.Select(x => x.ExtraAbility.ability));

            while (usedAb.Count < entryVariable && ab.Count > 0)
            {
                var idx = Random.Range(0, ab.Count);

                var a = ab[idx];
                ab.RemoveAt(idx);

                if(a != null)
                    usedAb.Add(a);
            }

            foreach(var a in usedAb)
            {
                if (caster.TryPerformRandomAbility(a))
                    exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
