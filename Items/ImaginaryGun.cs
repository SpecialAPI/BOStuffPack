using BOStuffPack.Effect;
using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class ImaginaryGun
    {
        public static readonly string ID = "ImaginaryGun_TW".Prefix();

        public static void Init()
        {
            var name = "Imaginary Gun";
            var flav = "\"You need to believe in it.\"";
            var desc = "Adds \"Intimidation\" as an additional ability, an \"attack\" that deals fake damage.";

            var item = NewItem<BasicWearable>(ID)
                .SetBasicInformation(name, flav, desc, "FakeGunPlaceholder")
                .SetPrice(2)
                .AddToTreasure();

            var abName = "Intimidation";
            var abDesc = "Deals 18 fake damage to the Opposing enemy and this party member.";

            var ab = NewAbility(AbilityIDs.Intimidation)
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
