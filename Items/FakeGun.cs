using BOStuffPack.Effect;
using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class FakeGun
    {
        public static void Init()
        {
            var name = "Fake Gun";
            var flav = "\"It can't actually shoot.\"";
            var desc = "Adds \"Intimidation\" as an additional ability, an \"attack\" that deals fake damage.";

            var item = NewItem<BasicWearable>("FakeGun_TW")
                .SetBasicInformation(name, flav, desc, "FakeGunPlaceholder")
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
