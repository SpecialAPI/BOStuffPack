using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class SpawnEnemyWithPerformedEffectsAction(EnemySO enemy, int preferredSlot, bool givesExperience, bool trySpawnAnyways, EffectInfo[] effects, bool immediateEffects, string spawnSoundID = nameof(CombatType_GameIDs.Spawn_Basic), int maxHealth = 0) : SpawnEnemyAndDoEffectActionBase(enemy, preferredSlot, givesExperience, trySpawnAnyways, spawnSoundID, maxHealth)
    {
        public override IEnumerator DoEffectOnEnemy(EnemyCombat spawnedEnemy)
        {
            if (immediateEffects)
                CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction(effects, spawnedEnemy));
            else
                CombatManager.Instance.AddSubAction(new EffectAction(effects, spawnedEnemy));

            yield break;
        }
    }
}
