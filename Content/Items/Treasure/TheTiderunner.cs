using Pentacle.Builders;
using Pentacle.CustomEvent;
using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.Content.Effect;
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
            var flav = "\"Smooth sailing.\"";
            var desc = "Using the leftmost or rightmost ability moves this party member in the same direction, unless they are Constricted.\nAdds \"Anchor\" as an additional ability. This party member's base abilities (except Slap) are moved to the edges of the abilities list.";

            var abilityName = "Anchor";
            var abilityDesc = "Apply 1 Constricted to this party member's position.\nIf this party member wasn't Constricted before, refresh this party member.";

            var ab = NewAbility("Anchor_A")
                .SetBasicInformation(abilityName, abilityDesc, "AttackIcon_Anchor")
                .SetVisuals(Visuals.Resolve, Targeting.Slot_SelfSlot)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<UnitFieldEffectCheckEffect>(x => x.field = StatusField_GameIDs.Constricted_ID.ToString()), 0, Targeting.Slot_SelfAll),

                    Effects.GenerateEffect(CreateScriptable<FieldEffect_Apply_Effect>(x => x._Field = StatusField.Constricted), 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2))
                })
                .SetIntents(new()
                {
                    TargetIntent(Targeting.Slot_SelfAll, IntentType_GameIDs.Field_Constricted.ToString()),
                    TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Refresh.ToString())
                })
                .AddToCharacterDatabase()
                .CharacterAbility(Pigments.Yellow);

            var itm = NewItem<MultiCustomTriggerEffectWearable>("TheTiderunner_TW")
                .SetBasicInformation(name, flav, desc, "TheTiderunner")
                .SetPrice(5)
                .SetStaticModifiers(ModdedDataModifier<TiderunnerStaticModifier>(), ExtraAbilityModifier(ab))
                .AddToTreasure();

            itm.triggerEffects = new()
            {
                new()
                {
                    trigger = CustomTriggers.OnAbilityPerformedContext,
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<UnitFieldEffectCheckEffect>(x => x.field = StatusField_GameIDs.Constricted_ID.ToString()), 0, Targeting.Slot_SelfAll),
                        Effects.GenerateEffect(CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = false), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1))
                    }),
                    conditions = [CreateScriptable<TiderunnerCheckCondition>(x => x.placementReq = TiderunnerCheckCondition.AbilityPlacement.Left)]
                },

                new()
                {
                    trigger = CustomTriggers.OnAbilityPerformedContext,
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<UnitFieldEffectCheckEffect>(x => x.field = StatusField_GameIDs.Constricted_ID.ToString()), 0, Targeting.Slot_SelfAll),
                        Effects.GenerateEffect(CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = true), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1))
                    }),
                    conditions = [CreateScriptable<TiderunnerCheckCondition>(x => x.placementReq = TiderunnerCheckCondition.AbilityPlacement.Right)]
                }
            };
        }
    }
}
