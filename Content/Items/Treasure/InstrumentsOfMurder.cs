using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.Content.StoredValues;
using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class InstrumentsOfMurder
    {
        public static void Init()
        {
            var name = "Instruments of Murder";
            var flav = "\"I take my E and think about my setbacks.\"";
            var desc = "Non-ruptured damage dealt by this party member increases the target's \"Blood\" count instead. Adds \"Murder Everyone You Know\" as an additional ability.";

            var abName = "Murder Everyone You Know";
            var abDesc = "Deal direct ruptured damage to each enemy and party member equal to their \"Blood\" count.";

            var ab = NewAbility("MurderEveryoneYouKnow_A")
                .SetBasicInformation(abName, abDesc, "AttackIcon_Murder")
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<PlayAnimationOnAllTargetsFulfillingStoredValueConditionEffect>(x =>
                    {
                        x.storedValueID = StuffPackStoredValues.StoredValue_Blood._UnitStoreDataID;
                        x.storedValueCondition = IntCondition.Positive;
                        x.visuals = Visuals.Slash;
                    }), 0, Targeting.AllUnits),
                    Effects.GenerateEffect(CreateScriptable<DamageByTargetStoredValueEffect>(x =>
                    {
                        x.storedValueID = StuffPackStoredValues.StoredValue_Blood._UnitStoreDataID;
                        x.damageType = CombatType_GameIDs.Dmg_Ruptured.ToString();
                    }), 1, Targeting.AllUnits)
                })
                .AddIntent(Targeting.Unit_AllOpponents, IntentForDamage(1999))
                .AddIntent(Targeting.Unit_AllAllies, IntentForDamage(1999))
                .AddToCharacterDatabase()
                .CharacterAbility(Pigments.RedBlue, Pigments.RedBlue, Pigments.RedBlue);

            var item = NewItem<MultiCustomTriggerEffectWearable>("InstrumentsOfMurder_TW")
                .SetBasicInformation(name, flav, desc, "InstrumentsOfMurder")
                .AddToTreasure()
                .SetStaticModifiers(ExtraAbilityModifier(ab))
                .AddItemTypes(ItemType_GameIDs.Knife.ToString());

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnAnyoneBeingDamaged,
                    doesPopup = true,
                    immediate = true,

                    effect = new ConvertDamageToUnitStoredValueTriggerEffect(1, StuffPackStoredValues.StoredValue_Blood._UnitStoreDataID),

                    conditions = new()
                    {
                        CreateScriptable<UnitValueMatchesSenderEffectorCondition>(x => x.unitValueIndex = 0),
                        CreateScriptable<StringHolderValueDoesntMatchEffectorCondition>(x =>
                        {
                            x.stringValueIndex = 0;
                            x.value = CombatType_GameIDs.Dmg_Ruptured.ToString();
                        })
                    }
                }
            });
        }
    }
}
