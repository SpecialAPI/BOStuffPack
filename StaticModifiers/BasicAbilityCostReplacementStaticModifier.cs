using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.StaticModifiers
{
    public class BasicAbilityCostReplacementStaticModifier : WearableStaticModifierSetterSO
    {
        public ManaColorSO newCost;
        public int minCost = -1;
        public int maxCost = -1;

        public override void OnAttachedToCharacter(WearableStaticModifiers modifiers, CharacterSO character, int rank)
        {
            var basicAbility = character.basicCharAbility != null ?
                character.basicCharAbility :
                LoadedDBsHandler.AbilityDB.SlapAbility;

            var numCost = basicAbility.cost?.Length ?? 0;
            if (minCost >= 0)
                numCost = Mathf.Max(numCost, minCost);
            if (maxCost >= 0)
                numCost = Math.Min(numCost, maxCost);

            var cost = new ManaColorSO[numCost];
            for (var i = 0; i < numCost; i++)
                cost[i] = newCost;

            modifiers.BasicAbilityModifier = new()
            {
                ability = basicAbility.ability,
                cost = cost
            };
        }

        public override void OnDettachedFromCharacter(WearableStaticModifiers modifiers)
        {
            modifiers.BasicAbilityModifier = null;
        }

        public static WearableStaticModifierSetterSO Create(ManaColorSO newCost, int minCost = -1, int maxCost = -1)
        {
            var sms = CreateScriptable<BasicAbilityCostReplacementStaticModifier>();
            sms.newCost = newCost;
            sms.minCost = minCost;
            sms.maxCost = maxCost;

            return sms;
        }
    }
}
