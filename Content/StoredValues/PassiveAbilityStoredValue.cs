using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StoredValues
{
    public class PassiveAbilityStoredValue : UnitStoreData_BasicSO
    {
        public string Format;
        public Color Color;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            result = string.Empty;

            if (string.IsNullOrEmpty(Format))
                return false;

            if (holder == null || holder.m_ObjectData is not BasePassiveAbilitySO passive)
                return false;

            if(passive == null)
                return false;

            result = string.Format(Format, passive.GetPassiveLocData().text).Colorize(Color);
            return true;
        }

        public PassiveAbilityStoredValue SetFormat(string format)
        {
            Format = format;

            return this;
        }

        public PassiveAbilityStoredValue SetColor(Color color)
        {
            Color = color;

            return this;
        }
    }
}
