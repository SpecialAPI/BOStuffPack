using BOStuffPack.CustomTrigger.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class BlankBookSetAbilityTriggerEffect : TriggerEffect
    {
        public string storedValue;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (args is not OnBeforeAbilityUsedContext ctx || ctx.combatAbility is not CombatAbility cAbility || cAbility.ability == null)
                return;

            if (sender is not IUnit u)
                return;

            u.TryGetStoredData(storedValue, out var svHolder);
            svHolder.m_ObjectData = ctx.combatAbility;
        }
    }
}
