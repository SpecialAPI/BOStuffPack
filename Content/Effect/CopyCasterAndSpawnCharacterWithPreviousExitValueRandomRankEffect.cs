using FMOD.Studio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class CopyCasterAndSpawnCharacterWithPreviousExitValueRandomRankEffect : EffectSO
    {
        public bool permanent;

        public int rankAddition;
        public int chanceToIncreaseRank;
        public int chanceToDecreaseRank;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if(!caster.IsUnitCharacter)
                return false;

            var cc = stats.TryGetCharacter(caster.ID);

            if(cc == null)
                return false;

            var ch = cc.Character;
            var rank = cc.Rank;

            var val = Random.Range(0, 100);

            if (val < chanceToIncreaseRank)
                rank += rankAddition;

            else if(val < chanceToIncreaseRank + chanceToDecreaseRank)
                rank -= rankAddition;

            rank = Mathf.Max(0, rank);

            for(var i = 0; i < entryVariable; i++)
                CombatManager.Instance.AddSubAction(new SpawnCharacterAction(ch, -1, true, "", permanent, rank, ch.GenerateAbilities(), PreviousExitValue, null));

            exitAmount = entryVariable;
            return true;
        }
    }
}
