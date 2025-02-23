using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class StressfulConnectTriggerEffect : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger, TriggerEffectExtraInfo extraInfo)
        {
            if (!extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                action = null;

            CombatManager.Instance.AddSubAction(new StressfulConnectedAction(sender, action));
        }
    }

    public class StressfulConnectedAction(IUnit u, CombatAction popupUIAction) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            if (u == null || !stats.TryGetUnitOnField(u.ID, u.IsUnitCharacter, out var onField))
                yield break;

            if (stats.playerCombat == null || stats.playerCombat is not PlayerCombat plr || stats.timeline == null || stats.playerCombat is not PlayerCombat || stats.timeline is not Timeline_TurnBase)
                yield break;

            var oldTL = stats.timeline;

            stats.playerCombat = new PlayerCombatRealTime(plr._player);
            stats.timeline = new Timeline_RealTime(stats.playerCombat)
            {
                ConfusionSources = oldTL.ConfusionSources,
            };

            foreach (var enm in stats.EnemiesOnField.Values)
                stats.timeline.AddEnemyToTimeline(enm);

            var info = stats.timeline.RoundTurnUIInfo;

            stats.combatUI._timeline_RT.ActivateTimeline();
            stats.combatUI._timeline.gameObject.SetActive(false);

            stats.combatUI._nextTurnLayout.DisableLayout();

            stats.combatUI._TimelineHandler = new TimelineLayoutHandler_RealTime(stats.combatUI._timeline_RT, stats.combatUI._enemiesInCombat, plr._player);

            if (popupUIAction != null)
                yield return popupUIAction.Execute(stats);

            if (info != null)
            {
                CombatManager.Instance.AddUIAction(new PopulateTimelineUIAction(info));
                CombatManager.Instance.AddUIAction(new UpdateTimelinePointerUIAction(stats.timeline.CurrentTurn));
            }
        }
    }
}
