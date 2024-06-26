using System;
using System.Collections.Generic;
using System.Text;
using Yarn.Compiler;

namespace BOStuffPack.Content.Items.Shop
{
    public static class SchmuckleTicket
    {
        public static void Init()
        {
            var name = "Schmuckle Ticket";
            var flav = "\"Have a horrible day\"";
            var desc = "At the start of combat, spawn an ATM.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "SchmuckleTicket").AddToShop(5).Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(null, CreateScriptable<SpawnEnemyAnywhereEffect>(x => { x.enemy = ATM(); x._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString(); }), 1)
                    })
                }
            };
        }

        public static EnemySO ATM()
        {
            var enm = CreateScriptable<EnemySO>();

            enm.name = "ATM_EN";

            enm._enemyName = "ATM";

            enm.health = 100;
            enm.healthColor = Pigments.Grey;

            enm.priority = Priority.Normal;

            enm.abilitySelector = LoadedDBsHandler.MiscDB.RarityAbilitySelector;
            enm.abilities = new()
            {
                NewAbility("Whack", "Deals a Painful amount of damage to the Opposing party members. Moves this enemy to the Left or Right.", null, new()
                {
                    Effect(Opposing, Damage, 4),
                    Effect(Self, MoveRandom)
                }, new()
                {
                    TargetIntent(Opposing, IntentType_GameIDs.Damage_3_6.ToString()),
                    TargetIntent(Self, IntentType_GameIDs.Swap_Sides.ToString())
                })
                .WithVisuals(Visuals_Crush, Opposing)
                .Enemy(Rarity.Common, Priority.Normal),

                NewAbility("Withdraw Schmucks", "Deals almost no indirect damage to this enemy.", null, new()
                {
                    Effect(Self, IndirectDamage, 1)
                }, new()
                {
                    TargetIntent(Self, IntentType_GameIDs.Damage_1_2.ToString())
                })
                .WithVisuals(Visuals_Wriggle, Self)
                .Enemy(Rarity.Uncommon, Priority.Normal),

                NewAbility("Out of Order", "Destroys this ATM.", null, new()
                {
                    Effect(Self, CreateScriptable<DirectDeathEffect>())
                }, new()
                {
                    TargetIntent(Self, IntentType_GameIDs.Damage_Death.ToString())
                })
                .WithVisuals(Visuals_ClobberLeft, Self)
                .Enemy(Rarity.Uncommon, Priority.VerySlow)
            };

            var evilDialogue = Dialogues.CreateAndAddCustom_DialogueSO("ATMDialogue", Bundle.LoadAsset<YarnProgram>("ATMDialogue"), "ATMDialogue", "ATMDialogue");
            Dialogues.AddCustom_DialogueProgram(evilDialogue.m_DialogID, evilDialogue.dialog);

            Dialogues.AddCustom_SpeakerData("ATM_SpeakerData", CreateScriptable<SpeakerData>(x =>
            {
                x.speakerName = "ATM";

                x.portraitLooksCenter = true;
                x.portraitLooksLeft = false;

                x._defaultBundle = new()
                {
                    portrait = LoadSprite("ATMSpeaker"),
                    dialogueSound = "event:/ATM_DX",
                    bundleTextColor = Color.white
                };

                x._emotionBundles = [];
            }));

            var evilPassive = NewPassive<MultiCustomTriggerEffectPassive>("ATM_PA", "ATM", "ATM", "ATMPassive").AutoDescription("This ally is Evil.");
            evilPassive.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnDirectDamaged.ToString(),
                    doesPopup = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(null, CreateScriptable<StartDialogueConversationEffect>(x => x._dialogue = evilDialogue))
                    })
                }
            };

            enm.passiveAbilities = new()
            {
                Passives.Abomination1,
                Passives.Cashout,
                Passives.Withering,
                DamageCapGenerator(1),
                evilPassive
            };

            enm.size = 1;
            enm.unitTypes = [];

            enm.enemyTemplate = Bundle.LoadAsset<GameObject>("ATMEnemy").AddComponent<EnemyInFieldLayout>();
            enm.enemyTemplate.m_Data = enm.enemyTemplate.GetComponent<EnemyInFieldLayout_Data>();

            enm.damageSound = "event:/ATM_Damage2";
            enm.deathSound = "event:/ATM_Death";

            enm.enemySprite = LoadSprite("ATMPortrait");

            enm.enemyLoot = new([]);

            LoadedDBsHandler.EnemyDB.AddNewEnemy(enm.name, enm);

            return enm;
        }
    }
}
