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

        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger, TriggerEffectExtraInfo extraInfo)
        {
            if (!ValueReferenceTools.TryGetIntHolder(args, out var intRef))
                return;

            var val = UseStoredValue ? sender.SimpleGetStoredValue(StoredValue) : Value;
            intRef.Value = DoOperation(intRef.Value, val, Operation);
        }
    }
}
