using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    [HarmonyPatch]
    public static class Nothing
    {
        public static string NothingID;
        public static float NothingRandomChanceMult;
        public static MethodInfo pnc_c = AccessTools.Method(typeof(Nothing), nameof(PreventNothingChance_Chance));

        public static void Init()
        {
            var name = "Nothing?";
            var flav = "\"There have been a lot of glorified nothings, to see it actually say \"nothing\" is refreshing actually\"";
            var desc = "There's no catch, this item does absolutely nothing.";

            var item = NewItem<BasicWearable>("Nothing_TW")
                .SetBasicInformation(name, flav, desc, "Nothing")
                .SetPrice(1)
                .AddToTreasure();

            NothingID = item.name;
            NothingRandomChanceMult = Random.Range(0.7f, 1f);
        }

        [HarmonyPatch(typeof(ItemPoolDataBase), nameof(ItemPoolDataBase.TryGetPrizeItem))]
        [HarmonyILManipulator]
        public static void PreventNothingChance_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchCallOrCallvirt<HashSet<string>>(nameof(HashSet<string>.Contains))))
                return;

            crs.Emit(OpCodes.Ldloc_2);
            crs.Emit(OpCodes.Call, pnc_c);
        }

        public static bool PreventNothingChance_Chance(bool curr, string item)
        {
            if (curr)
                return curr;

            if (item != NothingID)
                return curr;

            var time = DateTime.Now;

            var dayChanceMult = time.DayOfWeek switch
            {
                DayOfWeek.Monday => 1f,
                DayOfWeek.Tuesday => 0.6f,
                DayOfWeek.Wednesday => 0.5f,
                DayOfWeek.Thursday => 0.7f,
                DayOfWeek.Friday => 0.6f,
                DayOfWeek.Saturday => 0.4f,
                DayOfWeek.Sunday => 0.4f,

                _ => 1f
            };
            var monthChanceMult = time.Month switch
            {
                1 => 1f,
                2 => 0.95f,
                3 => 0.95f,
                4 => 0.9f,
                5 => 0.9f,
                6 => 0.87f,
                7 => 0.87f,
                8 => 0.9f,
                9 => 0.9f,
                10 => 0.95f,
                11 => 0.95f,
                12 => 1f,

                _ => 1f
            };
            var randomChanceMult = NothingRandomChanceMult;

            var nothingBaseChance = 0.5f;
            var nothingChance = nothingBaseChance * dayChanceMult * dayChanceMult * monthChanceMult * randomChanceMult;

            if (Random.value >= nothingChance)
                return true;

            return curr;
        }
    }
}
