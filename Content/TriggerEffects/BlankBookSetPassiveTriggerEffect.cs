using BOStuffPack.CustomTrigger.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class BlankBookSetPassiveTriggerEffect : TriggerEffect
    {
        public string storedValue;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (args is not OnPassivePopupReference popupRef)
                return;

            if (sender is not IPassiveEffector effector)
                return;

            var passive = effector.PassiveAbilities.FirstOrDefault(x => (x.passiveIcon == popupRef.passiveIcon) && (x.GetPassiveLocData().text == popupRef.localizedPassiveName));
            if (passive == null)
                passive = effector.PassiveAbilities.FirstOrDefault(x => x.passiveIcon == popupRef.passiveIcon);
            if (passive == null)
                passive = effector.PassiveAbilities.FirstOrDefault(x => x.GetPassiveLocData().text == popupRef.localizedPassiveName);

            if (passive == null)
                return;

            effector.TryGetStoredData(storedValue, out var svHolder);
            svHolder.m_ObjectData = passive;
        }
    }
}
