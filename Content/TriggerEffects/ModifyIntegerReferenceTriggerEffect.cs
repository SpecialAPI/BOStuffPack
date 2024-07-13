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

        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger)
        {
            if(!TryReadIntegerReference(args, out var intRef))
                return;

            var val = UseStoredValue ? sender.SimpleGetStoredValue(StoredValue) : Value;

            intRef.value = Operation switch
            {
                IntOperation.Add => intRef.value + val,
                IntOperation.Subtract => intRef.value - val,

                IntOperation.Multiply => intRef.value * val,
                IntOperation.Divide => intRef.value / val,

                _ => val
            };
        }
    }
}
