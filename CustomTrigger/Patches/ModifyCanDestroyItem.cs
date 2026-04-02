using BOStuffPack.CustomTrigger.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger.Patches
{
    [HarmonyPatch]
    internal class ModifyCanDestroyItem
    {
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.TryConsumeWearable))]
        [HarmonyILManipulator]
        private static void ModifyCanDestroyItem_TryConsumeWearable_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchCallOrCallvirt<CharacterCombat>($"get_{nameof(CharacterCombat.IsWearableConsumed)}")))
                return;

            crs.Emit(OpCodes.Ldarg_0);
            crs.EmitStaticDelegate(ModifyCanDestroyItem_TryConsumeWearable_TriggerEvent);
        }

        private static bool ModifyCanDestroyItem_TryConsumeWearable_TriggerEvent(bool curr, IUnit unit)
        {
            if(curr)
                return true;

            var canDestroyRef = new ModifyCanDestroyItemReference(true, unit, unit.HeldItem);
            CombatManager.Instance.PostNotification(LocalCustomTriggers.ModifyCanDestroyItem, unit, canDestroyRef);

            return !canDestroyRef.canDestroy;
        }

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.ConsumeWearable))]
        [HarmonyPrefix]
        private static bool ModifyCanDestroyItem_ConsumeWearable_Prefix(CharacterCombat __instance)
        {
            var canDestroyRef = new ModifyCanDestroyItemReference(true, __instance, __instance.HeldItem);
            CombatManager.Instance.PostNotification(LocalCustomTriggers.ModifyCanDestroyItem, __instance, canDestroyRef);

            return canDestroyRef.canDestroy;
        }
    }
}
