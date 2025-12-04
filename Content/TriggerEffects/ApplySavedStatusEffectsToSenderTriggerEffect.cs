using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class ApplySavedStatusEffectsToSenderTriggerEffect(string dataKey) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (string.IsNullOrEmpty(dataKey))
                return;

            var infoHolder = LoadedDBsHandler.InfoHolder;
            if (infoHolder == null)
                return;

            if (infoHolder.Run == null || infoHolder.Run.InGameData is not IInGameRunData dat)
                return;

            var savedStatuses = dat.GetStringData(dataKey);
            if (string.IsNullOrEmpty(savedStatuses))
                return;

            var statusData = savedStatuses.Split(['|'], StringSplitOptions.RemoveEmptyEntries);
            var statusesToApply = new List<(StatusEffect_SO seSO, int amount)>();
            foreach(var stData in statusData)
            {
                var commaIdx = stData.IndexOf(',');
                if(commaIdx < 0)
                    continue;

                var statusId = stData.Substring(0, commaIdx);
                var amtString = stData.Substring(commaIdx + 1);

                if(!LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(statusId, out var seSO) || !int.TryParse(amtString, out var amt))
                    continue;

                statusesToApply.Add((seSO, amt));
            }

            if(statusesToApply.Count <= 0)
                return;

            if (sender is not IStatusEffector effector)
                return;

            if(extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                CombatManager.Instance.AddUIAction(action);

            foreach(var (seSO, amt) in statusesToApply)
                effector.ApplyStatusEffect(seSO, amt);
        }

        public override bool ManuallyHandlePopup => true;
    }
}
