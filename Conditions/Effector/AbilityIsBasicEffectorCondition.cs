using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class AbilityIsBasicEffectorCondition : EffectorConditionSO
    {
        public bool needsToBeBasic;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(args is not AbilityUsageReference abRef || abRef.m_AbilityUsed == null)
                return false;

            var basicAbilityId = "";
            if (effector is CharacterCombat cc && cc.BasicAbility() is CharacterAbility ab)
                basicAbilityId = ab.ability.name;

            var isBasic = abRef.m_AbilityUsed.name == basicAbilityId;

            return isBasic == needsToBeBasic;
        }

        public static EffectorConditionSO Create(bool needsToBeBasic)
        {
            var ec = CreateScriptable<AbilityIsBasicEffectorCondition>();
            ec.needsToBeBasic = needsToBeBasic;

            return ec;
        }
    }
}
