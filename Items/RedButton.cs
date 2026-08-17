using BOStuffPack.Conditions.Effector;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class RedButton
    {
        public static readonly string ID = "RedButton_TW".Prefix();

        public static void Init()
        {
            var name = "Red Button";
            var flav = "\"Do Not Press.\"";
            var desc = "Adds Red Essence to this party member as a passive. Upon red lucky pigment being produced, randomize stored pigment.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "RedButtonPlaceholder")
                .SetPrice(16)
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
                        Effects.GenerateEffect(CommonEffects.RandomizeAllPigment)
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
