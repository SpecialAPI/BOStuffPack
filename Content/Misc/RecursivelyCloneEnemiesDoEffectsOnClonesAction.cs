using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class RecursivelyCloneEnemiesDoEffectsOnClonesAction(EffectInfo[] effects, bool immediateEffects) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            var e = stats.EnemiesOnField;
            if (e.Count <= 0)
                yield break;

            var ens = new List<EnemyCombat>(e.Values);
            while (ens.Count > 0)
            {
                var idx = Random.Range(0, ens.Count);

                var en = ens[idx];
                ens.RemoveAt(idx);

                if (en == null || en.Enemy == null)
                    continue;

                var fitSlot = stats.GetRandomEnemySlot(en.Size);

                if (fitSlot == -1)
                    continue;

                CombatManager.Instance.AddSubAction(new SpawnEnemyWithPerformedEffectsAction(en.Enemy, fitSlot, true, true, effects, immediateEffects, CombatType_GameIDs.Spawn_Basic.ToString()));
                CombatManager.Instance.AddSubAction(new RecursivelyCloneEnemiesDoEffectsOnClonesAction(effects, immediateEffects));
                break;
            }
        }
    }
}
