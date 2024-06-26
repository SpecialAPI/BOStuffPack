using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StoredValues
{
    public static class CustomStoredValues
    {
        public static AdvancedStoredValueIntInfo StoredValue_MergedCount;

        public static void Init()
        {
            StoredValue_MergedCount = StoredValue("MergedCount_SV").WithBaseColor(StoredValueColor.Positive).WithFormat("Merged Enemies: {0}");
        }
    }
}
