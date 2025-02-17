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
            var flav = "\"Divine blood\"";
            var desc = "Damage dealt by this party member always produces 1 red and 1 purple pigment instead of the target's health color.";

            var itm = NewItem<MultiCustomTriggerEffectWearable>("AlmightyBranch_TW")
                .SetBasicInformation(name, flav, desc, "AlmightyBranch")
                .SetPrice(3)
                .AddToTreasure();

            itm.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillApplyDamage.ToString(),
                    immediate = true,
                    doesPopup = true,
            
                    effect = new PickyEaterBonusDamageSetterTriggerEffect()
                }
            };

            var seu = new Ability("SEU_A")
            {
                Name = "Single-Event Upset",
                Description = "Flip a random bit in the opposing enemy's current health value.",
                AbilitySprite = ResourceLoader.LoadSprite("AttackIcon_SEU"),
                Visuals = Visuals.Poke,
                AnimationTarget = Targeting.Slot_Front,
                Effects = new EffectInfo[]
                {
                    Effects.GenerateEffect(CreateScriptable<TargetRandomHealthBitflipEffect>(), 0, Targeting.Slot_Front)
                },
                Cost = [Pigments.Yellow, Pigments.Yellow, Pigments.Yellow]
            };
            seu.AddIntentsToTarget(Targeting.Slot_Front, [IntentType_GameIDs.Other_MaxHealth_Alt.ToString()]);

            itm.SetStaticModifiers(CreateScriptable<ExtraAbility_Wearable_SMS>(x => x._extraAbility = seu.GenerateCharacterAbility()));
        }
    }
}
