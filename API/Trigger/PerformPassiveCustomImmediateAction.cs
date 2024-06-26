using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.Trigger
{
    public class PerformPassiveCustomImmediateAction(BasePassiveAbilitySO passive, object sender, object args, int idx) : IImmediateAction
    {
        public BasePassiveAbilitySO passive = passive;
        public object sender = sender;
        public object args = args;
        public int idx = idx;

        public void Execute(CombatStats stats)
        {
            if (passive != null)
            {
                passive.FinalizeCustomTriggerPassive(sender, args, idx);
            }
        }
    }
}
