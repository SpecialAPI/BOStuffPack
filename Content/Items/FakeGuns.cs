using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class FakeGuns
    {
        public static void Init()
        {
            var name = "Fake Guns";
            var flav = "\"Are you scared or are you mad?\"";
            var desc = "Adds \"Intimidation\" as an additional ability, an \"attack\" that deals fake damage.";

            var item = NewItem<BasicWearable>("FakGuns_TW")
                .SetBasicInformation(name, flav, desc, "FakeGuns")
                .SetPrice(2)
                .AddToTreasure();

            var abName = "Intimidation";
            var abDesc = "Deals 18 fake damage to the Opposing enemy and this party member.";

            var ab = NewAbility("Intimidation_A")
                .SetBasicInformationCharacter(abName, abDesc, "AttackIcon_Intimidation")
                .SetVisuals(Visuals.Misery, Targeting.Slot_Front.Join(Targeting.Slot_SelfSlot))
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<SpecialDamageEffect>(x => x.damageInfo = new() { FakeDamage = true }), 18, Targeting.Slot_Front),
                    Effects.GenerateEffect(CreateScriptable<SpecialDamageEffect>(x => x.damageInfo = new() { FakeDamage = true }), 18, Targeting.Slot_SelfSlot)
                })
                .SetIntents(new()
                {
                    TargetIntent(Targeting.Slot_Front, MiscIntents.IntentForThreaten(18)),
                    TargetIntent(Targeting.Slot_SelfSlot, MiscIntents.IntentForThreaten(18)),
                })
                .CharacterAbility(Pigments.RedPurple, Pigments.RedPurple);

            item.SetStaticModifiers(ExtraAbilityModifier(ab));
        }
    }
}
