using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StoredValues
{
    public class MergingStonesStoredValue : UnitStoreData_BasicSO
    {
        public string Format;
        public Color Color;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            result = string.Empty;
            
            if(string.IsNullOrEmpty(Format))
                return false;

            if(holder == null || holder.m_ObjectData is not List<BaseWearableSO> items)
                return false;

            var itemString = "";

            foreach(var itm in items)
            {
                if(itm == null)
                    continue;

                var loc = itm.GetItemLocData();

                if(loc == null || string.IsNullOrEmpty(loc.text))
                    continue;

                if (!string.IsNullOrEmpty(itemString))
                    itemString += ", ";

                itemString += loc.text;
            }

            if(string.IsNullOrEmpty(itemString))
                return false;

            result = string.Format(Format, itemString).Colorize(Color);
            return true;
        }

        public MergingStonesStoredValue SetFormat(string format)
        {
            Format = format;

            return this;
        }

        public MergingStonesStoredValue SetColor(Color color)
        {
            Color = color;

            return this;
        }
    }
}
