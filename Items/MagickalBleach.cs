using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class MagickalBleach
    {
        public static readonly string ID = "MagickalBleach_TW".Prefix();

        public static void Init()
        {
            var name = "Magickal Bleach";
            var flav = "\"Erase their mistakes.\"";
            var desc = "At the start of combat, remove all passives from this party member and the Left and Right allies.";
            
            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "MagickalBleach")
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
