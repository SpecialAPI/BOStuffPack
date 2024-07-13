using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.Trigger
{
    public class MultiCustomTriggerEffectPassive : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate => false;
        public override bool DoesPassiveTrigger => true;

        public List<EffectsAndTrigger> triggerEffects;
        private readonly Dictionary<int, Action<object, object>> effectMethods = [];

        public List<TriggeredEffect> connectionEffects;
        public List<TriggeredEffect> disconnectionEffects;

        public MultiCustomTriggerEffectPassive()
        {
            _triggerOn = [];
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            if (connectionEffects == null)
                return;

            for(var i = 0; i < connectionEffects.Count; i++)
            {
                TryPerformItemEffect(unit, null, i);
            }
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            if (disconnectionEffects == null)
                return;

            for (var i = 0; i < disconnectionEffects.Count; i++)
            {
                TryPerformItemEffect(unit, null, i + (connectionEffects?.Count ?? 0));
            }
        }

        public override void CustomOnTriggerAttached(IPassiveEffector caller)
        {
            if (triggerEffects == null)
                return;

            for (var i = 0; i < triggerEffects.Count; i++)
            {
                var te = triggerEffects[i];
                var strings = te.TriggerStrings();

                foreach (var str in strings)
                {
                    CombatManager.Instance.AddObserver(GetEffectMethod(i + (connectionEffects?.Count ?? 0) + (disconnectionEffects?.Count ?? 0)), str, caller);
                }
            }
        }

        public override void CustomOnTriggerDettached(IPassiveEffector caller)
        {
            if (triggerEffects == null)
                return;

            for (var i = 0; i < triggerEffects.Count; i++)
            {
                var te = triggerEffects[i];
                var strings = te.TriggerStrings();

                foreach (var str in strings)
                {
                    CombatManager.Instance.RemoveObserver(GetEffectMethod(i + (connectionEffects?.Count ?? 0) + (disconnectionEffects?.Count ?? 0)), str, caller);
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
            if (index >= ((triggerEffects?.Count ?? 0) + (connectionEffects?.Count ?? 0) + (disconnectionEffects?.Count ?? 0)) || sender is not IPassiveEffector effector || !effector.CanPassiveTrigger(m_PassiveID))
                return;

            var te = GetEffectAtIndex(index, out var connect, out var disconnect);

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

            if (te.immediate || connect || disconnect)
                CombatManager.Instance.ProcessImmediateAction(new PerformPassiveCustomImmediateAction(this, sender, args, index), connect);

            else
                CombatManager.Instance.AddSubAction(new PerformPassiveCustomAction(this, sender, args, index));
        }

        public override void FinalizeCustomTriggerPassive(object sender, object args, int idx)
        {
            if (idx >= ((triggerEffects?.Count ?? 0) + (connectionEffects?.Count ?? 0) + (disconnectionEffects?.Count ?? 0)) || sender is not IPassiveEffector effector || sender is not IUnit caster)
                return;

            var te = GetEffectAtIndex(idx, out _, out _);

            if (te == null)
                return;

            if (te.doesPopup)
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(effector.ID, effector.IsUnitCharacter, GetPassiveLocData().text, passiveIcon));

            te.effect?.DoEffect(caster, args, te);
        }

        public TriggeredEffect GetEffectAtIndex(int idx, out bool connection, out bool disconnection)
        {
            connection = true;
            disconnection = false;

            if(connectionEffects != null && idx < connectionEffects.Count)
                return connectionEffects[idx];

            connection = false;
            disconnection = true;

            idx -= connectionEffects?.Count ?? 0;

            if (disconnectionEffects != null && idx < disconnectionEffects.Count)
                return disconnectionEffects[idx];

            disconnection = false;

            idx -= disconnectionEffects?.Count ?? 0;

            if(triggerEffects != null && idx < triggerEffects.Count)
                return triggerEffects[idx];

            return null;
        }

        public override void TriggerPassive(object sender, object args)
        {
        }
    }
}
