using BOStuffPack.Content.Items.StaticModifiers;
using BOStuffPack.Content.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class MergingStones
    {
        public static void Init()
        {
            var name = "Merging Stones";
            var flav = "\"The operation, it is complete?\"";
            var desc = "At the start of combat, unequip the items held by the left and right allies and make this item copy both of their effects at once.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("MergingStones_TW")
                .SetBasicInformation(name, flav, desc, "MergingStonesv2")
                .SetPrice(13)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeforeCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<TransferTargetItemsToCasterEffect>(x => x.itemsStoredValue = StuffPackStoredValues.StoredValue_MergingStones), 0, Targeting.Slot_AllySides)
                    })
                }
            };

            item.connectionEffects = new()
            {
                new()
                {
                    immediate = true,
                    doesPopup = false,

                    effect = new ConnectOrDisconnectStoredItemsTriggerEffect()
                    {
                        itemsStoredValue = StuffPackStoredValues.StoredValue_MergingStones,
                        disconnect = false
                    }
                }
            };

            item.disconnectionEffects = new()
            {
                new()
                {
                    immediate = true,
                    doesPopup = false,

                    effect = new ConnectOrDisconnectStoredItemsTriggerEffect()
                    {
                        itemsStoredValue = StuffPackStoredValues.StoredValue_MergingStones,
                        disconnect = true
                    }
                }
            };

            item.SetStaticModifiers(CreateScriptable<ResetEverythingOnDisconnectStaticModifier>());
        }
    }
}
