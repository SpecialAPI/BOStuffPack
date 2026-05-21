using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.TriggerEffects
{
    public class ConnectOrDisconnectStoredItemsTriggerEffect : TriggerEffect
    {
        public string itemsStoredValue;
        public bool disconnect;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (sender is not IWearableEffector effector)
                return;

            sender.TryGetStoredData(itemsStoredValue, out var hold);

            if (hold.m_ObjectData is not List<BaseWearableSO> items)
                return;

            foreach(var i in items)
            {
                if(i == null)
                    continue;

                if (disconnect)
                    i.OnTriggerDettached(effector);
                else
                    i.OnTriggerAttached(effector);
            }
        }
    }
}
