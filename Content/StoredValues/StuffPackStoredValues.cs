using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StoredValues
{
    public static class StuffPackStoredValues
    {
        public static UnitStoreData_BasicSO StoredValue_MergedCount;
        public static UnitStoreData_BasicSO StoredValue_KeybladeRTurn;
        public static UnitStoreData_BasicSO StoredValue_KeybladeBTurn;
        public static UnitStoreData_BasicSO StoredValue_KeybladeYTurn;
        public static UnitStoreData_BasicSO StoredValue_KeybladePTurn;
        public static UnitStoreData_BasicSO StoredValue_Blood;

        public static UnitStoreData_BasicSO StoredValue_MergingStones;

        public static void Init()
        {
            StoredValue_MergedCount = NewStoredValue<AdvancedStoredValueIntInfo>("MergedCount_USD", "MergedCount").SetColor(StoredValueColor_Negative).SetFormat("Merged Enemies: {0}");
            StoredValue_KeybladeRTurn = NewStoredValue<AdvancedStoredValueIntInfo>("KeybladeRTurn_USD", "KeybladeRTurn").SetColor(StoredValueColor_Negative).SetFormat("Keyblade R Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            StoredValue_KeybladeBTurn = NewStoredValue<AdvancedStoredValueIntInfo>("KeybladeBTurn_USD", "KeybladeBTurn").SetColor(StoredValueColor_Negative).SetFormat("Keyblade B Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            StoredValue_KeybladeYTurn = NewStoredValue<AdvancedStoredValueIntInfo>("KeybladeYTurn_USD", "KeybladeYTurn").SetColor(StoredValueColor_Negative).SetFormat("Keyblade Y Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            StoredValue_KeybladePTurn = NewStoredValue<AdvancedStoredValueIntInfo>("KeybladePTurn_USD", "KeybladePTurn").SetColor(StoredValueColor_Negative).SetFormat("Keyblade P Disabled").SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);
            StoredValue_Blood = NewStoredValue<AdvancedStoredValueIntInfo>("Blood_USD", "Blood").SetColor(StoredValueColor_Negative).SetFormat("Blood: {0}");

            StoredValue_MergingStones = NewStoredValue<MergingStonesStoredValue>("MergingStones_USD", "MergingStones").SetColor(StoredValueColor_Rare).SetFormat("Merging Stones: {0}");
        }

        public static bool CurrentTurnIsLowerThanValueDisplayCondition(UnitStoreDataHolder holder)
        {
            return holder.m_MainData >= (CombatManager.Instance._stats.TurnsPassed + 1);
        }
    }
}
