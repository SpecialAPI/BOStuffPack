using BOStuffPack.API.UnitExtension;
using BrutalAPI;
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

    public class PreprocessAbilityInformationAction(PerformedAbilityInformation info, IUnit caster) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            var holder = new ModifyTargettingInfo(new(false), new(0), info.ability);
            CombatManager.Instance.PostNotification(CustomEvents.MODIFY_TARGETTING, caster, holder);

            info.targetOffset = holder.intReference.value;
            info.isTargetsSwapped = holder.boolReference.value;

            yield break;
        }
    }

    public class RemoveAbilityInformationFromListAction(PerformedAbilityInformation info, IUnit caster) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            var ext = caster.Ext();

            if(ext != null)
                ext.CurrentlyPerformedAbilities.Remove(info);

            yield break;
        }
    }
}
