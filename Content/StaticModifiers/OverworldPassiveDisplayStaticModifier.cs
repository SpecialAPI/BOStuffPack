using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StaticModifiers
{
    [HarmonyPatch]
    public class OverworldPassiveDisplayStaticModifier(List<BasePassiveAbilitySO> passives) : ItemModifierDataSetter
    {
        public List<BasePassiveAbilitySO> passives = passives;

        [HarmonyPatch(typeof(CharacterInGameData), nameof(CharacterInGameData.UpdateCurrentPassives))]
        [HarmonyPostfix]
        public static void AddOverworldDisplayModifierPassives_Postfix(CharacterInGameData __instance)
        {
            if (!__instance.WearableModifiers.ModdedDataSetter.TryGetValue(nameof(OverworldPassiveDisplayStaticModifier), out var mod) || mod is not OverworldPassiveDisplayStaticModifier owPassiveDisplay)
                return;

            var currentPassives = __instance.CurrentPassives;
            currentPassives.AddRange(
                owPassiveDisplay.passives.FindAll(
                    newPassive => !currentPassives.Exists(currPassive => currPassive.m_PassiveID == newPassive.m_PassiveID)));
        }
    }
}
