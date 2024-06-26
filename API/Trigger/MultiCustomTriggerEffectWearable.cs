using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.Trigger
{
    public class MultiCustomTriggerEffectWearable : BaseWearableSO
    {
        public List<EffectsAndTrigger> triggerEffects;
        private readonly Dictionary<int, Action<object, object>> effectMethods = [];

        public override bool IsItemImmediate => false;
        public override bool DoesItemTrigger => false;

        public override void CustomOnTriggerAttached(IWearableEffector caller)
        {
            if (triggerEffects == null)
                return;

            for (var i = 0; i < triggerEffects.Count; i++)
            {
                var te = triggerEffects[i];
                var strings = te.TriggerStrings();

                foreach (var str in strings)
                {
                    CombatManager.Instance.AddObserver(GetEffectMethod(i), str, caller);
                }
            }
        }

        public override void CustomOnTriggerDettached(IWearableEffector caller)
        {
            if (triggerEffects == null)
                return;

            for (var i = 0; i < triggerEffects.Count; i++)
            {
                var te = triggerEffects[i];
                var strings = te.TriggerStrings();

                foreach (var str in strings)
                {
                    CombatManager.Instance.RemoveObserver(GetEffectMethod(i), str, caller);
                }
            }
        }

        public Action<object, object> GetEffectMethod(int i)
        {
            if (effectMethods.TryGetValue(i, out var existing))
                return existing;

            return effectMethods[i] = (sender, args) => TryPerformItemEffect(sender, args, i);
        }

        public void TryPerformItemEffect(object sender, object args, int index)
        {
            if (index >= triggerEffects.Count || sender is not IWearableEffector effector || !effector.CanWearableTrigger)
                return;

            var te = triggerEffects[index];

            if (te == null)
                return;

            if (te.conditions != null)
            {
                foreach (var cond in te.conditions)
                {
                    if (!cond.MeetCondition(effector, args))
                        return;
                }
            }

            if (te.immediate)
                CombatManager.Instance.ProcessImmediateAction(new PerformItemCustomImmediateAction(this, sender, args, index));

            else
                CombatManager.Instance.AddSubAction(new PerformItemCustomAction(this, sender, args, index));
        }

        public override void FinalizeCustomTriggerItem(object sender, object args, int idx)
        {
            if (idx >= triggerEffects.Count || sender is not IWearableEffector effector || sender is not IUnit caster || effector.IsWearableConsumed)
                return;

            var te = triggerEffects[idx];

            if (te == null)
                return;

            var consumed = te.getsConsumed;

            if (consumed)
                effector.ConsumeWearable();

            if (te.doesPopup)
                CombatManager.Instance.AddUIAction(new ShowItemInformationUIAction(effector.ID, GetItemLocData().text, consumed, wearableImage));

            te.effect?.DoEffect(caster, args, te);
        }
    }
}
