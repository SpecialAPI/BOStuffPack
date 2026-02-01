using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StoredValues
{
    public class IntEnumerableStoredValue : UnitStoreData_BasicSO
    {
        public string Format;
        public Color Color;
        public string Separator = ", ";
        public IntSortOrder SortOrder = IntSortOrder.None;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            result = string.Empty;

            if (string.IsNullOrEmpty(Format))
                return false;

            if (holder == null || holder.m_ObjectData is not IEnumerable<int> enumerable)
                return false;

            if(SortOrder == IntSortOrder.Ascending)
                enumerable = enumerable.OrderBy(x => x);
            else if(SortOrder == IntSortOrder.Descending)
                enumerable = enumerable.OrderByDescending(x => x);

            result = string.Format(Format, string.Join(Separator, enumerable.Select(x => x.ToString()))).Colorize(Color);
            return true;
        }

        public IntEnumerableStoredValue SetFormat(string format)
        {
            Format = format;

            return this;
        }

        public IntEnumerableStoredValue SetColor(Color color)
        {
            Color = color;

            return this;
        }

        public IntEnumerableStoredValue SetSeparator(string separator)
        {
            Separator = separator;

            return this;
        }

        public IntEnumerableStoredValue SetSortOrder(IntSortOrder sortOrder)
        {
            SortOrder = sortOrder;

            return this;
        }

        public enum IntSortOrder
        {
            None,
            Ascending,
            Descending,
        }
    }
}
