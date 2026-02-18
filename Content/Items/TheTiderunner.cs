using Pentacle.Builders;
using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.Content.Effect;
using BOStuffPack.Content.StaticModifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class TheTiderunner
    {
        public static void Init()
        {
            var name = "The Tiderunner";
            var flav = "\"Smooth sailing.\"";
            var desc = "Upon the leftmost or rightmost party member using an ability, this party member is moved to the left or right respectively, unless they are Constricted.\nAdds \"Anchor\" as an additional ability.";

            var abilityName = "Anchor";
            var abilityDesc = "If this party member isn't Constricted, refresh them. Apply 1 Constricted to this party member's position.";

            var ab = NewAbility("Anchor_A")
                .SetBasicInformation(abilityName, abilityDesc, "AttackIcon_Anchor")
                .SetVisuals(Visuals.Resolve, Targeting.Slot_SelfSlot)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<UnitFieldEffectCheckEffect>(x => x.field = StatusField_GameIDs.Constricted_ID.ToString()), 0, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),

                    Effects.GenerateEffect(CreateScriptable<FieldEffect_Apply_Effect>(x => x._Field = Status.Constricted), 1, Targeting.Slot_SelfAll)
                })
                .SetIntents(new()
                {
                    TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Refresh.ToString()),
                    TargetIntent(Targeting.Slot_SelfAll, IntentType_GameIDs.Field_Constricted.ToString())
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
                    trigger = TriggerCalls.OnAnyAbilityUsed.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<UnitFieldEffectCheckEffect>(x => x.field = StatusField_GameIDs.Constricted_ID.ToString()), 0, Targeting.Slot_SelfAll),
                        Effects.GenerateEffect(CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = false), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1))
                    }),
                    conditions = new()
                    {
                        CreateScriptable<UnitValueSideCheckEffectorCondition>(x => x.neededSide = UnitValueSideCheckEffectorCondition.UnitSide.SameAsCaster),
                        CreateScriptable<UnitValuePlacementCheckEffectorCondition>(x => x.neededPlacement = UnitValuePlacementCheckEffectorCondition.UnitPlacement.Leftmost)
                    }
                },

                new()
                {
                    trigger = TriggerCalls.OnAnyAbilityUsed.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<UnitFieldEffectCheckEffect>(x => x.field = StatusField_GameIDs.Constricted_ID.ToString()), 0, Targeting.Slot_SelfAll),
                        Effects.GenerateEffect(CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = true), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1))
                    }),
                    conditions = new()
                    {
                        CreateScriptable<UnitValueSideCheckEffectorCondition>(x => x.neededSide = UnitValueSideCheckEffectorCondition.UnitSide.SameAsCaster),
                        CreateScriptable<UnitValuePlacementCheckEffectorCondition>(x => x.neededPlacement = UnitValuePlacementCheckEffectorCondition.UnitPlacement.Rightmost)
                    }
                }
            };
        }
    }
}
