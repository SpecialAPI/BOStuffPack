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
        public static readonly string ID = "InstrumentsOfMurder_TW".Prefix();
        public static readonly string AbilityID = "MurderEveryoneYouKnow_A".Prefix();
        public static readonly string MurderSVDB = "Murder_USD".Prefix();
        public static readonly string MurderSVID = "Murder".Prefix();
        public static readonly string DisableSVDB = "MurderTempDisable_USD".Prefix();
        public static readonly string DisableSVID = "MurderTempDisable".Prefix();

        public static void Init()
        {
            var incOnDamage = 5;

            var name = "Instruments of Murder";
            var flav = "\"I take my E and think about my setbacks.\"";
            var desc = $"Upon this party member dealing damage, increase the target's \"Murder\" count by {incOnDamage}. Adds \"Murder Everyone You Know\" as an additional ability.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "InstrumentsOfMurder")
                .AddToTreasure()
                .AddItemTypes(ItemType_GameIDs.Knife.ToString());

            NewStoredValue<AdvancedStoredValueIntInfo>(MurderSVDB, MurderSVID)
                .SetColor(StoredValueColor_Negative)
                .SetFormat("Murder: {0}");

            NewStoredValue<UnitStoreData_BasicSO>(DisableSVDB, DisableSVID);

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnTargetDamaged,
                    doesPopup = true,
                    immediate = true,

                    effect = new UnitValueStoredValueChangeTriggerEffect(MurderSVID, incOnDamage, IntOperation.Add, 1),

                    conditions = new()
                    {
                        StoredValueComparisonEffectorCondition.Create(DisableSVID, 0, IntComparison.Equal)
                    }
                }
            });

            var abName = "Murder Everyone You Know";
            var abDesc = "Deal damage to each enemy and party member equal to their \"Murder\" count. Reset their \"Murder\" count.";

            var unitsWithSV = Targeting.AllUnits
                .FilterUnitByStoredValueComparison(MurderSVID, 0, IntComparison.GreaterThan);

            var ab = NewAbility(AbilityID)
                .SetBasicInformationCharacter(abName, abDesc, "AttackIcon_Murder")
                .SetVisuals(Visuals.InvadeTheVeins, unitsWithSV)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CommonEffects.SetCasterStoredValue(DisableSVID), 1),
                    Effects.GenerateEffect(DamageByTargetStoredValueEffect.Create(MurderSVID), 1, unitsWithSV),
                    Effects.GenerateEffect(CommonEffects.SetCasterStoredValue(DisableSVID), 0),

                    Effects.GenerateEffect(TargetSetStoredValueEffect.Create(MurderSVID), 0, Targeting.AllUnits)
                })
                .SetIntents(
                [
                    TargetIntent(Targeting.Unit_AllAllies,    IntentType_GameIDs.Misc_Hidden.ToString()),
                    TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Misc_Hidden.ToString()),

                    ..LocalIntentTools.DamageIntentsWithStoredValueFilter(Targeting.Unit_AllAllies, MurderSVID),
                    ..LocalIntentTools.DamageIntentsWithStoredValueFilter(Targeting.Unit_AllOpponents, MurderSVID),

                    TargetIntent(Targeting.Unit_AllAllies.FilterUnitByStoredValueComparison(MurderSVID, 0, IntComparison.GreaterThan), IntentType_GameIDs.Misc.ToString()),
                    TargetIntent(Targeting.Unit_AllOpponents.FilterUnitByStoredValueComparison(MurderSVID, 0, IntComparison.GreaterThan), IntentType_GameIDs.Misc.ToString()),
                ])
                .CharacterAbility(Pigments.RedBlue, Pigments.RedBlue, Pigments.RedBlue);

            item.SetStaticModifiers(ExtraAbilityModifier(ab));
        }
    }
}
