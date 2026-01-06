using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class ApplySavedStoredValuesToSenderTriggerEffect(string dataKey) : TriggerEffect
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

            var savedValues = dat.GetStringData(dataKey);
            if (string.IsNullOrEmpty(savedValues))
                return;

            var svData = savedValues.Split(['|'], StringSplitOptions.RemoveEmptyEntries);
            var svToApply = new List<(string svID, int amount)>();
            foreach (var sv in svData)
            {
                var commaIdx = sv.IndexOf(',');
                if (commaIdx < 0)
                    continue;

                var svID = sv.Substring(0, commaIdx);
                var amtString = sv.Substring(commaIdx + 1);

                if (!LoadedDBsHandler.MiscDB.m_UnitStoreDataPool.ContainsKey(svID) || !int.TryParse(amtString, out var amt))
                    continue;

                svToApply.Add((svID, amt));
            }

            if (svToApply.Count <= 0)
                return;

            if (sender is not IUnit unit)
                return;

            if (extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                CombatManager.Instance.AddUIAction(action);

            foreach (var (svID, amt) in svToApply)
                unit.SimpleSetStoredValue(svID, amt);
        }

        public override bool ManuallyHandlePopup => true;
    }
}
