using BOStuffPack.CustomTrigger;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class BlankBook
    {
        public static readonly string ID = "BlankBook_TW".Prefix();
        public static readonly string AbilitySVDB = "BlankBook_Ability_USD".Prefix();
        public static readonly string AbilitySVID = "BlankBook_Ability".Prefix();
        public static readonly string PassiveSVDB = "BlankBook_Passive_USD".Prefix();
        public static readonly string PassiveSVID = "BlankBook_Passive".Prefix();

        public static void Init()
        {
            var name = "Blank Book";
            var flav = "\"WIP\"";
            var desc = "At the end of combat, or upon this character dying or fleeing, destroy this item and produce a Written Book that grants its holder this party member's last used ability and passive at the time of this effect triggering.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "BlankBookPlaceholder")
                .SetPrice(8)
                .AddToTreasure();

            NewStoredValue<CombatAbilityStoredValue>(AbilitySVDB, AbilitySVID)
                .SetColor(StoredValueColor_Rare)
                .SetFormat("Last used ability: {0}");

            NewStoredValue<PassiveAbilityStoredValue>(PassiveSVDB, PassiveSVID)
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
                        storedValue = AbilitySVID
                    }
                },
                new()
                {
                    trigger = LocalCustomTriggers.OnPassivePopup,
                    immediate = true,
                    doesPopup = false,

                    effect = new BlankBookSetPassiveTriggerEffect()
                    {
                        storedValue = PassiveSVID
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
                        abilityStoredValue = AbilitySVID,
                        abilityDataKey = WrittenBook.ExtraAbilityDataKey,

                        passiveStoredValue = PassiveSVID,
                        passiveDataKey = WrittenBook.ExtraPassiveDataKey,

                        itemID = WrittenBook.ID
                    }
                }
            });
        }
    }
}
