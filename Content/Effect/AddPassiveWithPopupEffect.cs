using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Effect
{
    public class AddPassiveWithPopupEffect : EffectSO
    {
        public BasePassiveAbilitySO passive;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var u = t.Unit;
                if(!u.AddPassiveAbility(passive))
                    continue;

                exitAmount++;
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(u.ID, u.IsUnitCharacter, passive.GetPassiveLocData().text, passive.passiveIcon));
            }

            return exitAmount > 0;
        }
    }
}
