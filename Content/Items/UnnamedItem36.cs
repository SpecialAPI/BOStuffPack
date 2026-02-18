using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem36
    {
        public static void Init()
        {
            var name = "Unnamed Item 36";
            var flav = "\"WIP\"";
            var desc = "This party member has \"Transmute\" as an additional ability, an ability that can reroll unwanted pigment.";

            var item = NewItem<BasicWearable>("UnnamedItem36")
                .SetBasicInformation(name, flav, desc, "")
                .AddToTreasure()
                .SetPrice(7);

            var abName = "Transmute";
            var abDesc = "Produce 2 pigment of random colors not used to perform this ability. 50% chance to refresh this party member.";
            var ability = NewAbility("Transmute_A")
                .SetBasicInformation(abName, abDesc, "AttackIcon_Transmute")
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<ProducePigmentNotUsedForAbilityEffect>(x => x.pigmentColors = [Pigments.Yellow, Pigments.Red, Pigments.Blue, Pigments.Purple]), 2),
                    Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot, Effects.ChanceCondition(50))
                })
                .AddIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Mana_Generate.ToString(), IntentType_GameIDs.Other_Refresh.ToString())
                .AddToCharacterDatabase()
                .CharacterAbility(Pigments.Grey, Pigments.Grey);

            item.SetStaticModifiers(ExtraAbilityModifier(ability));
        }
    }
}
