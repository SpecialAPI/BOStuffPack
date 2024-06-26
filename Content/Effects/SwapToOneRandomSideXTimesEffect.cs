using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class SwapToOneRandomSideXTimesEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var chars = new List<IUnit>();
            var enemies = new List<IUnit>();

            foreach(var t in targets)
            {
                if (!t.HasUnit)
                    continue;

                var u = t.Unit;

                if (u.IsUnitCharacter && !chars.Contains(u))
                    chars.Add(u);

                else if (!u.IsUnitCharacter && !enemies.Contains(u))
                    enemies.Add(u);
            }

            foreach (var ch in chars)
            {
                var move = Random.Range(0, 2) * 2 - 1;

                if (ch.SlotID + move < 0 || ch.SlotID + move >= stats.combatSlots.CharacterSlots.Length)
                    move *= -1;

                for (int i = 0; i < entryVariable; i++)
                {
                    if (ch.SlotID + move >= 0 && ch.SlotID + move < stats.combatSlots.CharacterSlots.Length && stats.combatSlots.SwapCharacters(ch.SlotID, ch.SlotID + move, isMandatory: true))
                        exitAmount++;

                    else
                        break;
                }
            }

            foreach (var en in enemies)
            {
                var move = Random.Range(0, 2) * (en.Size + 1) - 1;

                if (!stats.combatSlots.CanEnemiesSwap(en.SlotID, en.SlotID + move, out var firstSlotSwap, out var secondSlotSwap))
                    move = (move < 0) ? en.Size : (-1);

                for (int i = 0; i < entryVariable; i++)
                {
                    if (stats.combatSlots.CanEnemiesSwap(en.SlotID, en.SlotID + move, out firstSlotSwap, out secondSlotSwap) && stats.combatSlots.SwapEnemies(en.SlotID, firstSlotSwap, en.SlotID + move, secondSlotSwap))
                        exitAmount++;

                    else
                        break;
                }
            }

            return exitAmount > 0;
        }
    }
}
