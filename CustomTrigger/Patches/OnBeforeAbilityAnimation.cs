using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger.Patches
{
    [HarmonyPatch]
    internal static class OnBeforeAbilityAnimation
    {
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.UseAbility))]
        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.UseAbility))]
        [HarmonyILManipulator]
        private static void OnBeforeAbilityAnimation_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchNewobj<PlayAbilityAnimationAction>()))
                return;

            crs.Emit(OpCodes.Ldarg_0);
            crs.EmitStaticDelegate(OnBeforeAbilityAnimation_TriggerEvent);
        }

        private static PlayAbilityAnimationAction OnBeforeAbilityAnimation_TriggerEvent(PlayAbilityAnimationAction curr, IUnit unit)
        {
            CombatManager.Instance.AddRootAction(new PerformDelegateAction(x =>
            {
                CombatManager.Instance.PostNotification(LocalCustomTriggers.OnBeforeAbilityAnimation, unit, null);
            }));

            return curr;
        }
    }
}
