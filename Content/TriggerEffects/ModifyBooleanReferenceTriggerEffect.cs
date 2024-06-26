using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class ModifyBooleanReferenceTriggerEffect : TriggerEffect
    {
        public bool value;
        public BoolOperation operation;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger)
        {
            if(!TryReadBooleanReference(args, out var boolRef))
                return;

            boolRef.value = operation switch
            {
                BoolOperation.And => boolRef.value && value,
                BoolOperation.Or => boolRef.value || value,
                BoolOperation.Xor => boolRef.value ^ value,

                _ => value
            };
        }
    }
}
