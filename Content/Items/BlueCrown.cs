using BOStuffPack.Content.Conditions.Effector;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class BlueCrown
    {
        public static void Init()
        {
            var name = "Blue Crown";
            var flav = "\"Recently set with freshly cut sapphires\"";
            var desc = "Adds Red Essence to this party member as a passive. Upon red lucky pigment being produced, randomize stored pigment.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("BlueCrown")
                .SetBasicInformation(name, flav, desc, "CrownOfTheBlueprints")
                .SetPrice(46)
                .AddToTreasure()
                .SetStaticModifiers(ExtraPassiveModifier(Passives.EssenceRed));

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = CustomTriggers.OnLuckyPigmentSuccess,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<RandomizeAllManaEffect>(x => x.manaRandomOptions = [Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple]))
                    }),
                    conditions = new()
                    {
                        CreateScriptable<CheckCurrentLuckyPigmentColorEffectorCondition>(x =>
                        {
                            x.targetColor = Pigments.Red;
                            x.matchType = PigmentMatchType.ShareColor;
                            x.shouldMatch = true;
                        })
                    }
                }
            };
        }
    }
}
