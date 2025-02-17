using Pentacle.CustomEvent.Args;
using Pentacle.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class TiderunnerCheckCondition : EffectorConditionSO
    {
        public AbilityPlacement placementReq;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return args is AbilityUsedContext ctx && effector is IUnit u && u.Abilities() is List<CombatAbility> abs && placementReq switch
            {
                AbilityPlacement.Middle => abs.Count <= 1 || (ctx.abilityIndex > 0 && ctx.abilityIndex < abs.Count - 1),
                AbilityPlacement.Left => abs.Count > 1 && ctx.abilityIndex == 0,
                AbilityPlacement.Right => abs.Count > 1 && ctx.abilityIndex == abs.Count - 1,

                _ => false
            };
        }

        public enum AbilityPlacement
        {
            Left,
            Middle,
            Right
        };
    }
}
