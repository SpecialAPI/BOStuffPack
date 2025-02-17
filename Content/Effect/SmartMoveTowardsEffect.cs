using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class SmartMoveTowardsEffect : EffectSO
    {
        public bool right;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var characters = new List<IUnit>();
            var enemies = new List<IUnit>();

            var ignoreGuy = targets.Length > 0 ? targets[0].Unit : null;

            foreach(var t in targets)
            {
                if (!t.HasUnit || t.Unit == ignoreGuy)
                    continue;

                var unit = t.Unit;

                if (unit.IsUnitCharacter && !characters.Contains(unit))
                    characters.Add(unit);

                else if (!unit.IsUnitCharacter && !enemies.Contains(unit))
                    enemies.Add(unit);
            }

            var move = right ? 1 : -1;

            foreach (var ch in characters)
            {
                var newSlot = ch.SlotID + move;

                if (newSlot < 0 || newSlot >= stats.combatSlots.CharacterSlots.Length)
                    continue;

                if (stats.combatSlots.CharacterSlots[newSlot] != null && stats.combatSlots.CharacterSlots[newSlot].HasUnit && stats.combatSlots.CharacterSlots[newSlot].Unit == ignoreGuy)
                    continue;

                if (stats.combatSlots.SwapCharacters(ch.SlotID, newSlot, true))
                    exitAmount++;
            }

            foreach (var en in enemies)
            {
                var enemyMove = right ? en.Size : -1;
                var newSlot = en.SlotID + enemyMove;

                if (!stats.combatSlots.CanEnemiesSwap(en.SlotID, newSlot, out var firstSlotSwap, out var secondSlotSwap))
                    continue;

                if(stats.combatSlots.EnemySlots[newSlot] != null && stats.combatSlots.EnemySlots[newSlot].HasUnit && stats.combatSlots.EnemySlots[newSlot].Unit == ignoreGuy)
                    continue;

                if (stats.combatSlots.SwapEnemies(en.SlotID, firstSlotSwap, newSlot, secondSlotSwap, true))
                    exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
