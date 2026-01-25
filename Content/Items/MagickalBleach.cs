using BOStuffPack.Content.StaticModifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class MagickalBleach
    {
        public static void Init()
        {
            var name = "Magickal Bleach";
            var flav = "\"Erase their mistakes.\"";
            var desc = "At the start of combat, remove all passives from all party members.";
            
            var item = NewItem<MultiCustomTriggerEffectWearable>("MagickalBleach_TW")
                .SetBasicInformation(name, flav, desc, "MagickalBleach")
                //.SetStaticModifiers(ModdedDataModifier(new BleachStaticModifier()))
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
                        Effects.GenerateEffect(CreateScriptable<RemoveAllPassives_Effect>(), 0, Targeting.Unit_AllAllies)
                    })
                }
            });
        }
    }
}
