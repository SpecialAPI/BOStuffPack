using BOStuffPack.Content.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StaticModifiers
{
    public class SavedExtraAbilityAndPassiveStaticModifier : WearableStaticModifierSetterSO
    {
        public string abilityDataKey;
        public string passiveDataKey;

        public override void OnAttachedToCharacter(WearableStaticModifiers modifiers, CharacterSO character, int rank)
        {
            modifiers.ExtraAbilityModifier = null;
            modifiers.ExtraPassiveAbilities.Clear();

            if (string.IsNullOrEmpty(abilityDataKey) || string.IsNullOrEmpty(passiveDataKey))
                return;

            var infoHolder = LoadedDBsHandler.InfoHolder;
            if (infoHolder == null)
                return;

            if (infoHolder.Run == null || infoHolder.Run.InGameData is not IInGameRunData dat)
                return;

            if (WrittenBook.DeserializeAbility(dat.GetStringData(abilityDataKey), out var ab))
                modifiers.ExtraAbilityModifier = ab;
            if (WrittenBook.DeserializePassive(dat.GetStringData(passiveDataKey), out var pa))
                modifiers.AddExtraPassive(pa);
        }

        public override void OnDettachedFromCharacter(WearableStaticModifiers modifiers)
        {
            modifiers.ExtraAbilityModifier = null;
            modifiers.ExtraPassiveAbilities.Clear();
        }
    }
}
