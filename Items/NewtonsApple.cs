using BOStuffPack.CustomTrigger;
using FMOD;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking.Types;

namespace BOStuffPack.Items
{
    public static class NewtonsApple
    {
        public static readonly string ID = "NewtonsApple_TW".Prefix();

        public static void Init()
        {
            var name = "Newton's Apple";
            var flav = "\"Doesn't fall far from the tree.\"";
            var desc = "Before this party member performs an ability, move all enemies left of them to the right, then move all enemies right of them to the left.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "NewtonsApple")
                .SetPrice(3)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnBeforeAbilityAnimation,
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.SwapRight, 0, Targeting.Slot_OpponentAllLefts),
                        Effects.GenerateEffect(CommonEffects.SwapLeft, 0, Targeting.Slot_OpponentAllRights)
                    })
                }
            };
        }
    }
}
