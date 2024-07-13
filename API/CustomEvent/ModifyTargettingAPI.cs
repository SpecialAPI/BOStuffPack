using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.CustomEvent
{
    public static class ModifyTargettingAPI
    {
        public static int? targetOffset;
        public static bool? targetSwapped;

        public static void SetTemporaryTargettingModifications(int offset, bool swapped)
        {
            targetOffset = offset;
            targetSwapped = swapped;
        }

        public static void ResetTemporaryTargettingModifications()
        {
            targetOffset = null;
            targetSwapped = null;
        }

        public static TargetSlotInfo[] GetModifiedTargets(this BaseCombatTargettingSO targetting, SlotsCombat slots, int casterSlotId, bool casterIsCharacter, int targetOffset = 0, bool targetsSwapped = false)
        {
            SetTemporaryTargettingModifications(targetOffset, targetsSwapped);

            var ret = targetting.GetTargets(slots, casterSlotId, casterIsCharacter);

            ResetTemporaryTargettingModifications();

            return ret;
        }
    }
}
