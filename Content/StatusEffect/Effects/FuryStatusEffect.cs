using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StatusEffect.Effects
{
    public class FuryStatusEffect : StatusEffect_SO
    {
        public override bool IsPositive => true;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, CustomEvents.ON_BEFORE_ABILITY_EFFECTS, caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, CustomEvents.ON_BEFORE_ABILITY_EFFECTS, caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if(args is AbilityUsedContext ctx && sender is IUnit unit && sender is IStatusEffector effector && ctx.ability != null && (ctx.ability is not AdvancedAbilitySO adv || adv.Flags == null || !adv.Flags.Contains("NoFuryRepeat")))
            {
                var repeats = holder.m_ContentMain + holder.Restrictor;

                for(int i = 0; i < repeats; i++)
                {
                    CombatManager.Instance.AddSubAction(new EffectAction(ctx.ability.effects, unit));
                    CombatManager.Instance.AddSubAction(new ReduceStatusDurationAction(this, holder, effector));
                }
            }
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder, sender as IStatusEffector);
        }
    }
}
