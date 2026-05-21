using BOStuffPack.CustomTrigger;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class BlankBook
    {
        public static void Init()
        {
            var name = "Blank Book";
            var flav = "\"WIP\"";
            var desc = "At the end of combat, or upon this character dying or fleeing, destroy this item and produce a Written Book that grants its holder this party member's last used ability and passive at the time of this effect triggering.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ItemIDs.BlankBook)
                .SetBasicInformation(name, flav, desc, "BlankBookPlaceholder")
                .SetPrice(8)
                .AddToTreasure();

            NewStoredValue<CombatAbilityStoredValue>(StoredValueIDs.BlankBookAbilityDB, StoredValueIDs.BlankBookAbilityID)
                .SetColor(StoredValueColor_Rare)
                .SetFormat("Last used ability: {0}");

            NewStoredValue<PassiveAbilityStoredValue>(StoredValueIDs.BlankBookPassiveDB, StoredValueIDs.BlankBookPassiveID)
                .SetColor(StoredValueColor_Rare)
                .SetFormat("Last used passive: {0}");

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnBeforeAbilityAnimation,
                    immediate = true,
                    doesPopup = false,

                    effect = new BlankBookSetAbilityTriggerEffect()
                    {
                        storedValue = StoredValueIDs.BlankBookAbilityID
                    }
                },
                new()
                {
                    trigger = LocalCustomTriggers.OnPassivePopup,
                    immediate = true,
                    doesPopup = false,

                    effect = new BlankBookSetPassiveTriggerEffect()
                    {
                        storedValue = StoredValueIDs.BlankBookPassiveID
                    }
                },
                new TriggerEffectAndTriggersInfo()
                {
                    triggers = [TriggerCalls.OnCombatEnd.ToString(), TriggerCalls.OnDeath.ToString(), TriggerCalls.OnFleeting.ToString()],
                    immediate = false,
                    doesPopup = true,
                    getsConsumed = true,

                    effect = new SetUpAndProduceWrittenBookTriggerEffect()
                    {
                        abilityStoredValue = StoredValueIDs.BlankBookAbilityID,
                        abilityDataKey = WrittenBook.ExtraAbilityDataKey,

                        passiveStoredValue = StoredValueIDs.BlankBookPassiveID,
                        passiveDataKey = WrittenBook.ExtraPassiveDataKey,

                        itemID = ItemIDs.WrittenBook
                    }
                }
            });
        }
    }
}
