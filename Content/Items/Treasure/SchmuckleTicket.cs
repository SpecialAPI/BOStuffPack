using Pentacle.Advanced.BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Yarn.Compiler;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class SchmuckleTicket
    {
        public static void Init()
        {
            var name = "Schmuckle Ticket";
            var flav = "\"Have a horrible day\"";
            var desc = "At the start of combat, spawn an ATM.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("SchmuckleTicket_TW")
                .SetBasicInformation(name, flav, desc, "SchmuckleTicket")
                .SetPrice(5)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<SpawnEnemyAnywhereEffect>(x => { x.enemy = ATM(); x._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString(); }), 1)
                    })
                }
            };
        }

        public static EnemySO ATM()
        {
            var customCashout = NewPassive<MultiCustomTriggerEffectPassive>("Cashout_ATM_PA", PassiveType_GameIDs.Cashout.ToString())
                .SetBasicInformation("Cashout", Passives.Cashout.passiveIcon)
                .AutoSetDescriptions("Upon this ally receiving any damage, gain 1 coin.")
                .AddToDatabase();

            customCashout.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnDamaged.ToString(),
                    doesPopup = true,
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<ExtraCurrencyEffect>(), 1)
                    })
                }
            });

            var evilDialogue = Dialogues.CreateAndAddCustom_DialogueSO("ATMDialogue", Bundle.LoadAsset<YarnProgram>("ATMDialogue"), "ATMDialogue", "ATMDialogue");
            Dialogues.AddCustom_SpeakerData("ATM_SpeakerData", CreateScriptable<SpeakerData>(x =>
            {
                x.speakerName = "ATM";

                x.portraitLooksCenter = true;
                x.portraitLooksLeft = false;

                x._defaultBundle = new()
                {
                    portrait = ResourceLoader.LoadSprite("ATMSpeaker"),
                    dialogueSound = "event:/ATM_DX",
                    bundleTextColor = Color.white
                };

                x._emotionBundles = [];
            }));

            var atmSpeakHiddenEffect = CreateScriptable<MultiCustomTriggerEffectHiddenEffect>();
            atmSpeakHiddenEffect.name = $"{MOD_PREFIX}_ATMSpeak_HE";
            atmSpeakHiddenEffect.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnDirectDamaged.ToString(),
                    immediate = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<StartDialogueConversationEffect>(x => x._dialogue = evilDialogue))
                    })
                }
            };

            var enm = NewEnemy("ATM_EN")
                .SetName("ATM")
                .SetSprites("ATM_OW", "ATM_OW", "ATM_Corpse")
                .SetEnemyPrefab("ATMEnemy")
                .SetSounds("event:/ATM_Damage2", "event:/ATM_Death")
                .SetHealth(100, Pigments.Grey)
                .AddPassives(Passives.Abomination1, Passives.Skittish, customCashout, Passives.Withering)
                .AddHiddenEffects(atmSpeakHiddenEffect)
                .AddToDatabase(true, false, false);

            enm.SetAbilities(new()
            {
                NewAbility("Whack_A")
                .SetName("Whack")
                .SetDescription("Deals a painful amount of damage to the opposing party member")
                .SetVisuals(Visuals.Crush, Targeting.Slot_Front)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<DamageEffect>(), 5, Targeting.Slot_Front)
                })
                .AddIntent(Targeting.Slot_Front, IntentForDamage(5))
                .AddToEnemyDatabase()
                .EnemyAbility(Rarity.Common, Priority.Fast),

                NewAbility("WithdrawSchmucks_A")
                .SetName("Withdraw Schmucks")
                .SetDescription("Deals almost no indirect damage to this enemy.")
                .SetVisuals(Visuals.Wriggle, Targeting.Slot_SelfSlot)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<DamageEffect>(x => x._indirect = true), 1, Targeting.Slot_SelfSlot)
                })
                .AddIntent(Targeting.Slot_SelfSlot, IntentForDamage(1))
                .AddToEnemyDatabase()
                .EnemyAbility(Rarity.Rare, Priority.Fast),

                NewAbility("OutOfOrder_A")
                .SetName("Out of Order")
                .SetDescription("Deals a mortal amount of damage to this enemy.\nSteals 1-2 coins.")
                .SetVisuals(Visuals.Clobber_Left, Targeting.Slot_SelfSlot)
                .SetEffects(new()
                {
                    Effects.GenerateEffect(CreateScriptable<ExtraVariableForNextEffect>(), 21),
                    Effects.GenerateEffect(CreateScriptable<RandomDamageBetweenPreviousAndEntryEffect>(), 50, Targeting.Slot_SelfSlot),

                    Effects.GenerateEffect(CreateScriptable<LosePlayerCurrencyEffect>(), 1),
                    Effects.GenerateEffect(CreateScriptable<LosePlayerCurrencyEffect>(), 1, null, Effects.ChanceCondition(50)),
                })
                .AddIntent(Targeting.Slot_SelfSlot, [IntentForDamage(50), IntentType_GameIDs.Misc_Currency.ToString()])
                .EnemyAbility(Rarity.Uncommon, Priority.Fast)
            });

            return enm;
        }
    }
}
