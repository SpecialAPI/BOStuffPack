using BOStuffPack.Effect;
using BOStuffPack.StaticModifiers;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class MergingStones
    {
        public static readonly string ID = "MergingStones_TW".Prefix();

        public static void Init()
        {
            var name = "Merging Stones";
            var flav = "\"The operation, it is complete?\"";
            var desc = "At the start of combat, unequip the items held by the left and right allies and make this item copy both of their effects at once.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "MergingStonesv2")
                .SetPrice(13)
                .AddToTreasure();

            NewStoredValue<MergingStonesStoredValue>(StoredValueIDs.MergingStonesDB, StoredValueIDs.MergingStonesID)
                .SetColor(StoredValueColor_Rare)
                .SetFormat("Merging Stones: {0}");

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeforeCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<TransferTargetItemsToCasterEffect>(x => x.itemsStoredValue = StoredValueIDs.MergingStonesID), 0, Targeting.Slot_AllySides)
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
                        itemsStoredValue = StoredValueIDs.MergingStonesID,
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
                        itemsStoredValue = StoredValueIDs.MergingStonesID,
                        disconnect = true
                    }
                }
            };

            item.SetStaticModifiers(CreateScriptable<ResetEverythingOnDisconnectStaticModifier>());
        }
    }
}
