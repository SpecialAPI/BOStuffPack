using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.CustomEvent
{
    public class AbilityContextNotifyAction(IUnit unit, int abilityIdx, AbilitySO ability, FilledManaCost[] cost, string notif) : CombatAction
    {
        public IUnit unit = unit;
        public int abilityIdx = abilityIdx;
        public AbilitySO ability = ability;
        public FilledManaCost[] cost = cost;
        public string notification = notif;

        public override IEnumerator Execute(CombatStats stats)
        {
            CombatManager.Instance.PostNotification(notification, unit, new AbilityUsedContext(abilityIdx, ability, cost));

            yield break;
        }
    }
}
