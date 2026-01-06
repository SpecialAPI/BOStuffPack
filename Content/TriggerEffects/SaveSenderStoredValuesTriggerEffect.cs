using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class SaveSenderStoredValuesTriggerEffect(string dataKey) : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (string.IsNullOrEmpty(dataKey))
                return;

            IReadOnlyDictionary<string, UnitStoreDataHolder> storedValues;
            if (sender is CharacterCombat cc)
                storedValues = cc.ReadOnlyStoredValues;
            else if (sender is EnemyCombat ec)
                storedValues = ec.ReadOnlyStoredValues;
            else
                return;

            var svData = new List<string>();
            foreach(var sv in storedValues.Values)
            {
                if (sv.m_ObjectData != null) // ignore stored object values
                    continue;

                if (sv.m_MainData == 0) // no point since 0 is already the default
                    continue;

                if(sv._UnitData == null || !LoadedDBsHandler.MiscDB.m_UnitStoreDataPool.ContainsKey(sv._UnitData._UnitStoreDataID))
                    continue;

                svData.Add($"{sv._UnitData._UnitStoreDataID},{sv.m_MainData}");
            }

            CombatManager.Instance._stats.ModdedPostCombatResults.Add(new StringDataSetterPostCombatResult(dataKey, string.Join("|", svData)));
        }
    }
}
