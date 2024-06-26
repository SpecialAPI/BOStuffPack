using BOStuffPack.Content.Passive;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class MergeEnemiesEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (targets.Length <= 1)
                return false;

            var mergeData = new Dictionary<EnemySO, List<EnemyCombat>>();

            foreach (var target in targets)
            {
                if (target == null || !target.HasUnit || target.Unit is not EnemyCombat ec || !ec.IsAlive || ec.CurrentHealth <= 0)
                    continue;

                if (!mergeData.TryGetValue(ec.Enemy, out var dat))
                    mergeData[ec.Enemy] = dat = new();

                if (!dat.Contains(ec))
                    dat.Add(ec);
            }

            foreach (var kvp in mergeData)
            {
                var en = kvp.Key;
                var targettedEnemies = kvp.Value;

                if (en == null || targettedEnemies.Count <= 1)
                    continue;

                var currenthealth = 0;
                var maxhealth = 0;
                var extraabilities = -1;

                foreach (var e in targettedEnemies)
                {
                    currenthealth += e.CurrentHealth;
                    maxhealth += e.MaximumHealth;
                    extraabilities += e.GetIntStoredValue(StoredValue_MergedCount._UnitStoreDataID) + 1;
                }

                if (currenthealth > 0 && maxhealth > 0 && extraabilities >= 0)
                {
                    foreach (var e in targettedEnemies)
                    {
                        e.DirectDeath(null, false);
                    }

                    CombatManager.Instance.AddSubAction(new TrySpawnMergedEnemyAction(en, true, maxhealth, currenthealth, extraabilities));
                    exitAmount += targettedEnemies.Count;
                }
            }
            return exitAmount > 0;
        }
    }

    public class TrySpawnMergedEnemyAction(EnemySO enemy, bool givesExperience, int maxHealth, int currentHealth, int extraAbilities) : CombatAction
    {
        public EnemySO enemy = enemy;
        public bool experience = givesExperience;
        public int healthMax = maxHealth;
        public int health = currentHealth;
        public int extraAbilities = extraAbilities;

        public override IEnumerator Execute(CombatStats stats)
        {
            var slot = stats.GetRandomEnemySlot(enemy.size);
            if (slot != -1)
            {
                stats.AddNewEnemy(enemy, slot, experience, "Spawn_Basic");

                if (stats.combatSlots.EnemySlots[slot].Unit is EnemyCombat en)
                {
                    en.MaximumHealth = healthMax;
                    en.CurrentHealth = Math.Min(healthMax, health);
                    en.SetIntStoredValue(StoredValue_MergedCount._UnitStoreDataID, extraAbilities);
                    en.AddPassiveAbility(Merged);
                }
            }
            yield return null;
        }
    }
}
