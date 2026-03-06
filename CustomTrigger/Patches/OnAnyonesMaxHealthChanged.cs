using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger.Patches
{
    [HarmonyPatch]
    internal static class OnAnyonesMaxHealthChanged
    {
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.MaximizeHealth))]
        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.MaximizeHealth))]
        [HarmonyILManipulator]
        private static void OnAnyonesMaxHealthChanged_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchCallOrCallvirt<CombatManager>(nameof(CombatManager.PostNotification))))
                return;

            crs.Emit(OpCodes.Ldarg_0);
            crs.EmitStaticDelegate(OnAnyonesMaxHealthChanged_TriggerEvent);
        }

        private static void OnAnyonesMaxHealthChanged_TriggerEvent(IUnit u)
        {
            CombatManager.Instance.ProcessImmediateAction(new TriggerUnitGeneralEventAction(LocalCustomTriggers.OnAnyonesMaxHealthChanged, u));
        }
    }
}
