using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.StaticModifiers
{
    public class ResetEverythingOnDisconnectStaticModifier : WearableStaticModifierSetterSO
    {
        public override void OnAttachedToCharacter(WearableStaticModifiers modifiers, CharacterSO character, int rank)
        {
        }

        public override void OnDettachedFromCharacter(WearableStaticModifiers modifiers)
        {
            modifiers.MaximumHealthModifier = 0;
            modifiers.HealthColorModifier = null;
            modifiers.BasicAbilityModifier = null;
            modifiers.ExtraAbilityModifier = null;
            modifiers.ExtraPassiveAbilities.Clear();
            modifiers.CurrencyMultiplierModifier = 0;
            modifiers.RankModifier = 0;
            modifiers.UsesBasicBool = false;
            modifiers.UsesBasicAbilityModifier = false;
            modifiers.UsesAllAbilitiesBool = false;
            modifiers.UsesAllAbilitiesModifier = false;
            modifiers.IsMainCharacterModifier = false;
            modifiers.ModdedDataSetter.Clear();
        }
    }
}
