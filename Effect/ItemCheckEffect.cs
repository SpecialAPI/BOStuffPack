using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class ItemCheckEffect : EffectSO
    {
        public string requiredID;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                if(!t.Unit.HasUsableItem || !string.IsNullOrEmpty(requiredID) && t.Unit.HeldItem.name != requiredID)
                    continue;

                exitAmount++;
            }

            return exitAmount > 0;
        }

        public static EffectSO Create(string requiredID = "")
        {
            var e = CreateScriptable<ItemCheckEffect>();
            e.requiredID = requiredID;

            return e;
        }
    }
}
