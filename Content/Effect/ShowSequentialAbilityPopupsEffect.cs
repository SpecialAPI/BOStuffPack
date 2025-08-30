using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class ShowSequentialAbilityPopupsEffect : EffectSO
    {
        public List<string> popups;
        public float delay = 0f;
        public bool doDelayAfterLastPopup = false;
        public bool doDelayBeforeFirstPopup = false;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (doDelayBeforeFirstPopup)
                CombatManager.Instance.AddUIAction(new WaitUIAction(delay));

            for(var i = 0; i < popups.Count; i++)
            {
                CombatManager.Instance.AddUIAction(new ShowAttackInformationUIAction(caster.ID, caster.IsUnitCharacter, popups[i]));

                if((i < popups.Count - 1) || doDelayAfterLastPopup)
                    CombatManager.Instance.AddUIAction(new WaitUIAction(delay));
            }

            return true;
        }
    }

    public class WaitUIAction(float waitTime) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            yield return new WaitForSeconds(waitTime);
        }
    }
}
