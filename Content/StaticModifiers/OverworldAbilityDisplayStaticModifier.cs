using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StaticModifiers
{
    [HarmonyPatch]
    public class OverworldAbilityDisplayStaticModifier(List<CharacterAbility> abilties) : ItemModifierDataSetter
    {
        public List<CharacterAbility> abilties = abilties;

        [HarmonyPatch(typeof(CharacterInGameData), nameof(CharacterInGameData.UpdateCurrentAbilities))]
        [HarmonyPostfix]
        public static void AddOverworldDisplayModifierAbilities_Postfix(CharacterInGameData __instance)
        {
            if (!__instance.WearableModifiers.ModdedDataSetter.TryGetValue(nameof(OverworldAbilityDisplayStaticModifier), out var mod) || mod is not OverworldAbilityDisplayStaticModifier owAbilityDisplay)
                return;

            __instance.CurrentAbilities.AddRange(owAbilityDisplay.abilties);
        }
    }
}
