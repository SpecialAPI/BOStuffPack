using BOStuffPack.Content.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Shop
{
    public static class BloodyHacksaw
    {
        public static void Init()
        {
            var name = "Bloody Hacksaw";
            var flav = "\"Two into one!\"";
            var desc = "Adds \"Two Into One\" as an additional ability, a weak attack with the ability to merge enemies.";

            var abilityName = "Two Into One";
            var abilityDesc = "Deal 5 damage to the left and right enemies.\nIf the left and right enemies are duplicates, deal double damage and merge them if they survive.";

            var ab =
                NewAbility(abilityName, abilityDesc, "AttackIcon_TwoIntoOne",

                new()
                {
                    Effect(Relative(false, -1, 1), CreateScriptable<CheckDuplicateEnemiesEffect>()),

                    Effect(Relative(false, -1, 1), Damage, 5).WithCondition(Previous(1, false)),
                    Effect(Relative(false, -1, 1), Damage, 10).WithCondition(Previous(2, true)),

                    Effect(Relative(false, -1, 1), CreateScriptable<CheckDuplicateEnemiesEffect>()),
                    Effect(Relative(false, -1, 1), PlayVisuals(Visuals_Connection)).WithCondition(Previous(1, true)),
                    Effect(Relative(false, -1, 1), CreateScriptable<MergeEnemiesEffect>()).WithCondition(Previous(2, true)),
                },

                new()
                {
                    TargetIntent(Relative(false, -1, 1), IntentType_GameIDs.Damage_3_6.ToString(), IntentType_GameIDs.Damage_7_10.ToString(), IntentType_GameIDs.Misc.ToString())
                })

                .WithVisuals(Visuals_Slash, Relative(false, -1, 1))
                .Character(Pigments.Red, Pigments.Red, Pigments.Yellow);

            var item = NewItem<BasicWearable>(name, flav, desc, "BloodyHacksaw").AddToShop(5).AddModifiers(ExtraAbility(ab)).Build();
        }
    }
}
