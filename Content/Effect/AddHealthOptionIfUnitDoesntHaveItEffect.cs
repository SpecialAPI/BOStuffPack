using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class AddHealthOptionIfUnitDoesntHaveItEffect : EffectSO
    {
        public List<ManaColorSO> healthColorsToAdd;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (var t in targets)
            {
                if (t.HasUnit)
                {
                    var success = false;
                    var ext = t.Unit.Ext();

                    foreach (var hc in healthColorsToAdd)
                    {
                        if (ext.HealthColors.Contains(hc))
                            continue;

                        ext.AddHealthColor(hc);
                        success = true;
                    }

                    if (success)
                        exitAmount++;
                }
            }
            return exitAmount > 0;
        }
    }
}
