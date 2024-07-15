using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StatusEffect.Effects
{
    public class TargetSwapStatusEffect : StatusEffect_SO
    {
        public static List<string> BasegameAbilitiesIgnoredForTargetSwap = new()
        {
            // Burnout's "Exit" ability (applies the Fleeting passive)
            "Exit_1_A",
            "Exit_2_A",
            "Exit_3_A",
            "Exit_4_A",

            // Shatter instantly kills
            "Shatter_1_A",

            // Final boss instant kills
            "ComeHome_A",
            "MortalHorizon_A",

            // Make an example (I love instantly killing X&G!!!)
            "MakeAnExample_A",

            // Omnipresent (dont even want to think about this one....)
            "Omnipresent_A",

            // Heir would not have enouh space to spawn
            "HisChildisBorn_A"
        };

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, CustomEvents.MODIFY_TARGETTING, caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, CustomEvents.MODIFY_TARGETTING_INTENTS, caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_03, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, CustomEvents.MODIFY_TARGETTING, caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, CustomEvents.MODIFY_TARGETTING_INTENTS, caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_03, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (args is not ModifyTargettingInfo hold)
                return;

            if (hold.ability is AdvancedAbilitySO adv && adv.Flags != null && adv.Flags.Contains("Ignore_TargetSwap"))
                return;

            if (BasegameAbilitiesIgnoredForTargetSwap.Contains(hold.ability.name))
                return;

            hold.boolReference.value = !hold.boolReference.value;
        }

        public override void OnEventCall_03(StatusEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder, sender as IStatusEffector);
        }
    }
}
