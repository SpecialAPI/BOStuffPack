using BOStuffPack.Effect;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class BloodyHacksaw
    {
        public static readonly string ID = "BloodyHacksaw_SW".Prefix();
        public static readonly string AbilityID = "TwoIntoOne_A".Prefix();

        public static void Init()
        {
            var name = "Bloody Hacksaw";
            var flav = "\"Two into one!\"";
            var desc = "Adds \"Two Into One\" as an additional ability, a weak attack with the ability to merge enemies.";

            var item = NewItem<BasicWearable>(ID)
                .SetBasicInformation(name, flav, desc, "BloodyHacksaw")
                .SetPrice(7)
                .AddToShop()
                .AddItemTypes(ItemType_GameIDs.Knife.ToString());

            var dmg = 3;
            var abilityName = "Two Into One";
            var abilityDesc = $"If the Left and Right enemies are duplicates, merge them.\nOtherwise, deal {dmg} damage to the Left and Right enemies.";

            var mergeIntent = AddIntent("PA_Merged", "Merged");
            var ab = NewAbility(AbilityID)
                .SetBasicInformationCharacter(abilityName, abilityDesc, "AttackIcon_TwoIntoOne")
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<CheckDuplicateEnemiesEffect>(), 0, Targeting.Slot_OpponentSides),

                    Effects.GenerateEffect(CommonEffects.Animation(Visuals.Equal), 0, Targeting.Slot_OpponentSides, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(CreateScriptable<MergeEnemiesEffect>(), 0, Targeting.Slot_OpponentSides, Effects.CheckPreviousEffectCondition(true, 2)),

                    Effects.GenerateEffect(CommonEffects.Animation(Visuals.Mitosis), 0, Targeting.Slot_OpponentSides, Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(CommonEffects.Damage, dmg, Targeting.Slot_OpponentSides, Effects.CheckPreviousEffectCondition(false, 4)),
                })
                .AddIntent(Targeting.Slot_OpponentSides, IntentType_GameIDs.Misc_Hidden.ToString(), mergeIntent, IntentForDamage(dmg))
                .CharacterAbility(Pigments.Red, Pigments.Blue);

            item.SetStaticModifiers(ExtraAbilityModifier(ab));
        }
    }
}
