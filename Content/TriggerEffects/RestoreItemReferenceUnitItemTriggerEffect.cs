using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class RestoreItemReferenceUnitItemTriggerEffect : TriggerEffect
    {
        public List<string> IgnoredItemTags = [];
        public bool consumeSelfIfSuccessful;
        public bool ignoreIfTargetIsSender;
        public bool doesRestoredPopup = true;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (args is not ItemReference itemRef || itemRef.itemHolder is not IUnit target || target is not IWearableEffector targetEffector || targetEffector.HeldItem == null || !targetEffector.IsWearableConsumed)
                return;

            if (ignoreIfTargetIsSender && target == sender)
                return;

            if(IgnoredItemTags != null && IgnoredItemTags.Count > 0 && itemRef.item != null)
            {
                foreach(var tag in IgnoredItemTags)
                {
                    if (itemRef.item.IsItemType(tag))
                        return;
                }
            }

            if (consumeSelfIfSuccessful && sender is IWearableEffector senderEffector)
                senderEffector.ConsumeWearable();
            if(ManuallyHandlePopup && extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, consumeSelfIfSuccessful, out var action2))
                CombatManager.Instance.AddUIAction(action2);

            target.RestoreItem(doesRestoredPopup);
        }

        public override bool ManuallyHandlePopup => true;
    }
}
