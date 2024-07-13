using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StoredValues
{
    public static class CustomStoredValues
    {
        public static AdvancedStoredValueIntInfo StoredValue_MergedCount;
        public static AdvancedStoredValueIntInfo StoredValue_TargetShift;
        public static AdvancedStoredValueIntInfo StoredValue_SwapHealingAndDamageTriggering;

        public static UnitStoreData_BasicSO StoreData_UnitExt;

        public static void Init()
        {
            StoredValue_MergedCount = StoredValue("MergedCount_SV").WithBaseColor(StoredValueColor.Positive).WithFormat("Merged Enemies: {0}");
            StoredValue_TargetShift = StoredValue("TargetShift_SV").WithBaseColor(StoredValueColor.Positive).WithDisplayCondition(IntCondition.NonZero).WithDynamicString(x => $"TargetShift: {TargetString(x.m_MainData)}");
            StoredValue_SwapHealingAndDamageTriggering = StoredValue("SwapHealingAndDamageTriggering_SV");

            StoreData_UnitExt = CreateScriptable<UnitStoreData_BasicSO>(x =>
            {
                x.name = "UnitExt_SV";
                x._UnitStoreDataID = "UnitExt_SV";

                LoadedDBsHandler.MiscDB.AddNewUnitStoreData(x.name, x);
            });
        }
    }
}
