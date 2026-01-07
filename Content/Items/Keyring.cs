using BOStuffPack.Content.StaticModifiers;
using BOStuffPack.Content.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class Keyring
    {
        public static void Init()
        {
            var name = "Key Ring";
            var flav = "\"Keymaster\"";
            var desc = "At the start of combat, add Lock 1, Lock 2, Lock 3 and Lock 4 as additional abilities.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("Keyring_TW")
                .SetBasicInformation(name, flav, desc, "Keyring")
                .SetPrice(4)
                .AddToTreasure();

            var keybladeDmg = 4;

            var colors = new List<(ManaColorSO pigmentColor, UnitStoreData_BasicSO keyStoredValue, string spritePostfix)>()
            {
                (Pigments.Red, LocalStoredValues.StoredValue_KeybladeRTurn, "Red"),
                (Pigments.Blue, LocalStoredValues.StoredValue_KeybladeBTurn, "Blue"),
                (Pigments.Yellow, LocalStoredValues.StoredValue_KeybladeYTurn, "Yellow"),
                (Pigments.Purple, LocalStoredValues.StoredValue_KeybladePTurn, "Purple"),
            };

            var lockAbilities = new List<CharacterAbility>();
            var addLockEffects = new List<EffectInfo>();

            for (int i = 0; i < colors.Count; i++)
            {
                var (pigment, keySV, spritePostfix) = colors[i];
                var idx = pigment.pigmentID[0];

                var keyName = $"Keyblade {idx}";
                var keyDesc = $"Deal {keybladeDmg} damage to the opposing enemy and refresh this party member.\nDisable the effects of Keyblade {idx} for this turn.";

                var keyAb = NewAbility($"Key{idx}_A")
                    .SetBasicInformation(keyName, keyDesc, $"AttackIcon_Key_{spritePostfix}")
                    .SetEffects(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<CasterStoredValueToTurnComparisonEffect>(x =>
                        {
                            x.value = keySV._UnitStoreDataID;
                            x.comparison = IntComparison.LessThan;
                        })),

                        Effects.GenerateEffect(CreateScriptable<AnimationVisualsEffect>(x => { x._visuals = Visuals.Slash; x._animationTarget = Targeting.Slot_Front; }), condition: Effects.CheckPreviousEffectCondition(true, 1)),
                        Effects.GenerateEffect(CreateScriptable<DamageEffect>(), keybladeDmg, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                        Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                        Effects.GenerateEffect(CreateScriptable<CasterSetStoredValueToTurnEffect>(x => x.value = keySV._UnitStoreDataID), condition: Effects.CheckPreviousEffectCondition(true, 4))
                    })
                    .SetIntents(new()
                    {
                        TargetIntent(Targeting.Slot_Front, IntentForDamage(keybladeDmg)),
                        TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Refresh.ToString())
                    })
                    .AddToCharacterDatabase()
                    .ExtraAbility(Pigments.Grey, Pigments.Grey);

                var lockName = $"Lock {idx}";
                var lockDesc = $"If no wrong pigment was used to perform this ability, replace this ability with Keyblade {idx} and refresh this party member.";

                var lockAb = NewAbility($"Lock{idx}_A")
                    .SetBasicInformation(lockName, lockDesc, $"AttackIcon_Lock_{spritePostfix}")
                    .SetEffects(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<CheckWrongPigmentEffect>()),

                        Effects.GenerateEffect(CreateScriptable<CasterReplaceExtraAbilityEffect>(x => { x.abilityToReplace = AdvancedAbilityReference($"Lock{idx}_A"); x.replacement = keyAb; }), condition: Effects.CheckPreviousEffectCondition(false, 1)),
                        Effects.GenerateEffect(CreateScriptable<RefreshAbilityUseEffect>(), 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2))
                    })
                    .AddIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Misc.ToString(), IntentType_GameIDs.Other_Refresh.ToString())
                    .AddToCharacterDatabase()
                    .CharacterAbility(pigment, pigment, pigment);

                lockAbilities.Add(lockAb);
                addLockEffects.Add(Effects.GenerateEffect(CreateScriptable<CasterAddOrRemoveExtraAbilityEffect>(x => x._extraAbility = ExtraAbilityModifier(lockAb))));
            }

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(addLockEffects)
                }
            };
            item.SetStaticModifiers(ModdedDataModifier(new OverworldAbilityDisplayStaticModifier(lockAbilities)));
        }
    }
}
