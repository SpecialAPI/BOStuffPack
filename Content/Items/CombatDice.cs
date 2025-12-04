using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class CombatDice
    {
        public static void Init()
        {
            var name = "Combat Dice";
            var flav = "\"Or is it called a douse?\"";
            var desc = "Replaces \"Slap\" with \"Combat Roll\", a pigment rerolling ability with a chance to refresh.\nAdds \"Fury\" as an additional ability, an ability that applies Fury to this party member.";

            var itm = NewItem<BasicWearable>("CombatDice_TW")
                .SetBasicInformation(name, flav, desc, "CombatDice")
                .SetPrice(6)
                .AddToTreasure();

            var rollName = "Combat Roll";
            var rollDesc = "Generates 1 pigment of a random color not used for this ability.\n75% chance to refresh.";

            var roll = NewAbility("CombatRoll_A")
                .SetBasicInformation(rollName, rollDesc, "AttackIcon_CombatRoll")
                .SetVisuals(Visuals.Insult, Targeting.Slot_SelfSlot)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<ProducePigmentNotUsedForAbilityEffect>(x => x.pigmentColors = [Pigments.Yellow, Pigments.Red, Pigments.Blue, Pigments.Purple]), 1),
                    Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot, Effects.ChanceCondition(75))
                })
                .AddIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Mana_Generate.ToString(), IntentType_GameIDs.Other_Refresh.ToString())
                .AddToCharacterDatabase()
                .CharacterAbility(Pigments.Grey);

            var furyName = "Fury";
            var furyDesc = "Apply 1 Fury to this party member.\nIf this party member didn't have Fury before, refresh this party member.";

            var fury = NewAbility("Fury_A")
                .SetBasicInformation(furyName, furyDesc, "AttackIcon_Fury")
                .SetVisuals(Visuals.Bosch, Targeting.Slot_SelfSlot)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<StatusEffectCheckerEffect>(x => x._status = CustomStatusEffects.Fury), 0, Targeting.Slot_SelfSlot),

                    Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = CustomStatusEffects.Fury), 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                })
                .AddIntent(Targeting.Slot_SelfSlot, StatusFieldIntents.Status_Fury, IntentType_GameIDs.Other_Refresh.ToString())
                .AddToCharacterDatabase()
                .CharacterAbility(Pigments.Red, Pigments.Red, Pigments.Red);

            itm.SetStaticModifiers(BasicAbilityModifier(roll), ExtraAbilityModifier(fury));
        }
    }
}
