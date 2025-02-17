using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class ModifyIntegerReferenceTriggerEffect : TriggerEffect
    {
        public IntOperation Operation;
        public int Value;

        public bool UseStoredValue;
        public string StoredValue;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger, object activator)
        {
            if (!args.TryGetIntReference(out var intRef))
                return;

            var val = UseStoredValue ? sender.SimpleGetStoredValue(StoredValue) : Value;
            intRef.value = DoOperation(intRef.value, val, Operation);
        }
    }
}
