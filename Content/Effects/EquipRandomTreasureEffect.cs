using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class EquipRandomTreasureEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach (var t in targets)
            {
                if (t == null || !t.HasUnit || t.Unit is not CharacterCombat cc)
                    continue;

                var notifs = NtfUtils.notifications?._table;

                var notifsToCheck = new string[]
                {
                        TriggerCalls.OnBeforeCombatStart.ToString(),
                        TriggerCalls.OnCombatStart.ToString(),
                        TriggerCalls.OnFirstTurnStart.ToString()
                };

                var oldCallDict = new Dictionary<string, List<Action<object, object>>>();

                if (notifs != null)
                {
                    foreach (var notif in notifsToCheck)
                    {
                        if (notifs.TryGetValue(notif, out var unitCallsMap) && unitCallsMap != null && unitCallsMap.TryGetValue(cc, out var calls))
                            oldCallDict[notif] = new(calls);
                    }
                }

                if (!cc.TrySetUpNewItem(GetTotallyRandomTreasure()))
                    continue;

                exitAmount++;
                notifs = NtfUtils.notifications?._table;

                if (notifs == null)
                    continue;

                foreach (var notif in notifsToCheck)
                {
                    if (!notifs.TryGetValue(notif, out var unitCallsMap) || unitCallsMap == null || !unitCallsMap.TryGetValue(cc, out var calls) || calls == null)
                        continue;

                    if (!oldCallDict.TryGetValue(notif, out var oldNotifCalls))
                        oldNotifCalls = null;

                    foreach (var call in calls)
                    {
                        if (call == null)
                            continue;

                        if (oldNotifCalls != null && oldNotifCalls.Contains(call))
                            continue;

                        call(cc, null);
                    }
                }
            }

            return exitAmount > 0;
        }
    }
}
