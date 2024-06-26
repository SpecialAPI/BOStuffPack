using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CommonReferences
{
    public class Targets
    {
        public static BaseCombatTargettingSO Opposing = Targeting.Slot_Front;
        public static BaseCombatTargettingSO Self = Targeting.Slot_SelfAll;
        public static BaseCombatTargettingSO SelfOneSlot = Targeting.Slot_SelfSlot;

        public static BaseCombatTargettingSO Enemies = Targeting.Unit_AllOpponents;
        public static BaseCombatTargettingSO EnemySlots = Targeting.Unit_AllOpponentSlots;

        public static BaseCombatTargettingSO Allies = Targeting.Unit_AllAllies;
        public static BaseCombatTargettingSO AllySlots = Targeting.Unit_AllAllySlots;

        public static BaseCombatTargettingSO AllUnits = Targeting.AllUnits;

        public static BaseCombatTargettingSO EnemySide = Targeting.Slot_OpponentAllSlots;
        public static BaseCombatTargettingSO AllySide = Targeting.Slot_AllyAllSlots;

        public static BaseCombatTargettingSO Relative(bool allies, params int[] offsets)
        {
            return CreateScriptable<Targetting_BySlot_Index>(x =>
            {
                x.getAllies = allies;
                x.slotPointerDirections = offsets;

                x.allSelfSlots = true;
            });
        }

        public static BaseCombatTargettingSO Absolute(bool allies, params int[] slots)
        {
            return CreateScriptable<GenericTargetting_BySlot_Index>(x =>
            {
                x.getAllies = allies;
                x.slotPointerDirections = slots;
            });
        }

        public static BaseCombatTargettingSO Side(bool allies, bool allSlots = false, bool ignoreSelf = false)
        {
            return CreateScriptable<Targetting_ByUnit_Side>(x =>
            {
                x.getAllies = allies;

                x.getAllUnitSlots = allSlots;
                x.ignoreCastSlot = ignoreSelf;
            });
        }

        public static BaseCombatTargettingSO CustomRelative(List<int> offsets, List<int> selfOffsets)
        {
            return CreateScriptable<CustomOpponentTargetting_BySlot_Index>(x =>
            {
                x._slotPointerDirections = offsets.ToArray();
                x._frontOffsets = selfOffsets.ToArray();
            });
        }
    }
}
