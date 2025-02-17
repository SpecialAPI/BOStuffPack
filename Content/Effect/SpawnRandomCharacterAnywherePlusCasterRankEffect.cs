using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class SpawnRandomCharacterAnywherePlusCasterRankEffect : EffectSO
    {
        public bool permanent;
        public int additionalRank;
        public bool usePreviousExitValueAsHealth;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            var charDb = LoadedDBsHandler.CharacterDB;

            if (charDb == null)
                return false;

            var chars = charDb.GetRandomCharacters(entryVariable);

            var rank = additionalRank;

            if (caster is CharacterCombat cc && cc.ClampedRank is int casterRank)
                rank += casterRank;

            foreach (var ch in chars)
                CombatManager.Instance.AddSubAction(new SpawnCharacterAction(ch, -1, true, "", permanent, rank, ch.GenerateAbilities(), usePreviousExitValueAsHealth ? PreviousExitValue : ch.GetMaxHealth(rank)));

            return true;
        }
    }
}
