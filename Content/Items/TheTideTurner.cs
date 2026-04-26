using Pentacle.Builders;
using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.Content.Effect;
using BOStuffPack.Content.StaticModifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class TheTideTurner
    {
        public static void Init()
        {
            var name = "The Tide Turner";
            var flav = "\"Close enough.\"";
            var desc = "Upon another party member using an ability, move this party member towards them, unless this party member is Constricted.\nAdds \"Anchor\" as an additional ability.";

            var abilityName = "Anchor";
            var abilityDesc = "If this party member isn't Constricted, refresh them. Apply 1 Constricted to this party member's position.";

            var ab = NewAbility("Anchor_A")
                .SetBasicInformationCharacter(abilityName, abilityDesc, "AttackIcon_Anchor")
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
                .CharacterAbility(Pigments.Yellow);

            var itm = NewItem<MultiCustomTriggerEffectWearable>("TheTideTurner_TW")
                .SetBasicInformation(name, flav, desc, "TheTideTurner")
                .SetPrice(5)
                .SetStaticModifiers(ExtraAbilityModifier(ab))
                .AddToTreasure();

            itm.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnAnyAbilityUsed.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new MoveSenderTowardsUnitValueTriggerEffect(),
                    conditions = new()
                    {
                        CreateScriptable<UnitValueSideCheckEffectorCondition>(x => x.neededSide = UnitValueSideCheckEffectorCondition.UnitSide.SameAsCaster),
                        CreateScriptable<ContainsFieldEffectEffectorCondition>(x =>
                        {
                            x.fieldEffectCheck = StatusField.Constricted;
                            x.useNotContains = true;
                        })
                    }
                },
            };
        }
    }
}
