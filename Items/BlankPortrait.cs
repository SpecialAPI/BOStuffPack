using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class BlankPortrait
    {
        public static readonly string ID = "BlankPortrait_TW".Prefix();

        public static void Init()
        {
            var name = "Blank Portrait";
            var flav = "\"Erased From History.\"";
            var desc = "At the start of combat, remove all passives from this party member and the Left and Right allies.";
            
            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "BlankPortrait")
                .SetPrice(0)
                .AddToTreasure()
                .AddItemTypes(ItemType_GameIDs.Magic.ToString());

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    immediate = true,
                    doesPopup = true,
                    
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<RemoveAllPassives_Effect>(), 0, Targeting.Slot_SelfAndSides)
                    })
                }
            });
        }
    }
}
