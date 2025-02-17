using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class FailRounds
    {
        public static void Init()
        {
            //var name = "Failed Round";
            //var flav = "\"Misfire\"";
            //var desc = "This party member is 1 level higher than they would be otherwise.\nOn combat start, inflict 1 TargetSwap to this party member.";
            //
            //var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "FailRounds").AddModifiers(CreateScriptable<RankChange_Wearable_SMS>(x => x._rankAdditive = 1)).AddToTreasure().Build();
            //
            //item.triggerEffects = new()
            //{
            //    new()
            //    {
            //        trigger = TriggerCalls.OnCombatStart.ToString(),
            //        doesPopup = true,
            //
            //        effect = new PerformEffectTriggerEffect(new()
            //        {
            //            Effect(Self, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = TargetSwap), 1)
            //        })
            //    }
            //};
        }
    }
}
