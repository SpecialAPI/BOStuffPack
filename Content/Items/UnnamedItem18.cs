using BOStuffPack.Content.Misc;
using BOStuffPack.DynamicAppearances;
using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem18
    {
        public static void Init()
        {
            var name = "Unnamed Item 18";
            var flav = "\"WIP\"";
            var desc = "At the end of combat, save all status effects on this party member. At the start of combat, apply all saved status effects to this party member.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem18_TW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(13)
                .AddToTreasure();

            var seDataKey = Profile.GetID("SavedStatusEffects");
            var svDataKey = Profile.GetID("SavedStoredValues");
            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatEnd.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect =
                        new SaveSenderStatusEffectsEffect(seDataKey)
                        .Add(new SaveSenderStoredValuesTriggerEffect(svDataKey))
                },
                new()
                {
                    trigger = TriggerCalls.OnFirstTurnStart.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect =
                        new ApplySavedStatusEffectsToSenderTriggerEffect(seDataKey)
                        .Add(new ApplySavedStoredValuesToSenderTriggerEffect(svDataKey))
                }
            });
            item.AttachDynamicAppearance(new UnnamedItem18DynamicAppearance(seDataKey));
        }
    }
}
