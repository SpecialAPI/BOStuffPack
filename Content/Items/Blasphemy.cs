using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class Blasphemy
    {
        public static void Init()
        {
            var name = "Blasphemy";
            var flav = "\"Godslayer.\"";
            var desc = "This party member has Anointed (1) as a passive. Round up all damage dealt by this party member to 50% of its original value. This is applied after all damage modifiers on the target.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("Blasphemy_TW")
                .SetBasicInformation(name, flav, desc, "Blasphemy")
                .SetStaticModifiers(ExtraPassiveModifier(Passives.Anointed1))
                .SetPrice(13)
                .AddToTreasure();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnAnyoneBeingDamaged.ToString(),
                    immediate = true,
                    doesPopup = true,
                    
                    effect = new MaximizeValueToPercentOfOriginalLateTriggerEffect(50),
                    conditions = new()
                    {
                        CreateScriptable<UnitValueMatchesSenderEffectorCondition>(x => x.unitValueIndex = 0)
                    }
                }
            });
        }
    }
}
