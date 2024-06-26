using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.Advanced
{
    public class AdvancedAbilitySO : AbilitySO
    {
        public string Footnotes;
        public List<RichTextProcessorBase> RichTextProcessors = [];

        public List<string> Flags = [];
    }

    [HarmonyPatch]
    public static class AdvancedAbilityPatches
    {
        public static MethodInfo aa_f_atd = AccessTools.Method(typeof(AdvancedAbilityPatches), nameof(AdvancedAbility_Footnotes_AddToDescription));

        [HarmonyPatch(typeof(CombatVisualizationController), nameof(CombatVisualizationController.ShowcaseInfoAttackTooltip))]
        [HarmonyILManipulator]
        public static void AdvancedAbility_Footnotes_Transpiler(ILContext ctx)
        {
            var cursor = new ILCursor(ctx);

            foreach(var m in cursor.MatchAfter(x => x.MatchLdfld<StringPairData>(nameof(StringPairData.description))))
            {
                cursor.Emit(OpCodes.Ldloc_0);
                cursor.Emit(OpCodes.Call, aa_f_atd);
            }
        }

        public static string AdvancedAbility_Footnotes_AddToDescription(string desc, AbilitySO ab)
        {
            if (ab is not AdvancedAbilitySO adv)
                return desc;

            if (!string.IsNullOrEmpty(adv.Footnotes))
            {
                var footnote = adv.Footnotes;

                foreach (var process in adv.RichTextProcessors)
                {
                    footnote = process.ProcessReplacements(footnote);
                }

                desc += $"\n{footnote.Scale(75).Colorize(Color.grey).VerticalOffset(-0.5f)}";
            }

            return desc;
        }
    }
}
