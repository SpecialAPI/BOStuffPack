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

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.OnBeforeAbilityAnimation,
                    immediate = true,
                    doesPopup = false,

                    effect = new BlankBookSetAbilityTriggerEffect()
                    {
                        storedValue = LocalStoredValues.BlankBookAbility._UnitStoreDataID
                    }
                },
                new()
                {
                    trigger = LocalCustomTriggers.OnPassivePopup,
                    immediate = true,
                    doesPopup = false,

                    effect = new BlankBookSetPassiveTriggerEffect()
                    {
                        storedValue = LocalStoredValues.BlankBookPassive._UnitStoreDataID
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
                        abilityStoredValue = LocalStoredValues.BlankBookAbility._UnitStoreDataID,
                        abilityDataKey = WrittenBook.ExtraAbilityDataKey,

                        passiveStoredValue = LocalStoredValues.BlankBookPassive._UnitStoreDataID,
                        passiveDataKey = WrittenBook.ExtraPassiveDataKey,

                        itemID = ItemIDs.WrittenBook
                    }
                }
            });
        }
    }
}
