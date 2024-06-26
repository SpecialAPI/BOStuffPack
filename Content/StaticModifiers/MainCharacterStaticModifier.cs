using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StaticModifiers
{
    [HarmonyPatch]
    public class MainCharacterStaticModifier : ItemModifierDataSetter
    {
        public static MethodInfo mcsm_drmc_mc = AccessTools.Method(typeof(MainCharacterStaticModifier), nameof(MainCharacterStaticModifier_DontRemoveMainCharacters_ModifierCheck));

        [HarmonyPatch(typeof(GameInformationHolder), nameof(GameInformationHolder.PostCombatProcess))]
        [HarmonyILManipulator]
        public static void MainCharacterStaticModifier_DontRemoveMainCharacters_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            foreach(var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<CharacterInGameData>($"get_{nameof(CharacterInGameData.IsMainCharacter)}")))
            {
                crs.Emit(OpCodes.Ldloc_S, (byte)19);
                crs.Emit(OpCodes.Call, mcsm_drmc_mc);
            }
        }

        public static bool MainCharacterStaticModifier_DontRemoveMainCharacters_ModifierCheck(bool curr, CharacterInGameData dat)
        {
            return curr || (dat.WearableModifiers != null && dat.WearableModifiers.TryGetModdedDataSetter("MainCharacterStaticModifier", out var staticDat) && staticDat is MainCharacterStaticModifier);
        }
    }
}
