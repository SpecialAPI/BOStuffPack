using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger.Patches
{
    [HarmonyPatch]
    internal static class OnAnyoneDamaged
    {
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.Damage))]
        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.Damage))]
        [HarmonyILManipulator]
        private static void OnAnyoneDamaged_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchCallOrCallvirt<CombatManager>(nameof(CombatManager.PostNotification)), 2))
                return;

            crs.Emit(OpCodes.Ldloc, 8);
            crs.Emit(OpCodes.Ldarg, 6);
            crs.EmitStaticDelegate(OnAnyoneDamaged_TriggerEvent);
        }

        private static void OnAnyoneDamaged_TriggerEvent(IntegerReference_Damage intref, bool direct)
        {
            if (intref.possibleSourceUnit is IUnit attacker)
                CombatManager.Instance.PostNotification(LocalCustomTriggers.OnTargetDamaged, attacker, intref);

            CombatManager.Instance.ProcessImmediateAction(new TriggerUnitGeneralEventAction(LocalCustomTriggers.OnAnyoneDamaged, intref));

            if(direct)
                CombatManager.Instance.ProcessImmediateAction(new TriggerUnitGeneralEventAction(LocalCustomTriggers.OnAnyoneDirectDamaged, intref));
        }
    }
}
