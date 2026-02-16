using BOStuffPack.ReversePatches;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class LocalCombatTools
    {
        public static bool AddNewEnemyWithOutput(this CombatStats stats, EnemySO enemy, int slot, bool givesExperience, string spawnSoundID, int maxHealth, out EnemyCombat spawnedEnemy)
        {
            if (!ReversePatchesFinished)
            {
                Debug.LogError($"Trying to do AddNewEnemyWithOutput before reverse patches are finished. This shouldn't happen.");
                spawnedEnemy = null;
                return false;
            }

            return AddNewEnemyOutputReversePatch.AddNewEnemyWithOutput_ReversePatch(stats, enemy, slot, givesExperience, spawnSoundID, maxHealth, out spawnedEnemy);
        }

        public static int CustomRerollRandomEnemyTurns(this Timeline_Base tl, ITurn unit, int turnsToReroll, Func<ITurn, int, int> selectAbility, bool rerollAll = false, bool ignoreCantReroll = false)
        {
            if(tl is Timeline_TurnBase tl_tb)
            {
                if (!tl_tb.Enemies.Contains(unit))
                    return 0;

                var abilitySlotsToReroll = new List<int>();
                for (int i = tl_tb.CurrentTurn + 1; i < tl_tb.Round.Count; i++)
                {
                    if (tl_tb.Round[i].turnUnit != unit || (!ignoreCantReroll && !unit.CanRerollAbility(tl_tb.Round[i].abilitySlot)))
                        continue;

                    abilitySlotsToReroll.Add(i);
                }

                var rerolledSlots = new List<int>();
                var oldAbs = new List<int>();
                var newAbs = new List<int>();
                var rerolledAbs = 0;
                while ((turnsToReroll > 0 || rerollAll) && abilitySlotsToReroll.Count > 0)
                {
                    var idx = Random.Range(0, abilitySlotsToReroll.Count);
                    var abSlot = abilitySlotsToReroll[idx];
                    abilitySlotsToReroll.RemoveAt(idx);

                    var turn = tl_tb.Round[abSlot];
                    var newAb = selectAbility(unit, turn.abilitySlot);
                    if (newAb == -1)
                        break;

                    oldAbs.Add(turn.abilitySlot);
                    newAbs.Add(newAb);
                    rerolledSlots.Add(abSlot);
                    turn.abilitySlot = newAb;
                    tl_tb.Round[abSlot] = turn;

                    if(!rerollAll)
                        turnsToReroll--;

                    rerolledAbs++;
                }

                if (rerolledSlots.Count > 0)
                    CombatManager.Instance.AddUIAction(new UpdateReRolledSlotTimelineUIAction(unit.ID, rerolledSlots.ToArray(), oldAbs.ToArray(), newAbs.ToArray()));

                return rerolledAbs;
            }
            else if(tl is Timeline_RealTime tl_rt)
            {
                if (!tl_rt.Enemies.Contains(unit))
                    return 0;

                var abilitySlotsToReroll = new List<int>();
                for (int i = (tl_rt._ProcessingTurn ? 1 : 0); i < tl_rt.Round.Count; i++)
                {
                    if (tl_rt.Round[i].turnUnit != unit || (!ignoreCantReroll && !unit.CanRerollAbility(tl_rt.Round[i].abilitySlot)))
                        continue;

                    abilitySlotsToReroll.Add(i);
                }

                var rerolledSlots = new List<int>();
                var oldAbs = new List<int>();
                var newAbs = new List<int>();
                var rerolledAbs = 0;
                while ((turnsToReroll > 0 || rerollAll) && abilitySlotsToReroll.Count > 0)
                {
                    var idx = Random.Range(0, abilitySlotsToReroll.Count);
                    var abSlot = abilitySlotsToReroll[idx];
                    abilitySlotsToReroll.RemoveAt(idx);

                    var turn = tl_rt.Round[abSlot];
                    var newAb = selectAbility(unit, turn.abilitySlot);
                    if (newAb == -1)
                        break;

                    oldAbs.Add(turn.abilitySlot);
                    newAbs.Add(newAb);
                    rerolledSlots.Add(abSlot);
                    turn.abilitySlot = newAb;
                    tl_rt.Round[abSlot] = turn;

                    if (!rerollAll)
                        turnsToReroll--;

                    rerolledAbs++;
                }

                if (rerolledSlots.Count > 0)
                    CombatManager.Instance.AddUIAction(new UpdateReRolledSlotTimelineUIAction(unit.ID, rerolledSlots.ToArray(), oldAbs.ToArray(), newAbs.ToArray()));

                return rerolledAbs;
            }

            return 0;
        }
    }
}
