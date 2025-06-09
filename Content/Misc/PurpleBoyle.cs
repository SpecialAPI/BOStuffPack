using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public static class PurpleBoyle
    {
        public static void Init()
        {
            var boyle = GetCharacter("Boyle_CH");

            var ch = NewCharacter("PurpleBoyle_CH", "PurpleBoyle")
                .SetBasicInformation("Purple Boyle", Pigments.Red, "PurpleBoyle", "PurpleBoyle_Back", "PurpleBoyle_OW")
                .SetSounds(boyle.damageSound, boyle.deathSound, boyle.dxSound);

            ch.RankedDataSetup(4, (rank, abRank) =>
            {
                return new(RankedValue(15, 20, 20, 25),
                [
                    new()
                    {
                        ability = boyle.rankedData[rank].rankAbilities[0].ability,
                        cost = [Pigments.Purple, Pigments.Purple, Pigments.Yellow]
                    },

                    new()
                    {
                        ability = boyle.rankedData[rank].rankAbilities[1].ability,
                        cost = [Pigments.Purple, Pigments.Purple, Pigments.Purple]
                    },

                    new()
                    {
                        ability = boyle.rankedData[rank].rankAbilities[2].ability,
                        cost = [Pigments.Purple, Pigments.Purple]
                    }
                ]);
            });

            var purpleSlap = NewAbility("PurpleSlap_A")
                .SetBasicInformation("PurpleSlap", "Deal 1 damage to the opposing enemy.\nDamage dealt by this ability always produces purple pigment.", "AttackIcon_Slap")
                .SetVisuals(Visuals.Slap, Targeting.Slot_Front)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<SpecialDamageEffect>(x => x.damageInfo = new() { ProduceSpecialPigment = true, SpecialPigment = Pigments.Purple }), 1, Targeting.Slot_Front)
                })
                .AddIntent(Targeting.Slot_Front, IntentForDamage(1), IntentType_GameIDs.Mana_Generate.ToString())
                .AddToCharacterDatabase()
                .CharacterAbility(Pigments.Yellow);

            ch.SetBasicAbility(purpleSlap);
            ch.AddToDatabase(false);
        }
    }
}
