using BOStuffPack.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using BOStuffPack.Effect;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class Knives
    {
        public static readonly string ID = "Knives_TW".Prefix();
        public static readonly string AbilityID = "Knives_A".Prefix();
        public static readonly string KnivesSVID = "Knives".Prefix();
        public static readonly string DisableSVID = "KnivesDisable".Prefix();

        public static void Init()
        {
            var incOnDamage = 5;

            var abName = "Knives";
            var abDesc = "Deal damage to each enemy and party member equal to their \"Knives\". Reset their \"Knives\".";

            var name = "Knives";
            var flav = "\"Knives.\"";
            var desc = $"Upon this party member dealing damage, increase the target's \"Knives\" by {incOnDamage}. Adds \"{abName}\" as an additional ability.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "Knives")
                .AddToTreasure()
                .AddItemTypes(ItemType_GameIDs.Knife.ToString());

            NewStoredValue<AdvancedStoredValueIntInfo>(KnivesSVID)
                .SetColor(StoredValueColor_Negative)
                .SetFormat("Knives: {0}");

            NewStoredValue<UnitStoreData_BasicSO>(DisableSVID);

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnTargetDamaged,
                    doesPopup = true,
                    immediate = true,

                    effect = new TargetStoredValueChangeTriggerEffect(KnivesSVID, incOnDamage, IntOperation.Add, 1),

                    conditions = new()
                    {
                        StoredValueComparisonEffectorCondition.Create(DisableSVID, 0, IntComparison.Equal)
                    }
                }
            });

            var unitsWithSV = Targeting.AllUnits
                .FilterUnitByStoredValueComparison(KnivesSVID, 0, IntComparison.GreaterThan);

            var ab = NewAbility(AbilityID)
                .SetBasicInformationCharacter(abName, abDesc, "AttackIcon_Knives")
                .SetVisuals(Visuals.InvadeTheVeins, unitsWithSV)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CommonEffects.SetCasterStoredValue(DisableSVID), 1),
                    Effects.GenerateEffect(DamageByTargetStoredValueEffect.Create(KnivesSVID), 1, unitsWithSV),
                    Effects.GenerateEffect(CommonEffects.SetCasterStoredValue(DisableSVID), 0),

                    Effects.GenerateEffect(TargetSetStoredValueEffect.Create(KnivesSVID), 0, Targeting.AllUnits)
                })
                .SetIntents(
                [
                    TargetIntent(Targeting.Unit_AllAllies,    IntentType_GameIDs.Misc_Hidden.ToString()),
                    TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Misc_Hidden.ToString()),

                    ..LocalIntentTools.DamageIntentsWithStoredValueFilter(Targeting.Unit_AllAllies, KnivesSVID),
                    ..LocalIntentTools.DamageIntentsWithStoredValueFilter(Targeting.Unit_AllOpponents, KnivesSVID),

                    TargetIntent(Targeting.Unit_AllAllies.FilterUnitByStoredValueComparison(KnivesSVID, 0, IntComparison.GreaterThan), IntentType_GameIDs.Misc.ToString()),
                    TargetIntent(Targeting.Unit_AllOpponents.FilterUnitByStoredValueComparison(KnivesSVID, 0, IntComparison.GreaterThan), IntentType_GameIDs.Misc.ToString()),
                ])
                .CharacterAbility(Pigments.RedBlue, Pigments.RedBlue, Pigments.RedBlue);

            item.SetStaticModifiers(ExtraAbilityModifier(ab));
        }
    }
}
