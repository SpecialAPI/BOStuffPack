using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class CustomLoc
    {
        public const string ItemRestoredID = $"{MOD_PREFIX}_ItemRestoredText";
        public const string ItemRestoredDefault = "restored";

        public static string GetUIData(string id, string defaultText = "")
        {
            if (LocUtils._gameLoc._uiData.TryGetValue(id, out var uiData))
                return uiData;

            return defaultText;
        }
    }
}
