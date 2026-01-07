using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StoredValues
{
    public class CombatAbilityStoredValue : UnitStoreData_BasicSO
    {
        public string Format;
        public Color Color;

        public override bool TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
        {
            result = string.Empty;

            if (string.IsNullOrEmpty(Format))
                return false;

            if (holder == null || holder.m_ObjectData is not CombatAbility cAbility)
                return false;

            if(cAbility.ability == null)
                return false;

            result = string.Format(Format, cAbility.ability.GetAbilityLocData().text).Colorize(Color);
            return true;
        }

        public CombatAbilityStoredValue SetFormat(string format)
        {
            Format = format;

            return this;
        }

        public CombatAbilityStoredValue SetColor(Color color)
        {
            Color = color;

            return this;
        }
    }
}
