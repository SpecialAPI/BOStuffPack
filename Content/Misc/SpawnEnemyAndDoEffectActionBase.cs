using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public abstract class SpawnEnemyAndDoEffectActionBase(EnemySO enemy, int preferredSlot, bool givesExperience, bool trySpawnAnyways, string spawnSoundID = nameof(CombatType_GameIDs.Spawn_Basic), int maxHealth = 0) : CombatAction
    {
        public int preferredSlot = preferredSlot;
        public EnemySO enemy = enemy;
        public bool givesExperience = givesExperience;
        public bool trySpawnAnyways = trySpawnAnyways;
        public string spawnSoundID = spawnSoundID;
        public int maxHealth = maxHealth;

        public override IEnumerator Execute(CombatStats stats)
        {
            if (enemy == null)
                yield break;

            var spawnSlot = -1;
            if (preferredSlot >= 0)
                spawnSlot = stats.combatSlots.GetEnemyFitSlot(preferredSlot, enemy.size);

            if (spawnSlot < 0)
            {
                if (preferredSlot >= 0 && !trySpawnAnyways)
                    yield break;

                spawnSlot = stats.GetRandomEnemySlot(enemy.size);
            }

            if (spawnSlot < 0)
                yield break;

            if(!stats.AddNewEnemyWithOutput(enemy, spawnSlot, givesExperience, spawnSoundID, maxHealth, out var spawnedEnemy))
                yield break;

            yield return DoEffectOnEnemy(spawnedEnemy);
        }

        public abstract IEnumerator DoEffectOnEnemy(EnemyCombat spawnedEnemy);
    }
}
