using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.UnitExtension
{
    [HarmonyPatch]
    public static class UnitExtTools
    {
        public static UnitExt Ext(this IUnit unit)
        {
            if(unit == null || unit.Equals(null))
                return null;

            unit.TryGetStoredData(StoreData_UnitExt.name, out var hold, true);

            if (hold == null)
                return null; // WTF?

            return (hold.m_ObjectData ??= new UnitExt(unit)) as UnitExt;
        }
    }
}
