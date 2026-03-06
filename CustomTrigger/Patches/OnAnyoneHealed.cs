using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger.Patches
{
    [HarmonyPatch]
    internal static class OnAnyoneHealed
    {
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.Heal))]
        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.Heal))]
        [HarmonyILManipulator]
        private static void OnAnyoneHealed_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchCallOrCallvirt<CombatManager>(nameof(CombatManager.PostNotification)), 3))
                return;

            crs.Emit(OpCodes.Ldarg_0);
            crs.EmitStaticDelegate(OnAnyoneHealed_TriggerEvent);
        }

        private static void OnAnyoneHealed_TriggerEvent(IUnit u)
        {
            CombatManager.Instance.ProcessImmediateAction(new TriggerUnitGeneralEventAction(LocalCustomTriggers.OnAnyoneHealed, u));
        }
    }
}
