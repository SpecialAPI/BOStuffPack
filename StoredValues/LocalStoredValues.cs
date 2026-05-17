using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.StoredValues
{
    public static class LocalStoredValues
    {
        public static UnitStoreData_BasicSO MergedCount;
        public static UnitStoreData_BasicSO KeybladeRTurn;
        public static UnitStoreData_BasicSO KeybladeBTurn;
        public static UnitStoreData_BasicSO KeybladeYTurn;
        public static UnitStoreData_BasicSO KeybladePTurn;
        public static UnitStoreData_BasicSO Blood;
        public static UnitStoreData_BasicSO UnnamedItem5;
        public static UnitStoreData_BasicSO UnnamedItem42TempDisable;
        public static UnitStoreData_BasicSO FramedE;

        public static UnitStoreData_BasicSO MergingStones;
        public static UnitStoreData_BasicSO BlankBookAbility;
        public static UnitStoreData_BasicSO BlankBookPassive;
        public static UnitStoreData_BasicSO UnnamedItem31;

        public static void Init()
        {
            MergedCount = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.MergedCountDB, StoredValueIDs.MergedCountID).SetColor(StoredValueColor_Negative).SetFormat("Merged Enemies: {0}");
            KeybladeRTurn = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.KeybladeRTurnDB, StoredValueIDs.KeybladeRTurnID).SetColor(StoredValueColor_Negative).SetFormat("Keyblade R Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            KeybladeBTurn = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.KeybladeBTurnDB, StoredValueIDs.KeybladeBTurnID).SetColor(StoredValueColor_Negative).SetFormat("Keyblade B Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            KeybladeYTurn = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.KeybladeYTurnDB, StoredValueIDs.KeybladeYTurnID).SetColor(StoredValueColor_Negative).SetFormat("Keyblade Y Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            KeybladePTurn = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.KeybladePTurnDB, StoredValueIDs.KeybladePTurnID).SetColor(StoredValueColor_Negative).SetFormat("Keyblade P Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            Blood = NewStoredValue<AdvancedStoredValueIntInfo>(StoredValueIDs.BloodDB, StoredValueIDs.BloodID).SetColor(StoredValueColor_Negative).SetFormat("Blood: {0}");
            UnnamedItem5 = NewStoredValue<UnitStoreData_BasicSO>(StoredValueIDs.UnnamedItem5DB, StoredValueIDs.UnnamedItem5ID);
            UnnamedItem42TempDisable = NewStoredValue<UnitStoreData_BasicSO>(StoredValueIDs.UnnamedItem42TempDisableDB, StoredValueIDs.UnnamedItem42TempDisableID);
            FramedE = NewStoredValue<UnitStoreData_BasicSO>(StoredValueIDs.FramedEDB, StoredValueIDs.FramedEID);

            MergingStones = NewStoredValue<MergingStonesStoredValue>(StoredValueIDs.MergingStonesDB, StoredValueIDs.MergingStonesID).SetColor(StoredValueColor_Rare).SetFormat("Merging Stones: {0}");
            BlankBookAbility = NewStoredValue<CombatAbilityStoredValue>(StoredValueIDs.BlankBookAbilityDB, StoredValueIDs.BlankBookAbilityID).SetColor(StoredValueColor_Rare).SetFormat("Last used ability: {0}");
            BlankBookPassive = NewStoredValue<PassiveAbilityStoredValue>(StoredValueIDs.BlankBookPassiveDB, StoredValueIDs.BlankBookPassiveID).SetColor(StoredValueColor_Rare).SetFormat("Last used passive: {0}");
            UnnamedItem31 = NewStoredValue<IntEnumerableStoredValue>(StoredValueIDs.UnnamedItem31DB, StoredValueIDs.UnnamedItem31ID).SetColor(StoredValueColor_Negative).SetFormat("Already dealt: {0}").SetSortOrder(IntEnumerableStoredValue.IntSortOrder.Ascending);
        }

        public static bool CurrentTurnIsLowerThanValueDisplayCondition(UnitStoreDataHolder holder)
        {
            return holder.m_MainData >= CombatManager.Instance._stats.TurnsPassed + 1;
        }
    }
}
