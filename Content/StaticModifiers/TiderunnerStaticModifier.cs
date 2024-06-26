using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StaticModifiers
{
    [HarmonyPatch]
    public class TiderunnerStaticModifier : ItemModifierDataSetter
    {
        [HarmonyPatch(typeof(CharacterInGameData), nameof(CharacterInGameData.UpdateCurrentAbilities))]
        [HarmonyPostfix]
        public static void TiderunnerStaticModifier_MoveAbilities_NonCombat(CharacterInGameData __instance)
        {
            if(__instance.WearableModifiers.TryGetModdedDataSetter("TiderunnerStaticModifier", out var dat) && dat is TiderunnerStaticModifier)
            {
                if (__instance.Character.HasRankedData && __instance.CurrentAbilities.Count > 1)
                {
                    var rank = __instance.Character.rankedData[__instance.Rank].rankAbilities;

                    var firstIdx = __instance.CurrentAbilities.FindIndex(x => rank.Any(x2 => x2.ability == x.ability));
                    var lastIdx = __instance.CurrentAbilities.FindLastIndex(x => rank.Any(x2 => x2.ability == x.ability));

                    if(firstIdx > 0)
                    {
                        var firstItem = __instance.CurrentAbilities[firstIdx];

                        __instance.CurrentAbilities.RemoveAt(firstIdx);
                        __instance.CurrentAbilities.Insert(0, firstItem);
                    }

                    if(lastIdx >= 0 && lastIdx < __instance.CurrentAbilities.Count - 1 && lastIdx != firstIdx)
                    {
                        var lastItem = __instance.CurrentAbilities[lastIdx];

                        __instance.CurrentAbilities.RemoveAt(lastIdx);
                        __instance.CurrentAbilities.Add(lastItem);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.SetUpDefaultAbilities))]
        [HarmonyPostfix]
        public static void TiderunnerStaticModifier_MoveAbilities_Combat(CharacterCombat __instance)
        {
            if (__instance.CharacterWearableModifiers.TryGetModdedDataSetter("TiderunnerStaticModifier", out var dat) && dat is TiderunnerStaticModifier)
            {
                if (__instance.Character.HasRankedData && __instance.CombatAbilities.Count > 1)
                {
                    var rank = __instance.Character.rankedData[__instance.Rank].rankAbilities;

                    var firstIdx = __instance.CombatAbilities.FindIndex(x => rank.Any(x2 => x2.ability == x.ability));
                    var lastIdx = __instance.CombatAbilities.FindLastIndex(x => rank.Any(x2 => x2.ability == x.ability));

                    if (firstIdx == lastIdx)
                        return;

                    if (firstIdx > 0)
                    {
                        var firstItem = __instance.CombatAbilities[firstIdx];

                        __instance.CombatAbilities.RemoveAt(firstIdx);
                        __instance.CombatAbilities.Insert(0, firstItem);
                    }

                    if (lastIdx >= 0 && lastIdx < __instance.CombatAbilities.Count - 1)
                    {
                        var lastItem = __instance.CombatAbilities[lastIdx];

                        __instance.CombatAbilities.RemoveAt(lastIdx);
                        __instance.CombatAbilities.Add(lastItem);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.AddExtraAbility))]
        [HarmonyPostfix]
        public static void TiderunnerStaticModifier_MoveAbilities_CombatExtra(CharacterCombat __instance)
        {
            if (__instance.CharacterWearableModifiers.TryGetModdedDataSetter("TiderunnerStaticModifier", out var dat) && dat is TiderunnerStaticModifier)
            {
                if (__instance.Character.HasRankedData && __instance.CombatAbilities.Count > 1)
                {
                    var rank = __instance.Character.rankedData[__instance.Rank].rankAbilities;

                    var firstIdx = __instance.CombatAbilities.FindIndex(x => rank.Any(x2 => x2.ability == x.ability));
                    var lastIdx = __instance.CombatAbilities.FindLastIndex(x => rank.Any(x2 => x2.ability == x.ability));

                    if (firstIdx == lastIdx)
                        return;

                    if (lastIdx >= 0 && lastIdx < __instance.CombatAbilities.Count - 1)
                    {
                        var lastItem = __instance.CombatAbilities[lastIdx];

                        __instance.CombatAbilities.RemoveAt(lastIdx);
                        __instance.CombatAbilities.Add(lastItem);
                    }
                }
            }
        }
    }
}
