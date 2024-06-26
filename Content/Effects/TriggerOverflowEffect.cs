using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class TriggerOverflowEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (stats.overflowMana.OverflowManaAmount <= 0)
                return false;

            var chars = 0;
            var overflowAbsorbers = new HashSet<int>();
            var damagePercent = 0;
            var shouldTrigger = true;

            foreach (var ch in stats.CharactersOnField.Values)
            {
                if (!ch.IsAlive)
                    continue;

                chars++;

                if (ch.ShouldAbsorbOverflow)
                    overflowAbsorbers.Add(ch.ID);

                if (shouldTrigger)
                    shouldTrigger = shouldTrigger && ch.CanOverflowTrigger;
            }

            if (!shouldTrigger)
                return false;

            damagePercent = stats.overflowMana.DepleteAllStoredMana() * LoadedDBsHandler.CombatData.CharDmgPercPerPigmentOverflow;

            if (overflowAbsorbers.Count > 0)
                damagePercent = damagePercent * chars / overflowAbsorbers.Count;

            foreach (var ch in stats.CharactersOnField.Values)
            {
                if (!ch.IsAlive || damagePercent <= 0)
                    continue;

                if (overflowAbsorbers.Count <= 0 || overflowAbsorbers.Contains(ch.ID))
                {
                    var dmg = ch.CalculatePercentualAmount(damagePercent);

                    exitAmount += ch.ManaDamage(dmg, true, DeathType_GameIDs.Overflow.ToString());
                }
            }

            return exitAmount > 0;
        }
    }
}
