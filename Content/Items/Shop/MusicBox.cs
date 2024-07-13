using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Shop
{
    public static class MusicBox
    {
        public static void Init()
        {
            var name = "Abandoned Music Box";
            var flav = "\"A chaotic cacophony\"";
            var desc = "At the start of combat, add an ability to this party member that performs all of their abilities. The cost for the ability is set to the combined cost of every ability currently on this party member.";

            var abilityName = "Chaos Melody";
            var abilityDesc = "Make this party member perform all of their abilities except this one. Abilities will be performed as if this party member's position is shifted to that ability's position relative to the position of the middle ability.";

            var ability =
                NewAbility(abilityName, abilityDesc, "AttackIcon_Everything", new()
                {
                    Effect(null, CreateScriptable<PerformAllAbilitiesButThisOffsettedEffect>())
                },

                new()
                {
                    TargetIntent(Self, IntentType_GameIDs.Misc.ToString())
                })

                .Extra();

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "MusicBox").AddToShop(4).Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(null, CreateScriptable<AddExtraAbilityWithCombinedCostEffect>(x => x.ability = ability))
                    })
                }
            };
        }
    }
}
