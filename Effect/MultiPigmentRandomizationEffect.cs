using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class MultiPigmentRandomizationEffect : EffectSO
    {
        public Dictionary<ManaColorSO, List<ManaColorSO>> randomizeDict = [];
        public List<ManaColorSO> defaultRandomizeOptions = [];

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            var bar = stats.MainManaBar;
            var changedSlots = new List<int>();
            var newPigment = new List<ManaColorSO>();
            foreach (ManaBarSlot slot in bar.ManaBarSlots)
            {
                if (slot.IsEmpty)
                    continue;

                var pigmentHere = slot.ManaColor;
                if (randomizeDict == null || !randomizeDict.TryGetValue(pigmentHere, out var options))
                    options = defaultRandomizeOptions;

                if(options == null || options.Count == 0)
                    continue;

                var randomPigment = options.RandomElement();
                slot.SetMana(randomPigment);
                changedSlots.Add(slot.SlotIndex);
                newPigment.Add(randomPigment);
            }

            if (changedSlots.Count > 0)
                CombatManager.Instance.AddUIAction(new ModifyManaSlotsUIAction(bar.ID, changedSlots.ToArray(), newPigment.ToArray()));

            exitAmount = changedSlots.Count;
            return exitAmount > 0;
        }

        public static EffectSO Create(Dictionary<ManaColorSO, List<ManaColorSO>> randomizeDict, List<ManaColorSO> defaultRandomizeOptions = null)
        {
            var e = CreateScriptable<MultiPigmentRandomizationEffect>();
            e.randomizeDict = randomizeDict ?? [];
            e.defaultRandomizeOptions = defaultRandomizeOptions ?? [];

            return e;
        }
    }
}
