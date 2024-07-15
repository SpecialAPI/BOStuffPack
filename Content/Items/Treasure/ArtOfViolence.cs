using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class ArtOfViolence
    {
        public static void Init()
        {
            var name = "The Art of Violence";
            var flav = "\"The world is your canvas, so take up your brush and paint it <color=#ff0000>R E D</color>\"";
            var desc = "On combat start, change all enemies' health colors to grey and give them Leaky and Red Core as passives.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "ArtOfViolence").AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,
                    
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(Enemies, CreateScriptable<ChangeToRandomHealthColorEffect>(x => x._healthColors = [Pigments.Grey])),

                        Effect(Enemies, CreateScriptable<AddPassiveEffect>(x => x._passiveToAdd = Passives.Leaky1)),
                        Effect(Enemies, CreateScriptable<AddPassiveEffect>(x => x._passiveToAdd = RedCore))
                    })
                }
            };
        }
    }
}
