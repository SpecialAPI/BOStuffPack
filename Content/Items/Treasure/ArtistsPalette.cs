using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class ArtistsPalette
    {
        public static void Init()
        {
            var name = "Artist's Palette";
            var flav = "\"Primary Colors\"";
            var desc = "Adds Untethered Core to this party member as a passive.\nAt the beginning of combat, add Untethered Core to the opposing enemy as a passive.\nCore allows health color to be toggled to other colors.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "Palette").AddModifiers(ExtraPassive(UntetheredCore)).AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(Opposing, CreateScriptable<AddPassiveEffect>(x => x._passiveToAdd = UntetheredCore))
                    })
                }
            };
        }
    }
}
