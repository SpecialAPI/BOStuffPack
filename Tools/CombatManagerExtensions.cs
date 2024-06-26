using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class CombatManagerExtensions
    {
        public static List<IUnit> Units(this CombatStats stats)
        {
            var list = new List<IUnit>();

            foreach (var kvp in stats.Characters)
                list.Add(kvp.Value);

            foreach(var kvp in stats.Enemies)
                list.Add(kvp.Value);

            return list;
        }

        public static List<IUnit> UnitsOnField(this CombatStats stats)
        {
            var list = new List<IUnit>();

            foreach (var kvp in stats.CharactersOnField)
                list.Add(kvp.Value);

            foreach (var kvp in stats.EnemiesOnField)
                list.Add(kvp.Value);

            return list;
        }

        public static bool TryGetUnit(this CombatStats stats, int id, bool character, out IUnit unit)
        {
            if(character && stats.Characters.TryGetValue(id, out var cc))
            {
                unit = cc;

                return true;
            }

            if(!character && stats.Enemies.TryGetValue(id, out var ec))
            {
                unit = ec;

                return true;
            }

            unit = null;
            return false;
        }

        public static bool TryGetUnitOnField(this CombatStats stats, int id, bool character, out IUnit unit)
        {
            if (character && stats.CharactersOnField.TryGetValue(id, out var cc))
            {
                unit = cc;

                return true;
            }

            if (!character && stats.EnemiesOnField.TryGetValue(id, out var ec))
            {
                unit = ec;

                return true;
            }

            unit = null;
            return false;
        }
    }
}
