using BOStuffPack.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using BOStuffPack.Effect;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class InstrumentsOfMurder
    {
        public static void Init()
        {
            var name = "Instruments of Murder";
            var flav = "\"I take my E and think about my setbacks.\"";
            var desc = "Non-ruptured damage dealt by this party member increases the target's \"Blood\" count instead. Adds \"Murder Everyone You Know\" as an additional ability.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ItemIDs.InstrumentsOfMurder)
                .SetBasicInformation(name, flav, desc, "InstrumentsOfMurder")
                .AddToTreasure()
                .AddItemTypes(ItemType_GameIDs.Knife.ToString());

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnTargetBeingDamaged,
                    doesPopup = true,
                    immediate = true,

                    effect = new ConvertDamageToUnitStoredValueTriggerEffect(1, LocalStoredValues.Blood._UnitStoreDataID),

                    conditions = new()
                    {
                        CreateScriptable<StringHolderValueDoesntMatchEffectorCondition>(x =>
                        {
                            x.stringValueIndex = 0;
                            x.value = CombatType_GameIDs.Dmg_Ruptured.ToString();
                        })
                    }
                }
            });

            var abName = "Murder Everyone You Know";
            var abDesc = "Deal direct ruptured damage to each enemy and party member equal to their \"Blood\" count.";

            var ab = NewAbility(AbilityIDs.MurderEveryoneYouKnow)
                .SetBasicInformationCharacter(abName, abDesc, "AttackIcon_Murder")
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<PlayAnimationOnAllTargetsFulfillingStoredValueConditionEffect>(x =>
                    {
                        x.storedValueID = LocalStoredValues.Blood._UnitStoreDataID;
                        x.storedValueCondition = IntCondition.Positive;
                        x.visuals = Visuals.Slash;
                    }), 0, Targeting.AllUnits),
                    Effects.GenerateEffect(CreateScriptable<DamageByTargetStoredValueEffect>(x =>
                    {
                        x.storedValueID = LocalStoredValues.Blood._UnitStoreDataID;
                        x.damageType = CombatType_GameIDs.Dmg_Ruptured.ToString();
                    }), 1, Targeting.AllUnits)
                })
                .AddIntent(Targeting.Unit_AllOpponents, IntentForDamage(1999))
                .AddIntent(Targeting.Unit_AllAllies, IntentForDamage(1999))
                .CharacterAbility(Pigments.RedBlue, Pigments.RedBlue, Pigments.RedBlue);

            item.SetStaticModifiers(ExtraAbilityModifier(ab));
        }
    }
}
