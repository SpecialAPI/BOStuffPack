using FMOD;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking.Types;

namespace BOStuffPack.Content.Items
{
    public static class NewtonsApple
    {
        public static void Init()
        {
            var name = "Newton's Apple";
            var flav = "\"Tastes like science\"";
            var desc = "At the start of combat, spawns Isaac Newton as a permanent party member.\nThis item is destroyed upon activation.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("NewtonsApple_TW")
                .SetBasicInformation(name, flav, desc, "NewtonsApple")
                .SetPrice(3)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    doesPopup = true,
                    getsConsumed = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<CopyAndSpawnCustomCharacterAnywhereEffect>(x =>
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
            var kleiver = GetCharacter("Kleiver_CH");
            var smokestacks = GetCharacter("SmokeStacks_CH");

            var isaacNewtonPassive = NewPassive<MultiCustomTriggerEffectPassive>("IsaacNewton_PA", "IsaacNewton")
                .SetBasicInformation("Isaac Newton", ResourceLoader.LoadSprite("IsaacNewton_Passive_50", ppu: 300))
                .AutoSetDescriptions("This ally invented gravity.")
                .AddToDatabase();

            var ch = NewCharacter("IsaacNewton_CH", "IsaacNewton")
                .SetBasicInformation("Isaac Newton", Pigments.SplitPigment(Pigments.Green, Pigments.Red), "IsaacNewton_50", "IsaacNewton_Back_50", "IsaacNewton_OW")
                .SetSounds(kleiver.damageSound, smokestacks.deathSound, smokestacks.dxSound)
                .SetMovesOnOverworld(false)
                .AddPassives(isaacNewtonPassive)
                .AddUnitTypes("Bird", "Sandwich_Silly");

            var gravityIntent = AddIntent("Gravity", isaacNewtonPassive.passiveIcon);

            ch.RankedDataSetup(4, (rank, abRank) =>
            {
                var gravityDamage = RankedValue(6, 8, 10, 12);

                var gravityPulltext = RankedValue("gently nudge", "nudge", "pull", "punt");
                var gravityPullmult = RankedValue(1f, 1.25f, 1.5f, 1.75f);

                var gravityName = $"{RankedValue("Discover", "Invent", "Create", "Become")} Gravity";
                var gravityDesc = $"Move all enemies closer to this party member.\nDeal {gravityDamage} damage to the opposing enemy, give it gravity and {gravityPulltext} it towards this party member.";

                var gravity = NewAbility($"Gravity{abRank}_A")
                    .SetBasicInformation(gravityName, gravityDesc, "AttackIcon_Gravity")
                    .SetVisuals(Visuals.WrigglingWrath, Targeting.Slot_Front)
                    .SetEffects(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<SmartMoveTowardsEffect>(x => x.right = true), 0, Targeting.GenerateSlotTarget([0, -1, -2, -3, -4, -5])),
                        Effects.GenerateEffect(CreateScriptable<SmartMoveTowardsEffect>(x => x.right = false), 0, Targeting.GenerateSlotTarget([0, 1, 2, 3, 4, 5])),

                        Effects.GenerateEffect(CreateScriptable<DamageEffect>(), gravityDamage, Targeting.Slot_Front),
                        Effects.GenerateEffect(CreateScriptable<AddGravityEffect>(x =>
                        {
                            x.collider = ColliderType.None;
                            x.launchForce = new Vector3(0f, 50f, -100f) * gravityPullmult;
                            x.randomForce = 0f;
                        }), 0, Targeting.Slot_Front),
                    })
                    .SetIntents(new()
                    {
                        TargetIntent(Targeting.Slot_OpponentAllLefts, IntentType_GameIDs.Swap_Right.ToString()),
                        TargetIntent(Targeting.Slot_OpponentAllRights, IntentType_GameIDs.Swap_Left.ToString()),
                        TargetIntent(Targeting.Slot_Front, IntentForDamage(gravityDamage), gravityIntent)
                    })
                    .AddToCharacterDatabase()
                    .CharacterAbility(Pigments.Red, Pigments.Grey);

                var appleRuptured = RankedValue(2, 3, 3, 4);
                var appleExtension = RankedValue(1, 1, 2, 2);

                var applePulltext = RankedValue("gently nudge", "nudge", "pull", "punt");
                var applePullmult = RankedValue(1f, 1.33f, 1.66f, 2f);

                var appleName = $"{RankedValue("Rotten", "Fallen", "The", "Bad")} Apple";
                var appleDesc = $"Apply {appleRuptured} Ruptured to the opposing enemy.\nGive the opposing enemy gravity, switch its collider to sphere and {applePulltext} it in a random direction.\nIncrease all negative status and field effects on the enemy side by {appleExtension}.";

                var apple = NewAbility($"Apple_{abRank}_A")
                    .SetBasicInformation(appleName, appleDesc, "AttackIcon_Apple")
                    .SetVisuals(Visuals.UglyOnTheInside, Targeting.Slot_Front)
                    .SetEffects(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Ruptured), appleRuptured, Targeting.Slot_Front),
                        Effects.GenerateEffect(CreateScriptable<AddGravityEffect>(x =>
                        {
                            x.collider = ColliderType.Sphere;
                            x.launchForce = Vector3.zero;
                            x.randomForce = 50f * applePullmult;
                        }), 0, Targeting.Slot_Front),

                        Effects.GenerateEffect(CreateScriptable<IncreaseStatusEffectsEffect>(), appleExtension, Targeting.Slot_OpponentAllSlots),
                    })
                    .SetIntents(new()
                    {
                        TargetIntent(Targeting.Slot_Front, IntentType_GameIDs.Status_Ruptured.ToString(), gravityIntent),
                        TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Misc.ToString())
                    })
                    .AddToCharacterDatabase()
                    .CharacterAbility(Pigments.SplitPigment(Pigments.Green, Pigments.Red));

                var forceDamage = RankedValue(5, 7, 9, 13);

                var forcePulltext = RankedValue("gently nudge", "nudge", "pull", "punt");
                var forcePullmult = RankedValue(1f, 1.25f, 1.5f, 1.75f);

                var forceSpacesmoved = RankedValue(1, 1, 2, 2);
                var forceMoveaddition = forceSpacesmoved > 1 ? $" {forceSpacesmoved} spaces" : "";

                var forceName = $"{RankedValue("Add", "Apply", "Transfer", "Kick into")} Force";
                var forceDesc = $"Deal {forceDamage} damage to the opposing enemy.\nAdd gravity to the opposing enemy and {forcePulltext} it away from this party member. Move the opposing enemy{forceMoveaddition} to the left or right.";

                var force = NewAbility($"Force_{abRank}_A")
                    .SetBasicInformation(forceName, forceDesc, "AttackIcon_Force")
                    .SetVisuals(Visuals.Contusion, Targeting.Slot_Front)
                    .SetEffects(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<DamageEffect>(), forceDamage, Targeting.Slot_Front),
                        Effects.GenerateEffect(CreateScriptable<AddGravityEffect>(x =>
                        {
                            x.collider = ColliderType.None;
                            x.launchForce = new Vector3(0f, 100f, 250f) * forcePullmult;
                            x.randomForce = 0f;
                        }), 0, Targeting.Slot_Front),

                        Effects.GenerateEffect(CreateScriptable<SwapToOneRandomSideXTimesEffect>(), forceSpacesmoved, Targeting.Slot_Front)
                    })
                    .AddIntent(Targeting.Slot_Front, IntentForDamage(forceDamage), gravityIntent, forceSpacesmoved > 1 ? IntentType_GameIDs.Swap_Mass.ToString() : IntentType_GameIDs.Swap_Sides.ToString())
                    .AddToCharacterDatabase()
                    .CharacterAbility(Pigments.Grey, Pigments.Red);

                return new(RankedValue(12, 16, 20, 24), [gravity, apple, force]);
            });

            ch.AddToDatabase(false);
            return ch;
        }
    }
}
