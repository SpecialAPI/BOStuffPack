using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Conditions.Effector
{
    public class UnitValuePlacementCheckEffectorCondition : EffectorConditionSO
    {
        public int unitValueIndex;
        public UnitPlacement neededPlacement;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(effector is not IUnit sender)
                return false;

            if(!ValueReferenceTools.TryGetUnitHolder(args, out var unitHolder) || unitHolder[unitValueIndex] is not IUnit u)
                return false;

            if(!CombatManager.Instance._stats.TryGetUnitOnField(u.ID, u.IsUnitCharacter, out _))
                return false;

            var checkedPosition = neededPlacement switch
            {
                UnitPlacement.Leftmost => u.IsUnitCharacter ? CombatManager.Instance._stats.CharactersOnField.Values.Min(x => x.SlotID) : CombatManager.Instance._stats.EnemiesOnField.Values.Min(x => x.SlotID),
                UnitPlacement.Rightmost => u.IsUnitCharacter ? CombatManager.Instance._stats.CharactersOnField.Values.Max(x => x.LastSlotId()) : CombatManager.Instance._stats.EnemiesOnField.Values.Max(x => x.LastSlotId()),

                _ => -1
            };

            if(!CombatManager.Instance._stats.combatSlots.TryGetSlot(checkedPosition, u.IsUnitCharacter, out var slot))
                return false;

            return slot.Unit == u;
        }

        public enum UnitPlacement
        {
            Leftmost,
            Rightmost,
        }
    }
}
