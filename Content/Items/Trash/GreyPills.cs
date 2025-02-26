using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Trash
{
    public class GreyPills
    {
        public static void Init()
        {
            var useAltSpelling = Random.Range(0, 100) < 5; // TODO: make spelling vary per run instead of per game launch

            var name = $"{(useAltSpelling ? "Gray" : "Grey")} Pills";
            var flav = "\"How dull.\"";
            var desc = $"At the start of combat, attempt to produce 3 universal {(useAltSpelling ? "gray" : "grey")} pigment.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("GreyPills_TrashW")
                .SetBasicInformation(name, flav, desc, "GreyPills")
                .SetPrice(7)
                .AddToShop(); // TODO: add to trash instead of shop

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Grey), 3)
                    })
                }
            };
        }
    }
}
