using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class MindHouseCloneEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            CombatManager.Instance.AddSubAction(new MindHouseCloningAction());

            return true;
        }
    }

    public class MindHouseCloningAction : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            var spawnedCharacter = false;
            var spawnedEnemy = false;

            var c = stats.CharactersOnField;
            if (c.Count > 0)
            {
                var chars = new List<CharacterCombat>(c.Values);

                while (chars.Count > 0)
                {
                    var idx = Random.Range(0, chars.Count);

                    var ch = chars[idx];
                    chars.RemoveAt(idx);

                    if (ch == null || ch.Character == null)
                        continue;

                    var fitSlot = stats.GetRandomCharacterSlot();

                    if (fitSlot == -1)
                        continue;

                    CombatManager.Instance.AddSubAction(new SpawnCharacterAction(ch.Character, fitSlot, true, "Mind {0}", false, ch.Rank, ch.UsedAbilities, ch.CurrentHealth));
                    spawnedCharacter = true;
                    break;
                }
            }

            var e = stats.EnemiesOnField;
            if (e.Count > 0)
            {
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

                    CombatManager.Instance.AddSubAction(new SpawnEnemyAction(en.Enemy, fitSlot, true, true, CombatType_GameIDs.Spawn_Basic.ToString()));
                    spawnedEnemy = true;
                    break;
                }
            }

            if (!spawnedCharacter || !spawnedEnemy)
                yield break;

            CombatManager.Instance.AddSubAction(new MindHouseCloningAction());
        }
    }
}
