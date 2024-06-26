using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class IUnitExtensions
    {
        public static List<CombatAbility> Abilities(this IUnit unit)
        {
            if (unit is CharacterCombat cc)
                return cc.CombatAbilities;

            else if (unit is EnemyCombat ec)
                return ec.Abilities;

            return [];
        }

        public static List<string> FieldEffects(this IUnit unit)
        {
            if (unit is CharacterCombat cc)
                return cc.CurrentFieldEffects;

            else if (unit is EnemyCombat ec)
                return ec.CurrentFieldEffects;

            return [];
        }

        public static bool ContainsField(this IUnit unit, string field)
        {
            if (unit is CharacterCombat cc)
                return cc.ContainsFieldEffect(field);

            else if (unit is EnemyCombat ec)
                return ec.ContainsFieldEffect(field);

            return false;
        }
    }
}
