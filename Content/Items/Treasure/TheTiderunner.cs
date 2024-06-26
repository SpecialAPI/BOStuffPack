using BOStuffPack.Content.StaticModifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class TheTiderunner
    {
        public static void Init()
        {
            var name = "The Tiderunner";
            var flav = "\"Smooth Sailing\"";
            var desc = "Using the leftmost ability moves this party member to the left. Using the rightmost ability moves this party member to the right.\nThis party member's outer ranked abilities are moved to the edges of the abilities list.\nAdds \"Anchor\" as an additional ability.";

            var abilityName = "Anchor";
            var abilityDesc = "Apply 1 Constricted to this party member's position.\nIf this party member wasn't Constricted before, refresh this party member.";

            var ability = NewAbility(abilityName, abilityDesc, "AttackIcon_Anchor", new()
            {
                Effect(Self, CreateScriptable<UnitFieldEffectCheckEffect>(x => x.field = StatusField_GameIDs.Constricted_ID.ToString())),

                Effect(Self, ApplyConstricted, 1),
                Effect(Self, CreateScriptable<RefreshAbilityUseEffect>()).WithCondition(Previous(2, false))
            }, new()
            {
                TargetIntent(Self, IntentType_GameIDs.Field_Constricted.ToString(), IntentType_GameIDs.Misc.ToString())
            }).WithVisuals(Visuals_Resolve, Self).Character(Pigments.Yellow);

            var itm = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "TheTiderunner").AddToTreasure().AddModifiers(ModdedData("TiderunnerStaticModifier", new TiderunnerStaticModifier()), ExtraAbility(ability)).Build();

            itm.triggerEffects = new()
            {
                new()
                {
                    trigger = CustomEvents.ON_ABILITY_PERFORMED_CONTEXT,
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        new()
                        {
                            effect = MoveRight,
                            targets = Self,
                            condition = null,
                            entryVariable = 0
                        }
                    }),

                    conditions = new() { CreateScriptable<TiderunnerCheckCondition>(x => x.placementReq = TiderunnerCheckCondition.AbilityPlacement.Right )}
                },

                new()
                {
                    trigger = CustomEvents.ON_ABILITY_PERFORMED_CONTEXT,
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        new()
                        {
                            effect = MoveLeft,
                            targets = Self,
                            condition = null,
                            entryVariable = 0
                        }
                    }),

                    conditions = new() { CreateScriptable<TiderunnerCheckCondition>(x => x.placementReq = TiderunnerCheckCondition.AbilityPlacement.Left )}
                }
            };
        }
    }
}
