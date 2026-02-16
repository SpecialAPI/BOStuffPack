using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class RerollTargetTimelineAbilityIntoSpecificAbilityEffect : EffectSO
    {
        public string abId;
        public bool rerollAll;
        public bool ignoreCantReroll;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                if(t.Unit is not EnemyCombat ec)
                    continue;

                exitAmount += stats.timeline.CustomRerollRandomEnemyTurns(ec, entryVariable, RerolledAbilitySelector, rerollAll, ignoreCantReroll);
            }

            return exitAmount > 0;
        }

        public int RerolledAbilitySelector(ITurn turn, int currentAbility)
        {
            if (turn is not IUnit unit)
                return -1;

            var abs = unit.Abilities();
            for(var i = 0; i < abs.Count; i++)
            {
                var ab = abs[i];
                if(ab == null || ab.ability == null)
                    continue;

                if (ab.ability.name != abId)
                    continue;

                return i;
            }

            return -1;
        }
    }
}
