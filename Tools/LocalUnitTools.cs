using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class LocalUnitTools
    {
        public static bool TryRestoreItem(this IUnit u, bool showPopup = true)
        {
            if(u is not CharacterCombat cc)
                return false;

            if(cc.HeldItem == null)
                return false;

            if(!cc.IsWearableConsumed)
                return false;

            u.RestoreItem(showPopup);
            return true;
        }

        public static void RestoreItem(this IUnit u, bool showPopup = false)
        {
            if (u is not CharacterCombat cc)
                return;

            if (cc.HeldItem == null)
                return;

            if (!cc.IsWearableConsumed)
                return;

            if (showPopup)
                CombatManager.Instance.AddUIAction(new ShowItemRestoredUIAction(cc.ID, cc.HeldItem.GetItemLocData().text, cc.HeldItem.wearableImage));
            cc.IsWearableConsumed = false;
            cc.HeldItem.OnTriggerAttached(cc);
            CombatManager.Instance.AddUIAction(new CharacterItemChangeUIAction(cc.ID, cc.HeldItem, cc.IsWearableConsumed));
        }

        public static bool TryMoveLeftOrRight(this IUnit u)
        {
            var stats = CombatManager.Instance._stats;

            if (u.IsUnitCharacter)
            {
                var direction = Random.Range(0, 2) * 2 - 1;
                if (u.SlotID + direction >= 0 && u.SlotID + direction < stats.combatSlots.CharacterSlots.Length)
                    return stats.combatSlots.SwapCharacters(u.SlotID, u.SlotID + direction, isMandatory: true);

                direction *= -1;
                if (u.SlotID + direction >= 0 && u.SlotID + direction < stats.combatSlots.CharacterSlots.Length)
                    return stats.combatSlots.SwapCharacters(u.SlotID, u.SlotID + direction, isMandatory: true);

                return false;
            }
            else
            {
                var direction = Random.Range(0, 2) * (u.Size + 1) - 1;
                if (stats.combatSlots.CanEnemiesSwap(u.SlotID, u.SlotID + direction, out var firstSlotSwap, out var secondSlotSwap))
                    return stats.combatSlots.SwapEnemies(u.SlotID, firstSlotSwap, u.SlotID + direction, secondSlotSwap, isMandatory: true);

                direction = (direction < 0) ? u.Size : (-1);
                if (stats.combatSlots.CanEnemiesSwap(u.SlotID, u.SlotID + direction, out firstSlotSwap, out secondSlotSwap))
                    return stats.combatSlots.SwapEnemies(u.SlotID, firstSlotSwap, u.SlotID + direction, secondSlotSwap, isMandatory: true);

                return false;
            }
        }

        public static bool GetCanSwapNoTrigger(this IUnit u)
        {
            if(u is CharacterCombat cc)
                return cc.CanSwapNoTrigger;

            // replace with reverese patch in the future?
            else if(u is EnemyCombat ec)
            {
                var boolRef = new BooleanWithTriggerReference(ec._canSwap, false);
                CombatManager.Instance.PostNotification(TriggerCalls.CanSwap.ToString(), ec, boolRef);

                return boolRef.value;
            }

            return false;
        }

        public static bool GetCanUseAbilitiesNoTrigger(this IUnit u)
        {
            if (u is CharacterCombat cc)
                return cc.CanUseAbilitiesNoTrigger;

            return false;
        }

        public static bool GetCanUseAbilities(this IUnit u)
        {
            if (u is CharacterCombat cc)
                return cc.CanUseAbilities;

            return false;
        }
    }
}
