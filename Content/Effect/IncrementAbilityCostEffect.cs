using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class IncrementAbilityCostEffect : EffectSO
    {
        public string abilityTarget;
        public ManaColorSO[] costAddition;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            var abs = caster.Abilities();

            if (abs == null)
                return false;

            foreach(var ab in abs)
            {
                if (ab == null || ab.ability == null)
                    continue;

                if (ab.ability.name != abilityTarget)
                    continue;

                ab.cost = [.. ab.cost, .. costAddition];
                exitAmount++;
            }

            if(exitAmount > 0)
                CombatManager.Instance.AddUIAction(new CharacterUpdateAllAttacksUIAction(caster.ID, caster.Abilities().ToArray()));

            return exitAmount > 0;
        }
    }
}
