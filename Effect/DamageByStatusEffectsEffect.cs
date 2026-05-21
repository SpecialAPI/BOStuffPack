using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class DamageByStatusEffectsEffect : CustomDamageEffectBase
    {
        public override int BaseDamageAmount(IUnit unit, TargetSlotInfo target, CombatStats stats, IUnit caster, bool areTargetSlots, int entryVariable, bool indirect)
        {
            return entryVariable + (unit.StatusEffectCount * PreviousExitValue);
        }

        public static EffectSO Create(bool indirect = false, bool usePreviousExit = false, bool successOnKill = false, bool ignoreShield = false, string deathType = nameof(DeathType_GameIDs.Basic), string specialDamage = "")
        {
            var e = Create<DamageByStatusEffectsEffect>(indirect, usePreviousExit, successOnKill, ignoreShield, deathType, specialDamage);

            return e;
        }
    }
}
