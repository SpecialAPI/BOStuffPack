using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Shop
{
    public static class OilPaints
    {
        public static void Init()
        {
            var name = "Oil Paints";
            var flav = "\"Paint with oil\"";
            var desc = "When an enemy or a party member is inflicted with Oil-Slicked, give them a random Pigment Core that they don't have yet.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "OilPaints").AddToShop(2).Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = CustomEvents.STATUS_EFFECT_APPLIED,
                    doesPopup = true,
                    
                    effect = new DoEffectOnNotificationTargetTriggerEffect(new PerformEffectTriggerEffect(new()
                    {
                        Effect(Self, CreateScriptable<AddNewPassiveNotInHealthOptionsEffect>(x => x.passives = new()
                        {
                            (Pigments.Red.pigmentID, RedCore),
                            (Pigments.Blue.pigmentID, BlueCore),
                            (Pigments.Yellow.pigmentID, YellowCore),
                            (Pigments.Purple.pigmentID, PurpleCore),
                        }))
                    })),

                    conditions = new()
                    {
                        CreateScriptable<TargettedEffectApplicationMatchesEffectEffectorCondition>(x => x.status = StatusField.OilSlicked.StatusID)
                    }
                }
            };
        }
    }
}
