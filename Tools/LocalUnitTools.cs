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
    }
}
