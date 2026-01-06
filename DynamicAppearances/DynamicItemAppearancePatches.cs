using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.DynamicAppearances
{
    [HarmonyPatch]
    internal static class DynamicItemAppearancePatches
    {
        [HarmonyPatch(typeof(CombatVisualizationController), nameof(CombatVisualizationController.ShowcaseInfoPortraitTooltip))]
        [HarmonyILManipulator]
        private static void ApplyDynamicDescription_Combat_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchLdfld<StringTrioData>(nameof(StringTrioData.description))))
                return;

            crs.Emit(OpCodes.Ldloc_1);
            crs.EmitStaticDelegate(ApplyDynamicDescription_Combat_Wearable);
        }

        private static string ApplyDynamicDescription_Combat_Wearable(string curr, BaseWearableSO item)
        {
            if (item.TryGetDynamicAppearance(out var a))
                a.ModifyItemDescription(ref curr);

            return curr;
        }

        [HarmonyPatch(typeof(ExtraInformationUIHandler), nameof(ExtraInformationUIHandler.SetItemInformation))]
        [HarmonyILManipulator]
        private static void ApplyDynamicDescription_NonCombat_ExtraInfoUI_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchLdfld<StringTrioData>(nameof(StringTrioData.description))))
                return;

            crs.Emit(OpCodes.Ldarg_1);
            crs.EmitStaticDelegate(ApplyDynamicDescription_NonCombat_Wearable);
        }

        private static string ApplyDynamicDescription_NonCombat_Wearable(string curr, BaseWearableSO item)
        {
            if (item.TryGetDynamicAppearance(out var a))
                a.ModifyItemDescription(ref curr);

            return curr;
        }

        [HarmonyPatch(typeof(GoodEndingOverworldManagerBG), nameof(GoodEndingOverworldManagerBG.TryOpenPrizeChest))]
        [HarmonyPatch(typeof(TutorialOverworldManagerBG), nameof(TutorialOverworldManagerBG.TryOpenPrizeChest))]
        [HarmonyPatch(typeof(OverworldManagerBG), nameof(OverworldManagerBG.TryOpenPrizeChest))]
        [HarmonyILManipulator]
        private static void ApplyDynamicDescription_NonCombat_PrizeChest_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchLdfld<StringTrioData>(nameof(StringTrioData.description))))
                return;

            crs.Emit(OpCodes.Ldloc_1);
            crs.EmitStaticDelegate(ApplyDynamicDescription_NonCombat_PrizeChest);
        }

        private static string ApplyDynamicDescription_NonCombat_PrizeChest(string curr, PrizeContentData prize)
        {
            return ApplyDynamicDescription_NonCombat_Wearable(curr, prize.prize);
        }

        [HarmonyPatch(typeof(OverworldManagerBG), nameof(OverworldManagerBG.ProcessBronzoPresent), MethodType.Enumerator)]
        [HarmonyILManipulator]
        private static void ApplyDynamicDescription_NonCombat_BronzoPresent_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchLdfld<StringTrioData>(nameof(StringTrioData.description))))
                return;

            crs.Emit(OpCodes.Ldarg_0);
            crs.EmitStaticDelegate(ApplyDynamicDescription_NonCombat_BronzoPresent);
        }

        private static string ApplyDynamicDescription_NonCombat_BronzoPresent(string curr, IEnumerator enumerator)
        {
            return ApplyDynamicDescription_NonCombat_Wearable(curr, enumerator.EnumeratorGetField<BaseWearableSO>("item"));
        }
    }
}
