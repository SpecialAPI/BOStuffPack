using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Trash
{
    public static class GitHubSmoker
    {
        public static void Init()
        {
            var name = "GitHub Smoker";
            var flav = "\"Coding Kills\"";
            var desc = "This party member deals 10% more damage for each installed and enabled mod with a GitHub repository linked.\nThis party member receives 1 more damage for each installed and enabled mod without a GitHub repository linked.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("GitHubSmoker_TrashW")
                .SetBasicInformation(name, flav, desc, "GitHubSmoker")
                .SetPrice(1)
                .AddToShop(); // TODO: add to trash instead of shop

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillApplyDamage.ToString(),
                    immediate = true,
                    doesPopup = true,
                    
                    effect = new LinkedGitHubPercentageSetterTriggerEffect()
                    {
                        PercentagePerMod = 10,
                        UseModsWithoutGitHub = false
                    }
                },

                new()
                {
                    trigger = TriggerCalls.OnBeingDamaged.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new LinkedGitHubAdditionSetterTriggerEffect()
                    {
                        AdditionPerMod = 1,
                        UseModsWithoutGitHub = true
                    }
                }
            };
        }
    }
}
