using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger.Patches
{
    [HarmonyPatch]
    internal static class OnTargetBeingDamaged
    {
        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.Damage))]
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.Damage))]
        [HarmonyILManipulator]
        private static void OnTargetBeingDamaged_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchCallOrCallvirt<CombatManager>(nameof(CombatManager.PostNotification))))
                return;

            crs.Emit(OpCodes.Ldloc_2);
            crs.EmitStaticDelegate(OnTargetBeingDamaged_TriggerEvent);
        }

        private static void OnTargetBeingDamaged_TriggerEvent(DamageReceivedValueChangeException ex)
        {
            if (ex.possibleSourceUnit is IUnit attacker)
                CombatManager.Instance.PostNotification(LocalCustomTriggers.OnTargetBeingDamaged, attacker, ex);
        }
    }
}
