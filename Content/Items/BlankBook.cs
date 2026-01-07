using BOStuffPack.Content.StoredValues;
using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class BlankBook
    {
        public static void Init()
        {
            var name = "Blank Book";
            var flav = "\"WIP\"";
            var desc = "At the end of combat, or upon this character dying or fleeing, destroy this item and produce a Written Book that grants its holder this party member's last used ability and passive at the time of this effect triggering.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("BlankBook_TW")
                .SetBasicInformation(name, flav, desc, "BlankBook")
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
                        storedValue = LocalStoredValues.StoredValue_BlankBookAbility._UnitStoreDataID
                    }
                },
                new()
                {
                    trigger = LocalCustomTriggers.OnPassivePopup,
                    immediate = true,
                    doesPopup = false,

                    effect = new BlankBookSetPassiveTriggerEffect()
                    {
                        storedValue = LocalStoredValues.StoredValue_BlankBookPassive._UnitStoreDataID
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
                        abilityStoredValue = LocalStoredValues.StoredValue_BlankBookAbility._UnitStoreDataID,
                        abilityDataKey = WrittenBook.ExtraAbilityDataKey,

                        passiveStoredValue = LocalStoredValues.StoredValue_BlankBookPassive._UnitStoreDataID,
                        passiveDataKey = WrittenBook.ExtraPassiveDataKey,

                        itemID = Profile.GetID("WrittenBook_ExtraW")
                    }
                }
            });
        }
    }
}
