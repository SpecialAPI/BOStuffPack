using BOStuffPack.Effect;
using BOStuffPack.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class Keyring
    {
        public static readonly string ID = "Keyring_TW".Prefix();
        public static readonly string KeyID = "Key{0}_A".Prefix();
        public static readonly string LockID = "Lock{0}_A".Prefix();
        public static readonly string KeybladeTurnSVDB = "Keyblade{0}Turn_USD".Prefix();
        public static readonly string KeybladeTurnSVID = "Keyblade{0}Turn".Prefix();

        public static void Init()
        {
            var name = "Key Ring";
            var flav = "\"Keymaster\"";
            var desc = "At the start of combat, add Lock R, Lock B, Lock Y and Lock P as additional abilities.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "Keyring")
                .SetPrice(4)
                .AddToTreasure();

            var keybladeDmg = 4;

            var colors = new List<(ManaColorSO pigmentColor, string spritePostfix)>()
            {
                (Pigments.Red, "Red"),
                (Pigments.Blue, "Blue"),
                (Pigments.Yellow, "Yellow"),
                (Pigments.Purple, "Purple"),
            };

            var lockAbilities = new List<CharacterAbility>();
            var addLockEffects = new List<EffectInfo>();

            for (int i = 0; i < colors.Count; i++)
            {
                var (pigment, spritePostfix) = colors[i];
                var idx = pigment.pigmentID[0];

                var keyName = $"Keyblade {idx}";
                var keyDesc = $"Deal {keybladeDmg} damage to the opposing enemy and refresh this party member.\nDisable the effects of Keyblade {idx} for this turn.";

                var keySV = NewStoredValue<AdvancedStoredValueIntInfo>(string.Format(KeybladeTurnSVDB, idx), string.Format(KeybladeTurnSVID, idx))
                    .SetColor(StoredValueColor_Negative)
                    .SetFormat("Keyblade P Disabled")
                    .SetCustomDisplayCondition(CurrentTurnIsLowerThanValueDisplayCondition);

                var keyAb = NewAbility(string.Format(KeyID, idx))
                    .SetBasicInformationCharacter(keyName, keyDesc, $"AttackIcon_Key_{spritePostfix}")
                    .SetEffects(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<CasterStoredValueToTurnComparisonEffect>(x =>
                        {
                            x.value = keySV._UnitStoreDataID;
                            x.comparison = IntComparison.LessThan;
                        })),

                        Effects.GenerateEffect(CommonEffects.Animation(Visuals.Slash), 0, Targeting.Slot_Front, condition: Effects.CheckPreviousEffectCondition(true, 1)),
                        Effects.GenerateEffect(CommonEffects.Damage, keybladeDmg, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                        Effects.GenerateEffect(CommonEffects.Refresh, 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
                        Effects.GenerateEffect(CreateScriptable<CasterSetStoredValueToTurnEffect>(x => x.value = keySV._UnitStoreDataID), condition: Effects.CheckPreviousEffectCondition(true, 4))
                    })
                    .SetIntents(new()
                    {
                        TargetIntent(Targeting.Slot_Front, IntentForDamage(keybladeDmg)),
                        TargetIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Other_Refresh.ToString())
                    })
                    .ExtraAbility(Pigments.Grey, Pigments.Grey);

                var lockName = $"Lock {idx}";
                var lockDesc = $"If no wrong pigment was used to perform this ability, replace this ability with Keyblade {idx} and refresh this party member.";

                var lockAb = NewAbility(string.Format(LockID, idx))
                    .SetBasicInformationCharacter(lockName, lockDesc, $"AttackIcon_Lock_{spritePostfix}")
                    .SetEffects(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<CheckWrongPigmentEffect>()),

                        Effects.GenerateEffect(CreateScriptable<CasterReplaceExtraAbilityEffect>(x => { x.abilityToReplace = AdvancedAbilityReference($"Lock{idx}_A"); x.replacement = keyAb; }), condition: Effects.CheckPreviousEffectCondition(false, 1)),
                        Effects.GenerateEffect(CommonEffects.Refresh, 0, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2))
                    })
                    .AddIntent(Targeting.Slot_SelfSlot, IntentType_GameIDs.Misc.ToString(), IntentType_GameIDs.Other_Refresh.ToString())
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

        public static bool CurrentTurnIsLowerThanValueDisplayCondition(UnitStoreDataHolder holder)
        {
            return holder.m_MainData >= CombatManager.Instance._stats.TurnsPassed + 1;
        }
    }
}
