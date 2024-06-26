using FMOD;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking.Types;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class NewtonsApple
    {
        public static void Init()
        {
            var name = "Newton's Apple";
            var flav = "\"Tastes like science\"";
            var desc = "At the start of combat, spawns Isaac Newton as a permanent party member.\nThis item is destroyed upon activation.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "NewtonsApple").AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,
                    getsConsumed = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(null, CreateScriptable<CopyAndSpawnCustomCharacterAnywhereEffect>(x =>
                        {
                            x._characterCopy = Newton().name;

                            x._extraModifiers = [];
                            x._nameAddition = NameAdditionLocID.NameAdditionNone;

                            x._rank = 0;
                            x._permanentSpawn = true;
                        }), 1)
                    })
                }
            };
        }

        public static CharacterSO Newton()
        {
            var ch = NewCharacter("Isaac Newton", Pigments.SplitPigment(Pigments.Green, Pigments.Red), "IsaacNewton_50", "IsaacNewton_Back_50", "IsaacNewton_OW");

            ch.movesOnOverworld = false;

            ch.GrabDamagedFrom("Kleiver_CH");
            ch.GrabDeathFrom("SmokeStacks_CH");
            ch.GrabDialogueFrom("SmokeStacks_CH");

            ch.passiveAbilities = new()
            {
                NewPassive<MultiCustomTriggerEffectPassive>("IsaacNewton_PA", "IsaacNewton", "Isaac Newton", "IsaacNewton_Passive_50").AutoDescription("This ally invented gravity")
            };

            ch.RankedDataSetup(4, i =>
            {
                var gravityDamage = Choose(i, 6, 8, 10, 12);

                var gravityPulltext = Choose(i, "gently nudge", "nudge", "pull", "punt");
                var gravityPullmult = Choose(i, 1f, 1.25f, 1.5f, 1.75f);

                var gravityName = $"{Choose(i, "Discover", "Invent", "Create", "Become")} Gravity";
                var gravityDesc = $"Move all enemies closer to this party member.\nDeal {gravityDamage} damage to the opposing enemy, give it gravity and {gravityPulltext} it towards this party member.";

                var gravity =
                    NewAbility(gravityName, gravityDesc, "AttackIcon_Gravity", new()
                    {
                        Effect(Relative(false, 0, -1, -2, -3, -4), CreateScriptable<SmartMoveTowardsEffect>(x => x.right = true)),
                        Effect(Relative(false, 0, 1, 2, 3, 4), CreateScriptable<SmartMoveTowardsEffect>(x => x.right = false)),

                        Effect(Opposing, Damage, gravityDamage),

                        Effect(Opposing, CreateScriptable<AddGravityEffect>(x =>
                        {
                            x.collider = ColliderType.None;
                            x.launchForce = new Vector3(0f, 50f, -100f) * gravityPullmult;
                            x.randomForce = 0f;
                        }))
                    },
                    new()
                    {
                        TargetIntent(Opposing, IntentForDamage(gravityDamage), IntentType_GameIDs.Misc.ToString()),

                        TargetIntent(Relative(false, -1, -2, -3, -4), IntentType_GameIDs.Swap_Right.ToString()),
                        TargetIntent(Relative(false, 1, 2, 3, 4), IntentType_GameIDs.Swap_Left.ToString()),
                    })
                    
                    .WithCustomId($"Gravity_{i + 1}_A")
                    .WithVisuals(Visuals_Wriggle, Opposing)
                    .Character(Pigments.Red, Pigments.Grey);

                var appleRuptured = Choose(i, 1, 2, 2, 3);
                var appleExtension = Choose(i, 1, 1, 2, 2);
                
                var applePulltext = Choose(i, "gently nudge", "nudge", "pull", "punt");
                var applePullmult = Choose(i, 1f, 1.33f, 1.66f, 2f);
                
                var appleChance = Choose(i, 2, 5, 6, 7);
                var appleAppleitem = Choose(i, "a Tainted Apple", "a Tainted Apple", "a Tainted Apple", "The Apple");
                var appleAppleid = Choose(i, "TaintedApple_TW", "TaintedApple_TW", "TaintedApple_TW", "TheApple_TW");

                var appleName = $"{Choose(i, "Rotten", "Fallen", "Falling", "The")} Apple";
                var appleDesc = $"Apply {appleRuptured} Ruptured to the opposing enemy.\nIncrease all negative status and field effects on the enemy side by {appleExtension}.\nGive the opposing enemy gravity, switch its collider to sphere and {applePulltext} it in a random direction.\n{appleChance}% chance to produce {appleAppleitem}.";

                var apple =
                    NewAbility(appleName, appleDesc, "AttackIcon_Apple", new()
                    {
                        Effect(Opposing, ApplyRuptured, appleRuptured),
                        Effect(EnemySide, CreateScriptable<IncreaseStatusEffectsEffect>(), appleExtension),

                        Effect(Opposing, CreateScriptable<AddGravityEffect>(x =>
                        {
                            x.collider = ColliderType.Sphere;
                            x.launchForce = Vector3.zero;
                            x.randomForce = 50f * applePullmult;
                        })),

                        Effect(null, CreateScriptable<ExtraLootListEffect>(x =>
                        {
                            x._nothingPercentage = 0;
                            x._shopPercentage = 0;
                            x._treasurePercentage = 0;

                            x._lockedLootableItems = [];
                            x._lootableItems = new() { new() { itemName = appleAppleid, probability = 100 } };
                        })).WithCondition(Chance(appleChance))
                    },
                    new()
                    {
                        TargetIntent(Opposing, IntentType_GameIDs.Status_Ruptured.ToString(), IntentType_GameIDs.Misc.ToString()),
                        TargetIntent(EnemySide, IntentType_GameIDs.Misc.ToString()),

                        TargetIntent(Self, IntentType_GameIDs.Misc.ToString())
                    })
                    
                    .WithCustomId($"Apple_{i + 1}_A")
                    .WithVisuals(Visuals_WasteAway, Opposing)
                    .Character(Pigments.SplitPigment(Pigments.Green, Pigments.Red));

                var forceDamage = Choose(i, 5, 7, 9, 13);

                var forcePulltext = Choose(i, "gently nudge", "nudge", "pull", "punt");
                var forcePullmult = Choose(i, 1f, 1.25f, 1.5f, 1.75f);

                var forceSpacesmoved = Choose(i, 1, 1, 2, 2);
                var forceMoveaddition = forceSpacesmoved > 1 ? $" {forceSpacesmoved} spaces" : "";

                var forceName = $"{Choose(i, "Add", "Apply", "Transfer", "Kick into")} Force";
                var forceDesc = $"Deal {forceDamage} damage to the opposing enemy.\nAdd gravity to the opposing enemy and {forcePulltext} it away from this party member. Move the opposing enemy{forceMoveaddition} to the left or right.";

                var force =
                    NewAbility(forceName, forceDesc, "AttackIcon_Force", new()
                    {
                        Effect(Opposing, Damage, forceDamage),

                        Effect(Opposing, CreateScriptable<AddGravityEffect>(x =>
                        {
                            x.collider = ColliderType.None;
                            x.launchForce = new Vector3(0f, 100f, 250f) * forcePullmult;
                            x.randomForce = 0f;
                        })),

                        Effect(Opposing, CreateScriptable<SwapToOneRandomSideXTimesEffect>(), forceSpacesmoved)
                    },
                    new()
                    {
                        TargetIntent(Opposing, IntentForDamage(forceDamage), forceSpacesmoved > 1 ? IntentType_GameIDs.Swap_Mass.ToString() : IntentType_GameIDs.Swap_Sides.ToString())
                    })
                    
                    .WithCustomId($"Force_{i + 1}_A")
                    .WithVisuals(Visuals_Contusion, Opposing)
                    .Character(Pigments.Grey, Pigments.Red);

                return new(Choose(i, 12, 16, 20, 24), [gravity, apple, force]);
            });

            return ch;
        }
    }
}
