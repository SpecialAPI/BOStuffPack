using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class WorldShatter
    {
        public static void Init()
        {
            var name = "World Shatter";
            var flav = "\"Goodbye\"";
            var desc = "Adds \"End of the Universe\" as an additional ability.";

            var abilityName = "End of the Universe";
            var abilityDesc = "Some things are simply too horrible to be described. Use it and find out, coward.\n\"At least it doesn't crash the game\"";

            var Damage = CreateScriptable<DamageEffect>();
            var ApplyRuptured = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Ruptured);
            var ApplyFocused = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Focused);
            var ApplySpotlight = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Spotlight);
            var ApplyGutted = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Gutted);
            var ApplyFrail = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Frail);
            var ApplyScars = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Scars);
            var MoveRandom = CreateScriptable<SwapToSidesEffect>();

            var effects = new List<EffectInfo>();
            var effectBases = new List<EffectInfo>()
            {
                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Wriggle), 0),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(ApplyRuptured, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(ApplyRuptured, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Wriggle), 0),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Flex), 0),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.MotherlyLove), 0),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(ApplyFocused, 0),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(ApplySpotlight, 0),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(ApplySpotlight, 0),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(ApplyGutted, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Red), 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(MoveRandom, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Puke), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(MoveRandom, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(MoveRandom, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(MoveRandom, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Puke), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(MoveRandom, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Puke), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Puke), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Blue), 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Puke), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Yellow), 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Purple), 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(ApplyFrail, 1),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(ApplyFrail, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<ForceGeneratePigmentColorEffect>(x => x.pigmentColor = Pigments.Grey), 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<ForceGeneratePigmentColorEffect>(x => x.pigmentColor = Pigments.Green), 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Lighthouse), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 3),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.Headshot), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 4),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 4),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.ComeHome), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(Damage, 5),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(), 0),

                Effects.GenerateEffect(CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals.EndIt), 2),
                Effects.GenerateEffect(CreateScriptable<SetTimeScaleEffect>(x => x.timeScale = 1f), 0),

                Effects.GenerateEffect(Damage, 18582),
                Effects.GenerateEffect(Damage, 296915811),

                Effects.GenerateEffect(ApplyScars, 1),
                Effects.GenerateEffect(ApplyScars, 1),
                Effects.GenerateEffect(ApplyScars, 1),
                Effects.GenerateEffect(ApplyScars, 1),
                Effects.GenerateEffect(ApplyScars, 1),
                Effects.GenerateEffect(ApplyScars, 1),
                Effects.GenerateEffect(ApplyScars, 1),
                Effects.GenerateEffect(ApplyScars, 1),
                Effects.GenerateEffect(ApplyScars, 1)
            };

            var prevTimeScale = 1f;
            var timeScaleIncrease = .1f;

            foreach(var e in effectBases)
            {
                if(e.effect is SetTimeScaleEffect stse && stse.timeScale < 0f)
                {
                    prevTimeScale += timeScaleIncrease;

                    stse.timeScale = prevTimeScale;
                }

                effects.Add(Effects.GenerateEffect(CreateScriptable<PerformEffectOnRandomTargetEffect>(x =>
                {
                    x.effect = e;

                    x.chanceToUseOverrideTargetting = 25;
                    x.overrideTargetting = Targeting.Unit_AllAllies;
                }), 0, Targeting.Unit_AllOpponents));
            }

            var item = NewItem<BasicWearable>("WorldShatter_TW")
                .SetBasicInformation(name, flav, desc, "WorldShatter")
                .SetPrice(-2)
                .AddToTreasure();

            var ability = NewAbility("EndOfTheUniverse_A")
                .SetBasicInformation(abilityName, abilityDesc, "AttackIcon_EOTU")
                .SetVisuals(Visuals.DemonCore, null)
                .SetEffects(effects)
                .SetIntents(new()
                {
                    TargetIntent(Targeting.Unit_AllOpponents, IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString()),
                    TargetIntent(Targeting.Unit_AllAllies, IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString()),
                })
                .AddToCharacterDatabase(true)
                .CharacterAbility(Pigments.Purple, Pigments.Yellow, Pigments.Blue, Pigments.Red);

            item.SetStaticModifiers(ExtraAbilityModifier(ability));
        }
    }
}
