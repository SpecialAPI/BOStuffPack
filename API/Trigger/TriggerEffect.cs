using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.Trigger
{
    public abstract class TriggerEffect
    {
        public abstract void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger);
    }

    public class TriggeredEffect
    {
        public TriggerEffect effect;
        public List<EffectorConditionSO> conditions;
        public bool immediate;
        public bool doesPopup = true;
        public bool getsConsumed;
    }

    public class EffectsAndTrigger : TriggeredEffect
    {
        public string trigger;

        public virtual IEnumerable<string> TriggerStrings()
        {
            if (!string.IsNullOrEmpty(trigger))
                yield return trigger;
        }
    }

    public class EffectsAndTriggers : EffectsAndTrigger
    {
        public List<string> triggers = [];

        public override IEnumerable<string> TriggerStrings()
        {
            foreach (var b in base.TriggerStrings())
                yield return b;

            foreach (var t in triggers)
            {
                if (!string.IsNullOrEmpty(t))
                    yield return t;
            }
        }
    }
}
