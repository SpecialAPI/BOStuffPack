using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class OldBleach
    {
        public static void Init()
        {
            var name = "Old Bleach";
            var flav = "\"A cure for death, inanimacy and your head being on fire. Just drink it and you're good to go!\"";
            var desc = "This party member no longer has any passives.";

            var item = NewItem<BasicWearable>("OldBleach_ExtraW")
                .SetBasicInformation(name, flav, desc, "OldBleach")
                .SetStaticModifiers(ModdedDataModifier(new BleachStaticModifier()))
                .SetPrice(0)
                .AddWithoutItemPools()
                .AddItemTypes(ItemType_GameIDs.Magic.ToString());
        }
    }
}
