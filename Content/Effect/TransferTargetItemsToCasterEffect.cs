using FMOD.Studio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class TransferTargetItemsToCasterEffect : EffectSO
    {
        public UnitStoreData_BasicSO itemsStoredValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (caster is not CharacterCombat cc)
                return false;

            var items = new List<BaseWearableSO>();
            var id = caster.ID;

            foreach (var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var u = t.Unit;

                if(!u.HasUsableItem)
                    continue;

                items.Add(u.HeldItem);
                u.TryUnequipItem();
                exitAmount++;
            }

            if (items.Count <= 0)
                return false;

            var mods = cc.CharacterWearableModifiers;
            var extraAbs = new List<CharacterAbility>();

            foreach(var itm in items)
            {
                if (itm == null)
                    continue;

                var tempMods = new WearableStaticModifiers();
                itm.OnCharacterAttached(tempMods, cc.Character, cc.Rank);

                mods.MaximumHealthModifier += tempMods.MaximumHealthModifier;
                mods.CurrencyMultiplierModifier += tempMods.CurrencyMultiplierModifier;
                mods.RankModifier += tempMods.RankModifier;

                mods.UsesBasicBool |= tempMods.UsesBasicBool;
                mods.UsesAllAbilitiesBool |= tempMods.UsesAllAbilitiesBool;
                mods.UsesBasicAbilityModifier |= tempMods.UsesBasicAbilityModifier;
                mods.UsesAllAbilitiesModifier |= tempMods.UsesAllAbilitiesModifier;
                mods.IsMainCharacterModifier |= tempMods.IsMainCharacterModifier;

                if(tempMods.HasHealthColorModifier)
                    mods.HealthColorModifier = tempMods.HealthColorModifier;
                if(tempMods.HasBasicAbilityModifier)
                    mods.BasicAbilityModifier = tempMods.BasicAbilityModifier;
                if(tempMods.HasExtraAbilityModifier)
                    extraAbs.Add(tempMods.ExtraAbilityModifier);

                foreach(var pa in tempMods.ExtraPassiveAbilities)
                    mods.AddExtraPassive(pa);
                foreach(var kvp in tempMods.ModdedDataSetter)
                    mods.SetModdedDataSetter(kvp.Key, kvp.Value);
            }

            cc.ClampedRank = cc.Character.ClampRank(cc.Rank + mods.RankModifier);
            cc.CurrencyMultiplier = mods.CurrencyMultiplierModifier;

            var prevMaximumHealth = cc.MaximumHealth;
            cc.MaximumHealth = cc.Character.GetMaxHealth(cc.ClampedRank);
            cc.MaximumHealth = Mathf.Max(1, mods.MaximumHealthModifier + cc.MaximumHealth);

            var prevHealthColor = cc.HealthColor;
            cc.HealthColor = mods.HasHealthColorModifier ? mods.HealthColorModifier : cc.Character.healthColor;

            cc.CurrentHealth = Mathf.Min(cc.CurrentHealth, cc.MaximumHealth);
            cc.SetUpDefaultAbilities(true);

            foreach(var exAb in extraAbs)
                cc.AddExtraAbility(new(exAb));

            if (mods.ExtraPassiveAbilities != null)
            {
                foreach (BasePassiveAbilitySO extraPassiveAbility in mods.ExtraPassiveAbilities)
                {
                    if (extraPassiveAbility != null && !extraPassiveAbility.Equals(null))
                    {
                        cc.ItemExtraPassives.Add(extraPassiveAbility);
                        if (!cc.ContainsPassiveAbility(extraPassiveAbility.m_PassiveID))
                        {
                            cc.PassiveAbilities.Add(extraPassiveAbility);
                            extraPassiveAbility.OnTriggerAttached(cc);
                            extraPassiveAbility.OnPassiveConnected(cc);
                        }
                    }
                }
            }

            mods.ProcessModdedDataFromNewItem(cc);

            foreach(var itm in items)
            {
                if (itm == null)
                    continue;

                itm.InitializeAttachedWearable(cc);
            }

            CombatManager.Instance.AddUIAction(new CharacterPassiveAbilityChangeUIAction(id, cc.PassiveAbilities.ToArray(), cc.CanSwapNoTrigger, cc.CanUseAbilitiesNoTrigger));

            if (prevMaximumHealth != cc.MaximumHealth)
                CombatManager.Instance.AddUIAction(new CharacterMaximumHealthChangeUIAction(id, cc.CurrentHealth, cc.MaximumHealth, cc.MaximumHealth - prevMaximumHealth));

            if (prevHealthColor != cc.HealthColor)
                CombatManager.Instance.AddUIAction(new CharacterHealthColorChangeUIAction(id, cc.HealthColor));

            if(itemsStoredValue != null)
            {
                caster.TryGetStoredData(itemsStoredValue._UnitStoreDataID, out var hold);

                if (hold.m_ObjectData is not List<BaseWearableSO> storedItems)
                    hold.m_ObjectData = storedItems = [];

                storedItems.Clear();
                storedItems.AddRange(items);
            }

            return exitAmount > 0;
        }
    }
}
