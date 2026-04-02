using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    [HarmonyPatch]
    public static class ConsoleExcluder
    {
        private static readonly List<string> excludedItems = [];

        private static string LowerInvariant(string s) => s.ToLowerInvariant();

        [HarmonyPatch(typeof(DebugController), "LoadItemIds", MethodType.Enumerator)]
        [HarmonyILManipulator]
        private static void ExcludeItems_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchNewobj<List<string>>()))
                return;

            crs.EmitStaticDelegate(ExcludeItems_AddExcludedItems);
        }

        private static List<string> ExcludeItems_AddExcludedItems(List<string> processedList)
        {
            processedList.AddRange(excludedItems.Select(LowerInvariant));

            return processedList;
        }

        public static T ExcludeFromConsole<T>(this T w) where T : BaseWearableSO
        {
            excludedItems.Add(w.name);

            return w;
        }
    }
}
