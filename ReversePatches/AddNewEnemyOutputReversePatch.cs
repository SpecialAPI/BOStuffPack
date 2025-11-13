using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.ReversePatches
{
    public static class AddNewEnemyOutputReversePatch
    {
        [HarmonyPatch(typeof(CombatStats), nameof(CombatStats.AddNewEnemy))]
        [HarmonyReversePatch(HarmonyReversePatchType.Snapshot)]
        public static bool AddNewEnemyWithOutput_ReversePatch(CombatStats self, EnemySO enemy, int slot, bool givesExperience, string spawnSoundID, int maxHealth, out EnemyCombat spawnedEnemy)
        {
            static void ILManipulator(ILContext ctx)
            {
                if (ctx == null)
                    return;

                var crs = new ILCursor(ctx);

                crs.Emit(OpCodes.Ldarg, 6);
                crs.Emit(OpCodes.Ldnull);
                crs.Emit(OpCodes.Stind_Ref);

                if (!crs.JumpToNext(x => x.MatchStloc(2)))
                    return;

                crs.Emit(OpCodes.Ldarg, 6);
                crs.Emit(OpCodes.Ldloc_2);
                crs.Emit(OpCodes.Stind_Ref);
            }

            ILManipulator(null);
            spawnedEnemy = default;
            return default;
        }
    }
}
