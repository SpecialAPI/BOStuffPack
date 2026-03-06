using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StaticModifiers
{
    [HarmonyPatch]
    public class MainCharacterStaticModifier : ItemModifierDataSetter
    {
        [HarmonyPatch(typeof(GameInformationHolder), nameof(GameInformationHolder.PostCombatProcess))]
        [HarmonyILManipulator]
        public static void MainCharacterStaticModifier_DontRemoveMainCharacters_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            foreach(var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<CharacterInGameData>($"get_{nameof(CharacterInGameData.IsMainCharacter)}")))
            {
                crs.Emit(OpCodes.Ldloc_S, (byte)19);
                crs.EmitStaticDelegate(MainCharacterStaticModifier_DontRemoveMainCharacters_ModifierCheck);
            }
        }

        public static bool MainCharacterStaticModifier_DontRemoveMainCharacters_ModifierCheck(bool curr, CharacterInGameData dat)
        {
            return curr || (dat.WearableModifiers != null && dat.WearableModifiers.TryGetModdedDataSetter("MainCharacterStaticModifier", out var staticDat) && staticDat is MainCharacterStaticModifier);
        }
    }
}
