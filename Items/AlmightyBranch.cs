using BOStuffPack.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class AlmightyBranch
    {
        public static readonly string ID = "AlmightyBranch_TW".Prefix();

        public static void Init()
        {
            var name = "Almighty Branch";
            var flav = "\"Divine Blood.\"";
            var desc = "Damage dealt by this party member always produces 1 red and 1 pigment of this party member's health color instead of what would normally be produced.";

            var itm = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "AlmightyBranch")
                .SetPrice(3)
                .AddToTreasure();

            itm.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.ModifyTargetCanProducePigmentFromDamage,
                    doesPopup = true,
                    immediate = true,

                    effect = new OverridePigmentOnDamageTriggerEffect([Pigments.Red, null], true)
                }
            });
        }
    }
}
