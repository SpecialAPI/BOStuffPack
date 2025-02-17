using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class MindHouse
    {
        public static void Init()
        {
            var name = "Mind House";
            var flav = "\"Celestial Raven and the Mind Dragon\"";
            var desc = "At the start of combat, clone a random party member and a random enemy. Repeat this until cloning fails on either side";

            var item = NewItem<MultiCustomTriggerEffectWearable>("MindHouse_TW")
                .SetBasicInformation(name, flav, desc, "MindHouse")
                .SetPrice(34)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<MindHouseCloneEffect>())
                    })
                }
            };
        }
    }
}
