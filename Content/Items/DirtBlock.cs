using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class DirtBlock
    {
        public static void Init()
        {
            var name = "Dirt Block";
            var flav = "\"Copyright (c) 2026 Captain Pretzel\"";
            var desc = "This item cannot be destroyed in combat.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("DirtBlock_ExtraW")
                .SetBasicInformation(name, flav, desc, "DirtBlock")
                .SetPrice(0)
                .ExcludeFromConsole();
            LoadedWearables[item.name] = item;

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.ModifyCanDestroyItem,
                    doesPopup = false,
                    immediate = true,
                    
                    effect = new BoolHolderSetterTriggerffect(false)
                }
            });
        }
    }
}
