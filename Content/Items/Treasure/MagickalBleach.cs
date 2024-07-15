using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class MagickalBleach
    {
        public static void Init()
        {
            var name = "Magickal Bleach";
            var flav = "\"A cure for death, inanimacy and your head being on fire. Just drink it and you're good to go!\"";
            var desc = "This party member no longer has any passives.";

            var item = NewItem<BasicWearable>(name, flav, desc, "MagickalBleach").AddModifiers(ModdedData("BleachStaticModifier", new BleachStaticModifier())).AddToTreasure().Build();
        }
    }
}
