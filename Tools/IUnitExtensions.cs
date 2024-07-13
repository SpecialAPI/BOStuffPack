using System;
using System.Collections;
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

        public static bool UsesAllAbilities(this CharacterCombat cc)
        {
            return cc.CharacterWearableModifiers.UsesAllAbilitiesBool ? cc.CharacterWearableModifiers.UsesAllAbilitiesModifier : cc.Character.usesAllAbilities;
        }

        public static List<CharacterAbility> UsedRankedAbilities(this CharacterCombat cc, CharacterRankedData overrideRank = null)
        {
            var ret = new List<CharacterAbility>();

            if(overrideRank == null)
            {
                if (!cc.Character.HasRankedData)
                    return ret;

                overrideRank = cc.Character.rankedData[cc.ClampedRank];
            }

            if (overrideRank == null)
                return ret;

            if (cc.UsesAllAbilities())
            {
                foreach(var ab in overrideRank.rankAbilities)
                    ret.Add(ab);
            }

            else
            {
                foreach (var idx in cc.UsedAbilities)
                {
                    if (idx >= 0 && idx < overrideRank.rankAbilities.Length)
                        ret.Add(overrideRank.rankAbilities[idx]);
                }
            }

            return ret;
        }

        public static int DistanceBetween(this IUnit a, IUnit b)
        {
            if (a == null || b == null)
                return 0;

            var aFirst = a.SlotID;
            var aLast = a.LastSlotId();

            var bFirst = b.SlotID;
            var bLast = b.LastSlotId();

            if (bLast < aFirst && bFirst <= aLast)
                return Mathf.Abs(aFirst - bLast);

            else if(bFirst > aLast && bLast >= aFirst)
                return Mathf.Abs(bFirst - aLast);

            return 0;
        }

        public static bool IsOpposing(this IUnit a, IUnit b)
        {
            if (a == null || b == null || a.IsUnitCharacter == b.IsUnitCharacter)
                return false;

            var aFirst = a.SlotID;
            var aLast = a.LastSlotId();

            var bFirst = b.SlotID;
            var bLast = b.LastSlotId();

            return aFirst <= bLast && bFirst <= aLast;
        }

        public static bool IsRightOf(this IUnit a, IUnit b)
        {
            if (a == null || b == null)
                return false;

            var aFirst = a.SlotID;
            var bLast = b.LastSlotId();

            return bLast < aFirst;
        }

        public static bool IsLeftOf(this IUnit a, IUnit b)
        {
            if (a == null || b == null)
                return false;

            var bFirst = b.SlotID;
            var aLast = a.LastSlotId();

            return bFirst > aLast;
        }

        public static int LastSlotId(this IUnit u)
        {
            return u.SlotID + u.Size - 1;
        }

        public static bool TryMoveUnit(this IUnit unit, bool toRight)
        {
            if (unit == null)
                return false;

            var move = toRight ? unit.IsUnitCharacter ? 1 : unit.Size : -1;
            var slots = CombatManager.Instance._stats.combatSlots;

            if (unit.IsUnitCharacter)
                return unit.SlotID + move >= 0 && unit.SlotID + move < slots.CharacterSlots.Length && slots.SwapCharacters(unit.SlotID, unit.SlotID + move, true);

            return slots.CanEnemiesSwap(unit.SlotID, unit.SlotID + move, out var firstSlotSwap, out var secondSlotSwap) && slots.SwapEnemies(unit.SlotID, firstSlotSwap, unit.SlotID + move, secondSlotSwap, true);
        }
    }
}
