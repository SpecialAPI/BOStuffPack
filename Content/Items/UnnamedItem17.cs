using BOStuffPack.Content.Conditions.Effector;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem17
    {
        public static void Init()
        {
            var name = "Unnamed Item 17";
            var flav = "\"WIP\"";
            var desc = "Grey pigment can now be produced. This party member has \"Petrify\" as an additional ability.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem17_TW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(9)
                .AddToTreasure();

            var abName = "Petrify";
            var abDesc = "Change the left ally's health color to grey.\nIf this fails or the left ally's color is already grey, deal 1 damage to the left ally.\nDamage dealt by this ability produces 1 more pigment than it normally would.";

            var ab = NewAbility("Petrify_A")
                .SetBasicInformation(abName, abDesc)
                .SetVisuals(Visuals.Scream, Targeting.Slot_AllyLeft)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<ChangeHealthColorFailIfAlreadyThatColorEffect>(x => x.healthColor = Pigments.Grey), 0, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(CreateScriptable<SpecialDamageEffect>(x => x.damageInfo = new() { ExtraPigment = 1 }), 1, Targeting.Slot_AllyLeft, Effects.CheckPreviousEffectCondition(false, 1)),
                })
                .AddIntent(Targeting.Slot_AllyLeft, IntentType_GameIDs.Mana_Modify.ToString(), IntentForDamage(1), IntentType_GameIDs.Mana_Generate.ToString())
                .AddToCharacterDatabase()
                .CharacterAbility(Pigments.Yellow, Pigments.Blue);

            item.SetStaticModifiers(ExtraAbilityModifier(ab));
            item.triggerEffects = new()
            {
                new()
                {
                    trigger = CustomTriggers.CanProducePigmentColor,
                    doesPopup = false,
                    immediate = true,

                    effect = new BoolHolderSetterTriggerffect(true),
                    conditions = new()
                    {
                        CreateScriptable<CanProducePigmentColorReferenceMatchesPigmentEffectorCondition>(x => x.pigment = Pigments.Grey)
                    }
                }
            };
        }
    }
}
