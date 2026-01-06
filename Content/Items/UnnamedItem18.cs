using BOStuffPack.Content.Misc;
using BOStuffPack.DynamicAppearances;
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

            var dataKey = Profile.GetID("SavedStatusEffects");
            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatEnd.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new SaveSenderStatusEffectsEffect(dataKey)
                },
                new()
                {
                    trigger = TriggerCalls.OnFirstTurnStart.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new ApplySavedStatusEffectsToSenderTriggerEffect(dataKey)
                }
            });
            item.AttachDynamicAppearance(new UnnamedItem18DynamicAppearance(dataKey));
        }
    }
}
