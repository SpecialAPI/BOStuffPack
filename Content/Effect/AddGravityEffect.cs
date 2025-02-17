using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class AddGravityEffect : EffectSO
    {
        public ColliderType collider;
        public Vector3 launchForce;
        public float randomForce;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach (var t in targets)
            {
                if (t == null || !t.HasUnit)
                    continue;

                CombatManager.Instance.AddUIAction(new AddGravityUIAction(t.Unit.ID, t.Unit.IsUnitCharacter, collider, launchForce, randomForce));
                exitAmount++;
            }

            return exitAmount > 0;
        }
    }

    public class AddGravityUIAction(int id, bool isCharacter, ColliderType collider, Vector3 launchForce, float randomForce) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            if (!isCharacter && stats.combatUI._enemiesInCombat.TryGetValue(id, out var enemy) && enemy.FieldID >= 0)
            {
                var eif = stats.combatUI._enemyZone._enemies[enemy.FieldID].FieldEntity;
                var rb = eif.GetOrAddComponent<Rigidbody>();

                if (collider != ColliderType.None)
                {
                    Type keepType = collider switch
                    {
                        ColliderType.Cube => typeof(BoxCollider),
                        ColliderType.Sphere => typeof(SphereCollider),
                        ColliderType.Capsule => typeof(CapsuleCollider),

                        _ => null
                    };

                    if (keepType != null)
                    {
                        var comps = eif.GetComponents<Collider>();

                        foreach (var comp in comps)
                        {
                            if (comp.GetType() != keepType)
                            {
                                Object.Destroy(comp);
                            }
                        }

                        if (eif.GetComponent(keepType) == null)
                        {
                            eif.gameObject.AddComponent(keepType);
                        }
                    }
                }

                rb.AddForce(launchForce + randomForce * Random.insideUnitSphere);
            }

            else if(isCharacter && stats.combatUI._charactersInCombat.TryGetValue(id, out var ch) && ch.FieldID >= 0)
            {
                var cif = stats.combatUI._characterZone._characters[ch.FieldID].FieldEntity;
                var rb = cif.GetOrAddComponent<Rigidbody>();

                rb.AddForce(launchForce + randomForce * Random.insideUnitSphere);
            }

            yield break;
        }
    }

    public enum ColliderType
    {
        None,
        Cube,
        Sphere,
        Capsule
    }
}
