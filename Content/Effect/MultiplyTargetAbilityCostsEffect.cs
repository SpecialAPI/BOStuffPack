using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class MultiplyTargetAbilityCostsEffect : EffectSO
    {
        public List<string> ignoredAbilityIDs = [];

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            entryVariable = Mathf.Max(0, entryVariable);

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit || t.Unit is not CharacterCombat cc)
                    continue;

                var abs = cc.CombatAbilities;
                foreach(var ab in abs)
                {
                    if(ab == null || ab.ability == null || (ignoredAbilityIDs != null && ignoredAbilityIDs.Contains(ab.ability.name)))
                        continue;

                    var origCost = ab.cost;
                    var newCost = ab.cost = new ManaColorSO[ab.cost.Length * entryVariable];
                    for(var i = 0; i < newCost.Length; i++)
                    {
                        newCost[i] = origCost[i / entryVariable];
                    }
                }

                CombatManager.Instance.AddUIAction(new CharacterUpdateAllAttacksUIAction(cc.ID, abs.ToArray()));
                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
