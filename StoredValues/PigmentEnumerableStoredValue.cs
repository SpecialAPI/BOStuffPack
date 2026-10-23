using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.StoredValues
{
    public class PigmentEnumerableStoredValue : UnitStoreData_BasicSO
    {
        public string Format;
        public Color Color;
        public string Separator = ", ";

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            result = string.Empty;

            if (string.IsNullOrEmpty(Format))
                return false;

            if (holder == null || holder.m_ObjectData is not IEnumerable<ManaColorSO> enumerable)
                return false;

            if(!enumerable.Any())
                return false;

            result = string.Format(Format, string.Join(Separator, enumerable.Select(x => x.pigmentID))).Colorize(Color);
            return true;
        }

        public PigmentEnumerableStoredValue SetFormat(string format)
        {
            Format = format;

            return this;
        }

        public PigmentEnumerableStoredValue SetColor(Color color)
        {
            Color = color;

            return this;
        }

        public PigmentEnumerableStoredValue SetSeparator(string separator)
        {
            Separator = separator;

            return this;
        }
    }
}
