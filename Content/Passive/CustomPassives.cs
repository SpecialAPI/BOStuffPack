using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Passive
{
    public static class CustomPassives
    {
        public static MultiCustomTriggerEffectPassive Merged;
        public static MultiCustomTriggerEffectPassive Shapeshifter;

        public static StatusEffectPassiveAbility TargetSwapped;
        public static StatusEffectPassiveAbility Frenzied;

        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedDamageCap = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedTargetShift = [];

        public static void Init()
        {
            Merged = NewPassive<MultiCustomTriggerEffectPassive>("Merged_PA", "Merged", "Merged", "Merged").WithEnemyDescription($"This enemy will perform an additional ability for each enemy merged into it.").AddToGlossary("This enemy will perform an additional ability for each enemy merged into it.").WithStoredValue(StoredValue_MergedCount);
            Merged.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.AttacksPerTurn.ToString(),
                    
                    effect = new ModifyIntegerReferenceTriggerEffect()
                    {
                        Operation = IntOperation.Add,
                        StoredValue = StoredValue_MergedCount._UnitStoreDataID,
                        UseStoredValue = true
                    },

                    immediate = true,
                    doesPopup = false
                }
            };

            Shapeshifter = NewPassive<MultiCustomTriggerEffectPassive>("Shapeshifter_PA", "Shapeshifter", "Shape-Shifter", "Shapeshifter").WithCharacterDescription("At the start of each turn, unequip this party member's held item and equip a random treasure item. Attempt to trigger that item's on combat start effects.").AddToGlossary("At the start of each turn, unequip this party member's held item and equip a random treasure item. Attempt to trigger that item's on combat start effects.");
            Shapeshifter.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnTurnStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(Self, CreateScriptable<EquipRandomTreasureEffect>())
                    })
                }
            };

            TargetSwapped = NewPassive<StatusEffectPassiveAbility>("TargetSwapped_PA", "TargetSwapped", "TargetSwapped", "TargetSwapped").AutoDescription("Permanently applies TargetSwap to self.\nTargetSwap makes this ally's abilities target as if they were on the opponent side.");
            TargetSwapped._Status = TargetSwap;

            Frenzied = NewPassive<StatusEffectPassiveAbility>("Frenzied_PA", "Frenzied", "Frenzied", "Frenzied").AutoDescription("Permanently applies Berserk to self.\nBerserk makes this ally deal double damage.");
            Frenzied._Status = Berserk;

            Glossary.CreateAndAddCustom_PassiveToGlossary("Damage Cap", "The damage received by this party member/enemy can't go above a set amount.", LoadSprite("DamageCap"));
            Glossary.CreateAndAddCustom_PassiveToGlossary("TargetShift", "All abilities performed by this party member/enemy are performed as if their position is offset.", LoadSprite("DamageCap"));
        }

        public static BasePassiveAbilitySO DamageCapGenerator(int amount)
        {
            return GetOrCreatePassive(GeneratedDamageCap, amount, x =>
            {
                var pass = NewPassive<MultiCustomTriggerEffectPassive>($"DamageCap_{x}_PA", "DamageCap", $"Damage Cap ({x})", "DamageCap").AutoDescription($"This ally can't take more than {x} damage.");

                pass.triggerEffects = new()
                {
                    new()
                    {
                        trigger = TriggerCalls.OnBeingDamaged.ToString(),
                        doesPopup = true,
                        immediate = true,

                        effect = new DamageCapTriggerEffect(amount)
                    }
                };

                return pass;
            });
        }

        public static BasePassiveAbilitySO TargetShiftGenerator(int amount)
        {
            return GetOrCreatePassive(GeneratedTargetShift, amount, x =>
            {
                var pass = NewPassive<MultiCustomTriggerEffectPassive>($"TargetShift_{TargetString(x).ToId()}_PA", "TargetShift", $"TargetShift ({TargetString(x)})", "TargetShift").AutoDescription($"All abilities performed by this ally are performed as if they are on the space {TargetString(x)} of them.").WithStoredValue(StoredValue_TargetShift);

                pass.triggerEffects = new()
                {
                    new()
                    {
                        trigger = CustomEvents.MODIFY_TARGETTING,
                        doesPopup = true,
                        immediate = true,

                        effect = new ModifyIntegerReferenceTriggerEffect()
                        {
                            Operation = IntOperation.Add,
                            StoredValue = StoredValue_TargetShift.name,
                            UseStoredValue = true,
                            Value = 0
                        },

                        conditions = new()
                        {
                            CreateScriptable<IgnoreTargetShiftCheckCondition>()
                        }
                    },

                    new()
                    {
                        trigger = CustomEvents.MODIFY_TARGETTING_INTENTS,
                        doesPopup = false,
                        immediate = true,

                        effect = new ModifyIntegerReferenceTriggerEffect()
                        {
                            Operation = IntOperation.Add,
                            StoredValue = StoredValue_TargetShift.name,
                            UseStoredValue = true,
                            Value = 0
                        },

                        conditions = new()
                        {
                            CreateScriptable<IgnoreTargetShiftCheckCondition>()
                        }
                    }
                };

                pass.connectionEffects = new()
                {
                    new()
                    {
                        doesPopup = false,
                        immediate = true,

                        effect = new PerformEffectTriggerEffect(new()
                        {
                            Effect(null, CreateScriptable<CasterStoreValueSetterEffect>(x => x.m_unitStoredDataID = StoredValue_TargetShift.name), x)
                        })
                    }
                };

                return pass;
            });
        }

        private static TValue GetOrCreatePassive<TKey, TValue>(IDictionary<TKey, TValue> readFrom, TKey key, Func<TKey, TValue> create)
        {
            if (readFrom.TryGetValue(key, out var value))
                return value;

            return readFrom[key] = create(key);
        }
    }
}
