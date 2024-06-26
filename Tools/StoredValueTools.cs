using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class StoredValueTools
    {
        public static Color StoredValue_Positive = new(0f, 0.5961f, 0.8667f);
        public static Color StoredValue_Negative = new(0.8667f, 0f, 0.2157f);
        public static Color StoredValue_Rare = new(0.7725f, 0.2667f, 0.8196f);

        public static int GetIntStoredValue(this IUnit u, string storedValue) => u.TryGetStoredData(storedValue, out var hold, false) ? hold?.m_MainData ?? 0 : 0;
        public static void SetIntStoredValue(this IUnit u, string storedValue, int val)
        {
            u.TryGetStoredData(storedValue, out var holder);

            if (holder == null)
                return;

            holder.m_MainData = val;
        }

        public static AdvancedStoredValueIntInfo StoredValue(string id)
        {
            var val = CreateScriptable<AdvancedStoredValueIntInfo>();

            val.name = id;
            val._UnitStoreDataID = id;

            return val;
        }

        public static AdvancedStoredValueIntInfo WithFormat(this AdvancedStoredValueIntInfo inf, string format)
        {
            inf.staticString = format;

            return inf;
        }

        public static AdvancedStoredValueIntInfo WithDynamicString(this AdvancedStoredValueIntInfo inf, Func<UnitStoreDataHolder, string> str)
        {
            inf.dynamicString = str;

            return inf;
        }

        public static AdvancedStoredValueIntInfo WithBaseColor(this AdvancedStoredValueIntInfo inf, StoredValueColor color)
        {
            inf.color = color switch
            {
                StoredValueColor.Positive => StoredValue_Positive,
                StoredValueColor.Negative => StoredValue_Negative,
                StoredValueColor.Rare => StoredValue_Rare,

                _ => Color.white
            };

            return inf;
        }

        public static AdvancedStoredValueIntInfo WithCustomColor(this AdvancedStoredValueIntInfo inf, Color color)
        {
            inf.color = color;

            return inf;
        }

        public static AdvancedStoredValueIntInfo WithDynamicColor(this AdvancedStoredValueIntInfo inf, Func<UnitStoreDataHolder, Color> col)
        {
            inf.dynamicColor = col;

            return inf;
        }

        public static AdvancedStoredValueIntInfo WithDisplayCondition(this AdvancedStoredValueIntInfo inf, IntCondition condition)
        {
            inf.condition = condition;

            return inf;
        }

        public static AdvancedStoredValueIntInfo WithCustomCondition(this AdvancedStoredValueIntInfo inf, Func<UnitStoreDataHolder, bool> condition)
        {
            inf.customCondition = condition;
            inf.condition = IntCondition.None;

            return inf;
        }
    }

    public class AdvancedStoredValueIntInfo : UnitStoreData_BasicSO
    {
        public string staticString;
        public Func<UnitStoreDataHolder, string> dynamicString;

        public Color color = Color.white;
        public Func<UnitStoreDataHolder, Color> dynamicColor;

        public IntCondition condition = IntCondition.Positive;
        public Func<UnitStoreDataHolder, bool> customCondition;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            var display = MeetsCondition(holder);

            result = display ? FormatStoredValue(holder) : string.Empty;

            return display;
        }

        public virtual string FormatStoredValue(UnitStoreDataHolder holder)
        {
            return GetString(holder).Colorize(GetColor(holder));
        }

        public virtual string GetString(UnitStoreDataHolder holder)
        {
            return dynamicString?.Invoke(holder) ?? staticString.Replace("+{0}", holder.m_MainData >= 0 ? $"+{holder.m_MainData}" : holder.m_MainData.ToString()).Replace("{0}", holder.m_MainData.ToString());
        }

        public virtual Color GetColor(UnitStoreDataHolder holder)
        {
            return dynamicColor?.Invoke(holder) ?? color;
        }

        public virtual bool MeetsCondition(UnitStoreDataHolder holder)
        {
            return (customCondition?.Invoke(holder) ?? true) || MeetsIntCondition(holder.m_MainData, condition);
        }
    }

    public enum StoredValueColor
    {
        Positive,
        Negative,
        Rare
    }
}
