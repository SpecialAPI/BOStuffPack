using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class SetTimeScaleEffect : EffectSO
    {
        public float timeScale = -1f;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (timeScale < 0f)
                return false;

            CombatManager.Instance.AddUIAction(new SetTimeScaleUIAction(timeScale));

            return true;
        }
    }

    public class SetTimeScaleUIAction(float timeScale) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            Time.timeScale = timeScale;

            yield return null;
        }
    }
}
