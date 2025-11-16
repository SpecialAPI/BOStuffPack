using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class AlmightyBranch
    {
        public static void Init()
        {
            var name = "Almighty Branch";
            var flav = "\"Divine blood\"";
            var desc = "Damage dealt by this party member always produces 1 red and 1 purple pigment instead of the target's health color.";

            var itm = NewItem<MultiCustomTriggerEffectWearable>("AlmightyBranch_TW")
                .SetBasicInformation(name, flav, desc, "AlmightyBranch")
                .SetPrice(3)
                .AddToTreasure();
        }
    }
}
