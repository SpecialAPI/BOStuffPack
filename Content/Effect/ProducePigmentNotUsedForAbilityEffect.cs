using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ProducePigmentNotUsedForAbilityEffect : EffectSO
    {
        public List<ManaColorSO> pigmentColors;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (pigmentColors == null || pigmentColors.Count <= 0 || entryVariable <= 0)
                return false;

            var abilityUsedColors = caster.Ext().PigmentUsedForAbility;
            var pool = new List<ManaColorSO>(pigmentColors);

            if(abilityUsedColors != null && abilityUsedColors.Count >= 0)
            {
                for(var i = 0; i < pool.Count; i++)
                {
                    if (!abilityUsedColors.Exists(x => x.pigmentID == pool[i].pigmentID))
                        continue;

                    pool.RemoveAt(i);
                    i--;
                }
            }

            if(pool.Count <= 0)
                pool.AddRange(pigmentColors);

            exitAmount = entryVariable;
            var index = -1;
            var amount = 1;

            for (var i = 0; i < entryVariable; i++)
            {
                var rngColor = Random.Range(0, pool.Count);

                if (index == -1)
                {
                    index = rngColor;
                    continue;
                }

                if (index == rngColor)
                {
                    amount++;
                    continue;
                }

                CombatManager.Instance.ProcessImmediateAction(new AddManaToManaBarAction(pool[index], amount, caster.IsUnitCharacter, caster.ID));
                index = rngColor;
                amount = 1;
            }

            if (index >= 0)
                CombatManager.Instance.ProcessImmediateAction(new AddManaToManaBarAction(pool[index], amount, caster.IsUnitCharacter, caster.ID));

            return true;
        }
    }
}
