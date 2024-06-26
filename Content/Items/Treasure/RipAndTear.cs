using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class RipAndTear
    {
        public static void Init()
        {
            var name = "Rip and Tear";
            var flav = "\"Enemies have a 20% cha- oh wait, wrong game\"";
            var desc = "Upon killing an enemy, apply 2 Fury to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "RipAndTear").AddToTreasure().Build();
            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnKill.ToString(),
                    doesPopup = true,
                    
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(Self, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Fury), 2)
                    })
                }
            };
        }
    }
}
