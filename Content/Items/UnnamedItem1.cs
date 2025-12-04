using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem1
    {
        public static void Init()
        {
            var name = "Unnamed Item 1";
            var flav = "\"WIP\"";
            var desc = "Upon another item being destroyed, restore it and destroy this item if successful. This item's effects don't work on other copies of this item.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem1_SW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(4)
                .AddToShop();

            var ignoreTag = Profile.GetID("UnnamedItem1Ignore");
            item.AddItemTypes(ignoreTag);

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnANYItemConsumed.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new RestoreItemReferenceUnitItemTriggerEffect()
                    {
                        IgnoredItemTags = [ignoreTag],
                        consumeSelfIfSuccessful = true,
                        ignoreIfTargetIsSender = true,
                        doesRestoredPopup = true
                    }
                }
            });
        }
    }
}
