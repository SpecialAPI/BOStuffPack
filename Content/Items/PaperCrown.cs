using BOStuffPack.Content.Conditions.Effector;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class PaperCrown
    {
        public static void Init()
        {
            var name = "Paper Crown";
            var flav = "\"Also useful for practicing museum burglary\"";
            var desc = "After this party member performs an ability, if there is no red pigment in the pigment bar, randomize all stored pigment.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("PaperCrown_TW")
                .SetBasicInformation(name, flav, desc, "PaperCrown")
                .SetPrice(8)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnAbilityUsed.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<RandomizeAllManaEffect>(x => x.manaRandomOptions = [Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple]))
                    }),
                    conditions = new()
                    {
                        CreateScriptable<MatchingPigmentComparisonEffectorCondition>(x =>
                        {
                            x.targetColor = Pigments.Red;
                            x.matchType = PigmentMatchType.ShareColor;
                            x.compareTo = 0;
                            x.comparison = IntComparison.Equal;
                            x.checkedBarsShouldHaveAnyMana = true;
                        })
                    }
                }
            };
        }
    }
}
