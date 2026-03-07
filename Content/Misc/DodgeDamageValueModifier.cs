using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class DodgeDamageValueModifier(IUnit target, DamageReceivedValueChangeException ex, IUnit sender, string tempDisableSV) : IntValueModifier(-1)
    {
        public override int Modify(int value)
        {
            if (target == null)
                return value;

            if (!target.TryMoveLeftOrRight())
                return value;

            var firstDamagedSlot = ex.affectedStartSlot;
            var lastDamagedSlot = ex.affectedEndSlot;

            var includesSelf = false;
            var affectedOtherUnits = new List<IUnit>();

            var stats = CombatManager.Instance._stats;
            for(var i = firstDamagedSlot; i <= lastDamagedSlot; i++)
            {
                if(!stats.combatSlots.TryGetSlot(i, target.IsUnitCharacter, out var slot) || !slot.HasUnit)
                    continue;

                var unitHere = slot.Unit;
                if (unitHere == target)
                {
                    includesSelf = true;
                }
                else
                {
                    if (affectedOtherUnits.Contains(unitHere))
                        continue;

                    if (sender != null && !string.IsNullOrEmpty(tempDisableSV))
                        sender.SimpleSetStoredValue(tempDisableSV, 1);
                    unitHere.Damage(value, ex.possibleSourceUnit, ex.deathTypeID, -1, true, true, ex.ignoreShield, ex.damageTypeID);
                    if (sender != null && !string.IsNullOrEmpty(tempDisableSV))
                        sender.SimpleSetStoredValue(tempDisableSV, 0);

                    affectedOtherUnits.Add(unitHere);
                }
            }

            if (includesSelf)
                return value;

            ex.ShouldIgnoreUI = true;
            return 0;
        }
    }
}
