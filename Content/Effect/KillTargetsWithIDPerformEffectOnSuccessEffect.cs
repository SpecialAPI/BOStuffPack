using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class KillTargetsWithIDPerformEffectOnSuccessEffect : EffectSO
    {
        public List<string> entityIDs = [];
        public List<EffectInfo> effects = [];
        public bool startResultIsKilledRank = false;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if(entityIDs == null || entityIDs.Count == 0)
                return false;
            
            foreach(var t in targets)
            {
                if(!t.HasUnit)
                    continue;

                var u = t.Unit;
                if(!entityIDs.Contains(u.EntityID))
                    continue;

                if(!u.DirectDeath(caster, false, out _))
                    continue;

                var startRes = 0;
                if (startResultIsKilledRank && u is CharacterCombat cc)
                    startRes = cc.ClampedRank;

                exitAmount++;
                if (effects != null)
                    CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction([.. effects], caster, startRes));
            }

            return exitAmount > 0;
        }
    }
}
