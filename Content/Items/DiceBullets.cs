using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class DiceBullets
    {
        public static void Init()
        {
            var name = "Dice Bullets";
            var flav = "\"Gamblers Never Lose.\"";
            var desc = "Damage dealt by this party member is randomized between 4 and double the original amount of damage they would've dealt.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("DiceBullets_SW")
                .SetBasicInformation(name, flav, desc, "DiceBullets")
                .SetPrice(7)
                .AddToShop();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillApplyDamage.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new RandomizeBetweenValuesModifierSetterTriggerEffect(4, false, 200, true) 
                }
            });
        }
    }
}
