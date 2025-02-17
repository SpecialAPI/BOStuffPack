using System;
using System.Collections.Generic;
using System.Text;
using Pentacle.Tools;

namespace BOStuffPack.Content.StaticModifiers
{
    [HarmonyPatch]
    public class BleachStaticModifier : ItemModifierDataSetter
    {
        public static MethodInfo hfc_rp = AccessTools.Method(typeof(BleachStaticModifier), nameof(HandleFlagsChange_RecoverPassives));
        public static MethodInfo hfc_dp = AccessTools.Method(typeof(BleachStaticModifier), nameof(HandleFlagsChange_DisconnectPassives));

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.DefaultPassiveAbilityInitialization))]
        [HarmonyPrefix]
        public static bool MaybeDontAddPassives(CharacterCombat __instance)
        {
            return __instance.CharacterWearableModifiers == null || !__instance.CharacterWearableModifiers.TryGetModdedDataSetter("BleachStaticModifier", out var mod) || mod is not BleachStaticModifier;
        }

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.TrySetUpNewItem))]
        [HarmonyILManipulator]
        public static void HandleFlagsChange_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            foreach(var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<BaseWearableSO>(nameof(BaseWearableSO.OnTriggerDettached))))
            {
                crs.Emit(OpCodes.Ldarg_0);

                crs.Emit(OpCodes.Call, hfc_rp);
            }

            crs.Index = 0;
            foreach(var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<WearableStaticModifiers>(nameof(WearableStaticModifiers.ProcessModdedDataFromNewItem))))
            {
                crs.Emit(OpCodes.Ldarg_0);

                crs.Emit(OpCodes.Call, hfc_dp);
            }
        }

        public static void HandleFlagsChange_RecoverPassives(CharacterCombat cc)
        {
            if (cc.CharacterWearableModifiers == null || !cc.CharacterWearableModifiers.TryGetModdedDataSetter("BleachStaticModifier", out var mod) || mod is not BleachStaticModifier)
                return;

            cc.CharacterWearableModifiers.RemoveModdedData("BleachStaticModifier");
            cc.DefaultPassiveAbilityInitialization();
        }

        public static void HandleFlagsChange_DisconnectPassives(CharacterCombat cc)
        {
            if (cc.CharacterWearableModifiers == null || !cc.CharacterWearableModifiers.TryGetModdedDataSetter("BleachStaticModifier", out var mod) || mod is not BleachStaticModifier)
                return;

            cc.RemoveAndDisconnectAllPassiveAbilities();
        }
    }
}
