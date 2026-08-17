using BOStuffPack.Effect;
using BOStuffPack.StaticModifiers;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class DuctTape
    {
        public static readonly string ID = "DuctTape_TW".Prefix();
        public static readonly string StoredValueDB = "DuctTape_USD".Prefix();
        public static readonly string StoredValueID = "DuctTape".Prefix();

        public static void Init()
        {
            var name = "Duct Tape";
            var flav = "\"The operation, it is complete?\"";
            var desc = "At the start of combat, unequip the items held by the left and right allies and make this item copy both of their effects at once.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "MergingStonesv2")
                .SetPrice(13)
                .AddToTreasure();

            NewStoredValue<ItemListStoredValue>(StoredValueDB, StoredValueID)
                .SetColor(StoredValueColor_Rare)
                .SetFormat("Duct Tape: {0}");

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeforeCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<TransferTargetItemsToCasterEffect>(x => x.itemsStoredValue = StoredValueID), 0, Targeting.Slot_AllySides)
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
                        itemsStoredValue = StoredValueID,
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
                        itemsStoredValue = StoredValueID,
                        disconnect = true
                    }
                }
            };

            item.SetStaticModifiers(CreateScriptable<ResetEverythingOnDisconnectStaticModifier>());
        }
    }
}
