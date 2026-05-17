using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.StoredValues
{
    public static class LocalStoredValues
    {
        public static UnitStoreData_BasicSO StoredValue_MergedCount;
        public static UnitStoreData_BasicSO StoredValue_KeybladeRTurn;
        public static UnitStoreData_BasicSO StoredValue_KeybladeBTurn;
        public static UnitStoreData_BasicSO StoredValue_KeybladeYTurn;
        public static UnitStoreData_BasicSO StoredValue_KeybladePTurn;
        public static UnitStoreData_BasicSO StoredValue_Blood;
        public static UnitStoreData_BasicSO StoredValue_UnnamedItem5;
        public static UnitStoreData_BasicSO StoredValue_UnnamedItem42TempDisable;
        public static UnitStoreData_BasicSO StoredValue_FramedE;

        public static UnitStoreData_BasicSO StoredValue_MergingStones;
        public static UnitStoreData_BasicSO StoredValue_BlankBookAbility;
        public static UnitStoreData_BasicSO StoredValue_BlankBookPassive;
        public static UnitStoreData_BasicSO StoredValue_UnnamedItem31;

        public static void Init()
        {
            StoredValue_MergedCount = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.MergedCountDB, StoredValueIDs.MergedCountID).SetColor(StoredValueColor_Negative).SetFormat("Merged Enemies: {0}");
            StoredValue_KeybladeRTurn = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.KeybladeRTurnDB, StoredValueIDs.KeybladeRTurnID).SetColor(StoredValueColor_Negative).SetFormat("Keyblade R Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            StoredValue_KeybladeBTurn = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.KeybladeBTurnDB, StoredValueIDs.KeybladeBTurnID).SetColor(StoredValueColor_Negative).SetFormat("Keyblade B Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            StoredValue_KeybladeYTurn = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.KeybladeYTurnDB, StoredValueIDs.KeybladeYTurnID).SetColor(StoredValueColor_Negative).SetFormat("Keyblade Y Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            StoredValue_KeybladePTurn = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.KeybladePTurnDB, StoredValueIDs.KeybladePTurnID).SetColor(StoredValueColor_Negative).SetFormat("Keyblade P Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            StoredValue_Blood = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.BloodDB, StoredValueIDs.BloodID).SetColor(StoredValueColor_Negative).SetFormat("Blood: {0}");
            StoredValue_UnnamedItem5 = NewStoredValue<UnitStoreData_BasicSO>(StoredValueIDs.UnnamedItem5DB, StoredValueIDs.UnnamedItem5ID);
            StoredValue_UnnamedItem42TempDisable = NewStoredValue<UnitStoreData_BasicSO>(StoredValueIDs.UnnamedItem42TempDisableDB, StoredValueIDs.UnnamedItem42TempDisableID);
            StoredValue_FramedE = NewStoredValue<UnitStoreData_BasicSO>(StoredValueIDs.FramedEDB, StoredValueIDs.FramedEID);

            StoredValue_MergingStones = NewStoredValue<MergingStonesStoredValue>(StoredValueIDs.MergingStonesDB, StoredValueIDs.MergingStonesID).SetColor(StoredValueColor_Rare).SetFormat("Merging Stones: {0}");
            StoredValue_BlankBookAbility = NewStoredValue<CombatAbilityStoredValue>(StoredValueIDs.BlankBookAbilityDB, StoredValueIDs.BlankBookAbilityID).SetColor(StoredValueColor_Rare).SetFormat("Last used ability: {0}");
            StoredValue_BlankBookPassive = NewStoredValue<PassiveAbilityStoredValue>(StoredValueIDs.BlankBookPassiveDB, StoredValueIDs.BlankBookPassiveID).SetColor(StoredValueColor_Rare).SetFormat("Last used passive: {0}");
            StoredValue_UnnamedItem31 = NewStoredValue<IntEnumerableStoredValue>(StoredValueIDs.UnnamedItem31DB, StoredValueIDs.UnnamedItem31ID).SetColor(StoredValueColor_Negative).SetFormat("Already dealt: {0}").SetSortOrder(IntEnumerableStoredValue.IntSortOrder.Ascending);
        }

        public static bool CurrentTurnIsLowerThanValueDisplayCondition(UnitStoreDataHolder holder)
        {
            return holder.m_MainData >= CombatManager.Instance._stats.TurnsPassed + 1;
        }
    }
}
