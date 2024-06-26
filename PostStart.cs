using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack
{
    public static class PostStart
    {
        public static List<(BaseWearableSO item, string pool, int probability)> ModdedPoolItems = [];

        public static void OnPostStart()
        {
            foreach(var (item, pool, probability) in ModdedPoolItems)
                ItemUtils.AddItemToCustomLootPool(item, pool, probability, item.startsLocked);
        }
    }
}
