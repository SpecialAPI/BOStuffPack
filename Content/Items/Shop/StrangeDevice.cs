using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Shop
{
    public static class StrangeDevice
    {
        public static void Init()
        {
            var name = "Strange Device";
            var flav = "\"157\"";
            var desc = "All random events are now predetermined.\nAdds \"Pillars of the World\" as an additional ability.";

            var pillarsName = "Pillars of the World";
            var pillarsDesc = "Resets the predetermined outcomes of random events.\n75% chance to refresh this party member.";

            var pillars =
                NewAbility(pillarsName, pillarsDesc, "AttackIcon_PillarsOfTheWorld", new()
                {
                    Effect(null, CreateScriptable<SetRandomSeedEffect>(), 157),

                    Effect(Self, CreateScriptable<RefreshAbilityUseEffect>()).WithCondition(Chance(75)),
                },
                new()
                {
                    TargetIntent(Self, IntentType_GameIDs.Misc.ToString(), IntentType_GameIDs.Misc.ToString())
                })

                .WithVisuals(Visuals_Strings, Absolute(false, 0))
                .Character(Pigments.Yellow);

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "StrangeDevice").AddToShop(1).AddModifiers(ExtraAbility(pillars)).Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeforeCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(null, CreateScriptable<SetRandomSeedEffect>(), 157),
                    })
                }
            };
        }
    }
}
