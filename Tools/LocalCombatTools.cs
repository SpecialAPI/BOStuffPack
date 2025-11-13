using BOStuffPack.ReversePatches;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class LocalCombatTools
    {
        public static bool AddNewEnemyWithOutput(this CombatStats stats, EnemySO enemy, int slot, bool givesExperience, string spawnSoundID, int maxHealth, out EnemyCombat spawnedEnemy)
        {
            if (!ReversePatchesFinished)
            {
                Debug.LogError($"Trying to do AddNewEnemyWithOutput before reverse patches are finished. This shouldn't happen.");
                spawnedEnemy = null;
                return false;
            }

            return AddNewEnemyOutputReversePatch.AddNewEnemyWithOutput_ReversePatch(stats, enemy, slot, givesExperience, spawnSoundID, maxHealth, out spawnedEnemy);
        }
    }
}
