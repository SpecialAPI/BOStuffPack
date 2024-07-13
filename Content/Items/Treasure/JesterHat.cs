using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class JesterHat
    {
        public static void Init()
        {
            var name = "Jester Hat";
            var flav = "\"Can do anything\"";
            var desc = "This party member has Skittish as a passive.\nAdds \"Pirouette\" as an additional ability, a damaging ability with a different additional effect each turn.";

            var abilityName = "Pirouette";
            var abilityDesc = "Deals 7 indirect damage to the Opposing enemy.\nAdditional effect is different each turn.";

            var ab =
                NewAbility(abilityName, abilityDesc, "AttackIcon_Question",

                new()
                {
                    Effect(Self, PlayVisuals(Visuals_MiddleFinger)).WithCondition(TurnInRotation(1, 9)),

                    Effect(EnemySlots, PlayVisuals(Visuals_Wriggle)).WithCondition(TurnInRotation(2, 9)),
                    Effect(Enemies, ApplyScars, 1).WithCondition(TurnInRotation(2, 9)),

                    Effect(Self, PlayVisuals(Visuals_Malpractice)).WithCondition(TurnInRotation(3, 9)),
                    Effect(Self, ApplyFrail, 2).WithCondition(TurnInRotation(3, 9)),

                    Effect(AllySide, PlayVisuals(Visuals_Shield)).WithCondition(TurnInRotation(4, 9)),
                    Effect(AllySide, ApplyShield, 3).WithCondition(TurnInRotation(4, 9)),

                    Effect(Self, PlayVisuals(Visuals_MiddleFinger)).WithCondition(TurnInRotation(5, 9)),

                    Effect(AllySlots, PlayVisuals(Visuals_Heal)).WithCondition(TurnInRotation(6, 9)),
                    Effect(null, VariableForNext, 8).WithCondition(TurnInRotation(6, 9)),
                    Effect(Allies, CreateScriptable<HealRandomTargetBetweenPreviousAndEntry>(), 12).WithCondition(TurnInRotation(6, 9)),

                    Effect(AllySlots, PlayVisuals(Visuals_Concentration)).WithCondition(TurnInRotation(7, 9)),
                    Effect(Allies, CreateScriptable<ShuffleHealthEffect>()).WithCondition(TurnInRotation(7, 9)),

                    Effect(Opposing, PlayVisuals(Visuals_Wrath)).WithCondition(TurnInRotation(8, 9)),
                    Effect(Opposing, CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Berserk)).WithCondition(TurnInRotation(8, 9)),

                    Effect(AllySlots, PlayVisuals(Visuals_Heal)).WithCondition(TurnInRotation(9, 9)),
                    Effect(null, VariableForNext, 2).WithCondition(TurnInRotation(9, 9)),
                    Effect(Allies, CreateScriptable<RandomHealBetweenPreviousAndEntryEffect>(), 5).WithCondition(TurnInRotation(9, 9)),

                    Effect(Opposing, IndirectDamage, 7)
                },

                new()
                {
                    TargetIntent(Opposing, IntentType_GameIDs.Damage_7_10.ToString()),

                    TargetIntent(AllySide, IntentType_GameIDs.Misc_Hidden.ToString()),
                    TargetIntent(EnemySide, IntentType_GameIDs.Misc_Hidden.ToString())
                })
                
                .WithFootnotes($"Current turn: {"{CurrentTurn}".Colorize(new(1f, 0.5f, 0f))}", new RichTextCurrentTurnReplacement())
                .Character(Pigments.RedBlue, Pigments.YellowPurple);

            var item = NewItem<BasicWearable>(name, flav, desc, "JesterHat").AddToTreasure().AddModifiers(ExtraAbility(ab), ExtraPassive(Passives.Skittish)).Build();
        }
    }
}
