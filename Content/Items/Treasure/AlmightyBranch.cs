using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class AlmightyBranch
    {
        public static void Init()
        {
            var name = "Almighty Branch";
            var flav = "\"An unholy creation\"";
            var desc = "This party member now deals 20% more damage.\nThis party member is no longer removed from the party on death.";

            var itm = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "AlmightyBranch").AddModifiers(ModdedData("MainCharacterStaticModifier", new MainCharacterStaticModifier())).AddToTreasure().Build();
            itm.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillApplyDamage.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new AddPercentageModifierTriggerEffect(20, true)
                }
            };
        }
    }
}
