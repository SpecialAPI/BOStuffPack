using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class LocalIntentTools
    {
        private static readonly (int?, int?) LittleDamage     = (1, 2);
        private static readonly (int?, int?) PainfulDamage    = (3, 6);
        private static readonly (int?, int?) AgonizingDamage  = (7, 10);
        private static readonly (int?, int?) DeadlyDamage     = (11, 15);
        private static readonly (int?, int?) LethalDamage     = (16, 20);
        private static readonly (int?, int?) MortalDamage     = (21, null);

        private static readonly (int?, int?) SlightlyHeal        = (1, 4);
        private static readonly (int?, int?) Heal                = (5, 10);
        private static readonly (int?, int?) GreatlyHeal         = (11, 20);
        private static readonly (int?, int?) MiraculouslyHeal    = (21, null);

        private static readonly List<((int? min, int? max) range, string intent)> DamageIntentRanges = new()
        {
            (LittleDamage,       IntentType_GameIDs.Damage_1_2.ToString()),
            (PainfulDamage,      IntentType_GameIDs.Damage_3_6.ToString()),
            (AgonizingDamage,    IntentType_GameIDs.Damage_7_10.ToString()),
            (DeadlyDamage,       IntentType_GameIDs.Damage_11_15.ToString()),
            (LethalDamage,       IntentType_GameIDs.Damage_16_20.ToString()),
            (MortalDamage,       IntentType_GameIDs.Damage_21.ToString()),
        };

        private static readonly List<((int? min, int? max) range, string intent)> HealIntentRanges = new()
        {
            (SlightlyHeal,       IntentType_GameIDs.Heal_1_4.ToString()),
            (Heal,               IntentType_GameIDs.Heal_5_10.ToString()),
            (GreatlyHeal,        IntentType_GameIDs.Heal_11_20.ToString()),
            (MiraculouslyHeal,   IntentType_GameIDs.Heal_21.ToString()),
        };

        public static List<IntentTargetInfo> DamageIntentsWithStoredValueFilter(BaseCombatTargettingSO baseTargeting, string storedValue, int? min = null, int? max = null)
        {
            var output = new List<IntentTargetInfo>();
            var started = false;

            foreach(var (range, intent) in DamageIntentRanges)
            {
                if (min is not int min_ || min_.IsInRange(range))
                    started = true;

                if (!started)
                    continue;

                output.Add(TargetIntent(baseTargeting.FilterUnitByStoredValueInRange(storedValue, range.min, range.max), intent));

                if (max is int max_ && max_.IsInRange(range))
                    break;
            }

            return output;
        }
    }
}
