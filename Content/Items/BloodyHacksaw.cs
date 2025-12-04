using BOStuffPack.Content.Effect;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
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

            var mergeIntent = AddIntent("PA_Merged", "Merged");
            var ab = NewAbility("TwoIntoOne_A")
                .SetBasicInformation(abilityName, abilityDesc, "AttackIcon_TwoIntoOne")
                .SetVisuals(Visuals.Slash, Targeting.Slot_OpponentSides)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<CheckDuplicateEnemiesEffect>(), 0, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(CreateScriptable<DamageEffect>(), 5, Targeting.Slot_OpponentSides, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(CreateScriptable<DamageEffect>(), 10, Targeting.Slot_OpponentSides, Effects.CheckPreviousEffectCondition(true, 2)),

                    Effects.GenerateEffect(CreateScriptable<CheckDuplicateEnemiesEffect>(), 0, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(CreateScriptable<AnimationVisualsEffect>(x => { x._visuals = Visuals.Equal; x._animationTarget = Targeting.Slot_OpponentSides; }), condition: Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(CreateScriptable<MergeEnemiesEffect>(), 0, Targeting.Slot_OpponentSides, Effects.CheckPreviousEffectCondition(true, 2))
                })
                .AddIntent(Targeting.Slot_OpponentSides, IntentForDamage(5), IntentForDamage(10), mergeIntent)
                .AddToCharacterDatabase()
                .CharacterAbility(Pigments.Red, Pigments.Red, Pigments.YellowPurple);

            var item = NewItem<BasicWearable>("BloodyHacksaw_SW")
                .SetBasicInformation(name, flav, desc, "BloodyHacksaw")
                .SetPrice(5)
                .AddToShop()
                .AddItemTypes(ItemType_GameIDs.Knife.ToString());

            item.SetStaticModifiers(ExtraAbilityModifier(ab));
        }
    }
}
