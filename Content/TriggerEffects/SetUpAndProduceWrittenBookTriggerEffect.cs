using BOStuffPack.Content.Items;
using BOStuffPack.Content.Misc;
using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class SetUpAndProduceWrittenBookTriggerEffect : TriggerEffect
    {
        public string abilityStoredValue;
        public string abilityDataKey;
        public string passiveStoredValue;
        public string passiveDataKey;
        public string itemID;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            var stats = CombatManager.Instance._stats;
            stats.AddExtraLootAddition(itemID);

            sender.TryGetStoredData(abilityStoredValue, out var abHolder);
            if (abHolder.m_ObjectData is CombatAbility cAbility && cAbility.ability != null)
                stats.ModdedPostCombatResults.Add(new StringDataSetterPostCombatResult(abilityDataKey, WrittenBook.SerializeAbility(cAbility)));
            else
                stats.ModdedPostCombatResults.Add(new StringDataSetterPostCombatResult(abilityDataKey, string.Empty));

            sender.TryGetStoredData(passiveStoredValue, out var paHolder);
            if (paHolder.m_ObjectData is BasePassiveAbilitySO pa)
                stats.ModdedPostCombatResults.Add(new StringDataSetterPostCombatResult(passiveDataKey, WrittenBook.SerializePassive(pa)));
            else
                stats.ModdedPostCombatResults.Add(new StringDataSetterPostCombatResult(passiveDataKey, string.Empty));
        }
    }
}
