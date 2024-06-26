using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effects
{
    public class ShuffleHealthEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var healths = new List<(int current, int max, ManaColorSO color)>();
            var units = new List<IUnit>();

            foreach (var t in targets)
            {
                if (t.HasUnit)
                {
                    healths.Add((t.Unit.CurrentHealth, t.Unit.MaximumHealth, t.Unit.HealthColor));
                    units.Add(t.Unit);
                }
            }

            healths.Shuffle();

            for (int i = 0; i < units.Count; i++)
            {
                units[i].MaximizeHealth(healths[i].max);
                units[i].ChangeHealthTo(healths[i].current);
                units[i].ChangeHealthColor(healths[i].color);

                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
