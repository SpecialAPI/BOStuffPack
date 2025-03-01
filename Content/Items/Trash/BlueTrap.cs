using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Trash
{
    public static class BlueTrap
    {
        public static void Init()
        {
            var name = "Blue Trap";
            var flav = "\"Produce blue pigment. A blue situation.\"";
            var desc = "Adds Blue Essence as a passive to this party member.";

            var item = NewItem<BasicWearable>("BlueTrap_TrashW")
                .SetBasicInformation(name, flav, desc, "BlueTrap")
                .SetPrice(8)
                .AddWithoutItemPools(); // TODO: add to trash pool

            item.SetStaticModifiers(ExtraPassiveModifier(Passives.EssenceBlue));
        }
    }
}
