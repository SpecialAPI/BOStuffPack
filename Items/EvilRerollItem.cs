using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class EvilRerollItem
    {
        public static readonly string ID = "EvilRerollItem_SW".Prefix();
        public static readonly string AbilityID = "EvilReroll_A".Prefix();

        public static void Init()
        {
            var abName = "Evil Reroll";
            var abDesc = "Apply 1 Mist to the Opposing enemy. Reroll 1 ability from the Opposing enemy.\nIf the correct pigment was used to perform this ability, refresh this party member.";

            var name = "Evil Reroll Item";
            var flav = "\"It's really evil.\"";
            var desc = $"Adds \"{abName}\" as an additional ability, a cheap but unreliable rerolling ability.";

            var item = NewItem<BasicWearable>(ID)
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(4)
                .AddToShop();

            var ability = NewAbility(AbilityID)
                .SetBasicInformationCharacter(abName, abDesc)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CommonEffects.ApplyField(GrimoireStatusField.Mist), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(CommonEffects.RerollTargetAbilities, 1, Targeting.Slot_Front),

                    Effects.GenerateEffect(CheckWrongPigmentEffect.Create()),
                    Effects.GenerateEffect(CommonEffects.Refresh, 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1))
                })
                .SetIntents(new()
                {
                    TargetIntent(Targeting.Slot_Front, StatusFieldIntents.Field_Mist, IntentType_GameIDs.Misc.ToString()),
                    TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Refresh.ToString(), IntentType_GameIDs.Misc_Hidden.ToString()),
                })
                .CharacterAbility(Pigments.Yellow);

            item.SetStaticModifiers(ExtraAbilityModifier(ability));
        }
    }
}
