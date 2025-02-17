using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class StressifyMyOrchestraEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (stats.playerCombat == null || stats.playerCombat is not PlayerCombat plr || stats.timeline == null || stats.playerCombat is not PlayerCombat || stats.timeline is not Timeline_TurnBase)
                return false;

            var oldTL = stats.timeline;

            stats.playerCombat = new PlayerCombatRealTime(plr._player);
            stats.timeline = new Timeline_RealTime(stats.playerCombat)
            {
                ConfusionSources = oldTL.ConfusionSources,
            };

            foreach(var enm in stats.EnemiesOnField.Values)
                stats.timeline.AddEnemyToTimeline(enm);

            var info = stats.timeline.RoundTurnUIInfo;

            stats.combatUI._timeline_RT.ActivateTimeline();
            stats.combatUI._timeline.gameObject.SetActive(false);

            stats.combatUI._nextTurnLayout.DisableLayout();

            stats.combatUI._TimelineHandler = new TimelineLayoutHandler_RealTime(stats.combatUI._timeline_RT, stats.combatUI._enemiesInCombat, plr._player);

            if (info != null)
            {
                CombatManager.Instance.AddUIAction(new PopulateTimelineUIAction(info));
                CombatManager.Instance.AddUIAction(new UpdateTimelinePointerUIAction(stats.timeline.CurrentTurn));
            }

            return true;
        }
    }
}
