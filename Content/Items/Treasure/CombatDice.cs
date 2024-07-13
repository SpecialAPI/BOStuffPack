using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class CombatDice
    {
        public static void Init()
        {
            var name = "Combat Dice";
            var flav = "\"Or is it called a douse?\"";
            var desc = "Replaces \"Slap\" with \"Combat Roll\", a pigment rerolling ability with a chance to refresh.\nAdds \"Fury\" as an additional ability, an ability that applies Fury to this party member.";

            var rollName = "Combat Roll";
            var rollDesc = "Generates 1 pigment of a random color.\n75% chance to refresh.";

            var roll =
                NewAbility(rollName, rollDesc, "AttackIcon_CombatRoll", new()
                {
                    Effect(null, CreateScriptable<GenerateRandomManaBetweenEffect>(x => x.possibleMana = new ManaColorSO[] { Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple }), 1),

                    Effect(Self, CreateScriptable<RefreshAbilityUseEffect>()).WithCondition(Chance(75))
                },
                new()
                {
                    TargetIntent(Self, IntentType_GameIDs.Mana_Generate.ToString(), IntentType_GameIDs.Misc.ToString())
                })

                .WithVisuals(Visuals_MiddleFinger, Self)
                .Character(Pigments.Grey);

            var furyName = "Fury";
            var furyDesc = "Apply 1 Fury to this party member.\nIf this party member didn't have Fury before, refresh this party member.";

            var fury =
                NewAbility(furyName, furyDesc, "AttackIcon_Fury", new()
                {
                    Effect(Self, CreateScriptable<StatusEffectCheckerEffect>(x => x._status = Fury)),

                    Effect(Self, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Fury), 1),
                    Effect(Self, CreateScriptable<RefreshAbilityUseEffect>()).WithCondition(Previous(2, false))
                },
                new()
                {
                    TargetIntent(Self, IntentForStatus<FuryStatusEffect>(), IntentType_GameIDs.Misc.ToString())
                })
                
                .WithVisuals(Visuals_Wrath, Self)
                .WithFootnotes("This ability can't be repeated by Fury.")
                .WithFlags("NoFuryRepeat")
                .Character(Pigments.Red, Pigments.Red, Pigments.Red);

            var itm = NewItem<BasicWearable>(name, flav, desc, "CombatDice").AddToTreasure().AddModifiers(BasicAbility(roll), ExtraAbility(fury)).Build();
        }
    }
}
