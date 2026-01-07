using BOStuffPack.Content.Misc;
using BOStuffPack.Content.StaticModifiers;
using BOStuffPack.DynamicAppearances;
using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class WrittenBook
    {
        public static readonly string ExtraAbilityDataKey = Profile.GetID("WrittenBookAbility");
        public static readonly string ExtraPassiveDataKey = Profile.GetID("WrittenBookPassive");

        public static readonly Dictionary<string, CharacterAbility> deserializedAbilities = [];
        public static readonly Dictionary<string, BasePassiveAbilitySO> deserializedPassives = [];

        public static void Init()
        {
            var name = "Written Book";
            var flav = "\"WIP\"";
            var desc = "No effect.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("WrittenBook_ExtraW")
                .SetBasicInformation(name, flav, desc, "WrittenBook")
                .SetPrice(15)
                .AddWithoutItemPools();

            item.SetStaticModifiers(CreateScriptable<SavedExtraAbilityAndPassiveStaticModifier>(x =>
            {
                x.abilityDataKey = ExtraAbilityDataKey;
                x.passiveDataKey = ExtraPassiveDataKey;
            }));
            item.AttachDynamicAppearance(new WrittenBookDynamicAppearance(ExtraAbilityDataKey, ExtraPassiveDataKey));
        }

        public static bool DeserializeAbility(string str, out CharacterAbility ab)
        {
            if(deserializedAbilities.TryGetValue(str, out var info))
            {
                ab = info;
                return ab != null;
            }

            var data = str.Split(['|'], StringSplitOptions.RemoveEmptyEntries);
            if(data.Length < 1)
            {
                deserializedAbilities[str] = ab = null;
                return false;
            }

            var abId = data[0];
            var abSO = LoadCharacterAbility(abId);
            if(abSO == null)
                abSO = LoadEnemyAbility(abId);
            if(abSO == null)
                abSO = Resources.FindObjectsOfTypeAll<AbilitySO>().FirstOrDefault(x => x.name == abId);

            if(abSO == null)
            {
                deserializedAbilities[str] = ab = null;
                return false;
            }

            var cost = new List<ManaColorSO>();
            for(var i = 1; i < data.Length; i++)
            {
                var costInfo = data[i];

                if (costInfo.StartsWith("SPLIT,"))
                {
                    var splitCostInfo = costInfo.Substring("SPLIT,".Length).Split([','], StringSplitOptions.RemoveEmptyEntries);
                    var validSplitComponents = new List<ManaColorSO>();

                    foreach(var inf in splitCostInfo)
                    {
                        if (!LoadedDBsHandler.PigmentDB.PigmentPool.TryGetValue(inf, out var c))
                            continue;

                        validSplitComponents.Add(c);
                    }

                    if(validSplitComponents.Count == 1)
                    {
                        cost.Add(validSplitComponents[0]);
                        continue;
                    }
                    else if(validSplitComponents.Count > 1)
                    {
                        cost.Add(Pigments.SplitPigment(validSplitComponents.ToArray()));
                        continue;
                    }
                }

                if(LoadedDBsHandler.PigmentDB.PigmentPool.TryGetValue(costInfo, out var pigment))
                {
                    cost.Add(pigment);
                    continue;
                }

                cost.Add(Pigments.Grey);
            }

            deserializedAbilities[str] = ab = new()
            {
                ability = abSO,
                cost = cost.ToArray(),
            };
            return true;
        }

        public static string SerializeAbility(CombatAbility cAbility)
        {
            if (cAbility == null || cAbility.ability == null)
                return string.Empty;

            var data = new List<string>() { cAbility.ability.name };
            if (cAbility.cost != null)
            {
                foreach (var pigment in cAbility.cost)
                {
                    if (LoadedDBsHandler.PigmentDB.PigmentPool.ContainsKey(pigment.pigmentID))
                    {
                        data.Add(pigment.pigmentID);
                        continue;
                    }

                    var comps = pigment.GetComponentPigments();
                    var validCompIds = new List<string>();
                    foreach (var comp in comps)
                    {
                        if (!LoadedDBsHandler.PigmentDB.PigmentPool.ContainsKey(pigment.pigmentID))
                            continue;

                        validCompIds.Add(pigment.pigmentID);
                    }

                    if (validCompIds.Count > 0)
                    {
                        data.Add($"SPLIT,{string.Join(",", validCompIds)}");
                        continue;
                    }

                    data.Add(Pigments.Grey.pigmentID);
                }
            }

            return string.Join("|", data);
        }

        public static bool DeserializePassive(string str, out BasePassiveAbilitySO passive)
        {
            if(deserializedPassives.TryGetValue(str, out var p))
            {
                passive = p;
                return passive != null;
            }

            if (string.IsNullOrEmpty(str))
            {
                deserializedPassives[str] = passive = null;
                return false;
            }

            passive = GetPassive(str);
            if (passive == null)
                passive = Resources.FindObjectsOfTypeAll<BasePassiveAbilitySO>().FirstOrDefault(x => x.name == str);

            deserializedPassives[str] = passive;
            return passive != null;
        }

        public static string SerializePassive(BasePassiveAbilitySO passive)
        {
            if (passive == null)
                return string.Empty;

            return passive.name;
        }
    }
}
