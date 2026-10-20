using Pentacle.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Conditions.Effector
{
    public class AbilityPlacementCheckEffectorCondition : EffectorConditionSO
    {
        public AbilityPlacement neededPlacement;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return args is AbilityUsedContext ctx && effector is IUnit u && u.Abilities() is List<CombatAbility> abs && neededPlacement switch
            {
                AbilityPlacement.Middle => abs.Count <= 1 || ctx.abilityIndex > 0 && ctx.abilityIndex < abs.Count - 1,
                AbilityPlacement.Left => abs.Count > 1 && ctx.abilityIndex == 0,
                AbilityPlacement.Right => abs.Count > 1 && ctx.abilityIndex == abs.Count - 1,

                _ => false
            };
        }

        public static EffectorConditionSO Create(AbilityPlacement neededPlacement)
        {
            var ec = CreateScriptable<AbilityPlacementCheckEffectorCondition>();
            ec.neededPlacement = neededPlacement;

            return ec;
        }

        public enum AbilityPlacement
        {
            Left,
            Middle,
            Right
        };
    }
}
