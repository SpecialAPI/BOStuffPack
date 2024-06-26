using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Shop
{
    public static class WorldShatter
    {
        public static void Init()
        {
            var name = "World Shatter";
            var flav = "\"Goodbye\"";
            var desc = "Adds \"End of the Universe\" as an additional ability.";

            var abilityName = "End of the Universe";
            var abilityDesc = "Does horrible things.\n\"At least it doesn't crash the game\"";

            var effects = new List<EffectInfo>();

            var effectBases = new List<EffectInfo>()
            {
                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Wriggle), 0),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, ApplyRuptured, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, ApplyRuptured, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Wriggle), 0),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Flex), 0),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_HandLick), 0),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, ApplyFocused, 0),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, ApplySpotlight, 0),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, ApplySpotlight, 0),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, ApplyGutted, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Red), 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, MoveRandom, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Vomit), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, MoveRandom, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, MoveRandom, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, MoveRandom, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Vomit), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, MoveRandom, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Vomit), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Vomit), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Blue), 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Vomit), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Yellow), 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<GenerateColorManaEffect>(x => x.mana = Pigments.Purple), 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, ApplyFrail, 1),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, ApplyFrail, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<ForceGeneratePigmentColorEffect>(x => x.pigmentColor = Pigments.Grey), 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<ForceGeneratePigmentColorEffect>(x => x.pigmentColor = Pigments.Green), 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Obsession), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 3),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_Headshot), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 4),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 4),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_ComeHome), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, Damage, 5),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(), 0),

                Effect(null, CreateScriptable<AnimationVisualsOnEffectTargetsEffect>(x => x.visuals = Visuals_MortalHorizon), 2),
                Effect(null, CreateScriptable<SetTimeScaleEffect>(x => x.timeScale = 1f), 0),

                Effect(null, Damage, 296915811),

                Effect(null, ApplyScars, 1),
                Effect(null, ApplyScars, 1),
                Effect(null, ApplyScars, 1),
                Effect(null, ApplyScars, 1),
                Effect(null, ApplyScars, 1),
                Effect(null, ApplyScars, 1),
                Effect(null, ApplyScars, 1),
                Effect(null, ApplyScars, 1),
                Effect(null, ApplyScars, 1),

                Effect(null, CreateScriptable<TriggerOverflowEffect>(), 0)
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

                effects.Add(Effect(Enemies, CreateScriptable<PerformEffectOnRandomTargetEffect>(x =>
                {
                    x.effect = e;

                    x.chanceToUseOverrideTargetting = 25;
                    x.overrideTargetting = Allies;
                }), 0));
            }

            var ability =
                NewAbility(abilityName, abilityDesc, "AttackIcon_EOTU", effects, new()
                {
                    TargetIntent(EnemySide, IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString()),
                    TargetIntent(AllySide, IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString(), IntentType_GameIDs.Misc_Hidden.ToString()),
                })

                .WithVisuals(Visuals_DemonCore, null)
                .Character(Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple);

            var item = NewItem<BasicWearable>(name, flav, desc, "WorldShatter").AddToTreasure().AddModifiers(ExtraAbility(ability)).Build();
        }
    }
}
