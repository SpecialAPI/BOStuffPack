using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class SaveSenderStatusEffectsEffect(string dataKey) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if(string.IsNullOrEmpty(dataKey))
                return;

            if (sender is not IStatusEffector effector)
                return;

            var effectData = new List<string>();
            foreach(var status in effector.StatusEffects)
            {
                if(status == null || status.StatusContent <= 0)
                    continue;

                if (!LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(status.StatusID, out _))
                    continue;

                effectData.Add($"{status.StatusID},{status.StatusContent}");
            }

            CombatManager.Instance._stats.ModdedPostCombatResults.Add(new StringDataSetterPostCombatResult(dataKey, string.Join("|", effectData)));
        }
    }
}
