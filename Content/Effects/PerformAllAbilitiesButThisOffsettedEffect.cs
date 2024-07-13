using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class PerformAllAbilitiesButThisOffsettedEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var abs = caster.Abilities();

            if (abs == null)
                return false;

            var validAbs = abs.FindAll(x => x != null && x.ability != null && x.ability.effects != null && !x.ability.effects.Any(x => x.effect != null && x.effect is PerformAllAbilitiesButThisOffsettedEffect));
            var offsettedAbilities = new List<(AbilitySO ablity, int offset)>();

            for(int i = 0; i < validAbs.Count; i++)
            {
                var offs = i - Mathf.CeilToInt((validAbs.Count - 1) / 2) + (validAbs.Count % 2 == 0 && i > Mathf.FloorToInt((validAbs.Count - 1) / 2) ? 1 : 0);

                offsettedAbilities.Add((validAbs[i].ability, offs));
                exitAmount++;
            }

            if (offsettedAbilities.Count <= 0)
                return false;

            CombatManager.Instance.AddUIAction(new AdvancedAnimationVisualsAction(offsettedAbilities.Where(x => x.ablity.visuals != null).Select(x => new AdvancedAnimationData()
            {
                playAudio = true,
                targets = x.ablity.animationTarget,
                targettingOffset = x.offset,
                timeDelay = 0f,
                visuals = x.ablity.visuals,
                zRotation = 0f
            }).ToList(), caster));

            foreach ((var a, var offs) in offsettedAbilities)
            {
                var exit = 0;

                for (int j = 0; j < a.effects.Length; j++)
                {
                    var effect = a.effects[j];
                    var condition = effect.condition;

                    if (condition == null || condition.MeetCondition(caster, a.effects, j))
                    {
                        var possibleTargets = effect.targets != null ? effect.targets.GetModifiedTargets(stats.combatSlots, caster.SlotID, caster.IsUnitCharacter, offs) : new TargetSlotInfo[0];

                        bool targetSlots = effect.targets == null || effect.targets.AreTargetSlots;

                        exit = effect.StartEffect(stats, caster, possibleTargets, targetSlots, exit);
                    }

                    else
                        exit = effect.FailEffect(exit);
                }
            }

            return true;
        }
    }
}
