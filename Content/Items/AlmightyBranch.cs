using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class AlmightyBranch
    {
        public static void Init()
        {
            var name = "Almighty Branch";
            var flav = "\"Divine Blood.\"";
            var desc = "Damage dealt by this party member always produces 1 red and 1 purple pigment instead of the target's health color.";

            var itm = NewItem<MultiCustomTriggerEffectWearable>("AlmightyBranch_TW")
                .SetBasicInformation(name, flav, desc, "AlmightyBranch")
                .SetPrice(3)
                .AddToTreasure();

            itm.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.ModifyCanProducePigmentFromDamage_Anyone,
                    doesPopup = true,
                    immediate = true,

                    effect = new OverridePigmentOnDamageTriggerEffect()
                    {
                        pigments = new()
                        {
                            Pigments.Red,
                            Pigments.Purple
                        }
                    },
                    conditions = new()
                    {
                        CreateScriptable<UnitValueMatchesSenderEffectorCondition>(x => x.unitValueIndex = 1),
                    }
                }
            });
        }
    }
}
