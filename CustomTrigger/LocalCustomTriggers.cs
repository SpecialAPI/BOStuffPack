using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger
{
    public static class LocalCustomTriggers
    {
        public static readonly string OnAnyoneFleetingEnd                       = $"{MOD_PREFIX}_{nameof(OnAnyoneFleetingEnd)}";
        public static readonly string OnAnyoneDamaged                           = $"{MOD_PREFIX}_{nameof(OnAnyoneDamaged)}";
        public static readonly string OnAnyoneDirectDamaged                     = $"{MOD_PREFIX}_{nameof(OnAnyoneDirectDamaged)}";
        public static readonly string OnAnyonesMaxHealthChanged                 = $"{MOD_PREFIX}_{nameof(OnAnyonesMaxHealthChanged)}";
        public static readonly string OnAnyoneHealed                            = $"{MOD_PREFIX}_{nameof(OnAnyoneHealed)}";
        public static readonly string ModifyCanProducePigmentFromDamage_Anyone  = $"{MOD_PREFIX}_{nameof(ModifyCanProducePigmentFromDamage_Anyone)}";
        public static readonly string OnBeforeAbilityAnimation                  = $"{MOD_PREFIX}_{nameof(OnBeforeAbilityAnimation)}";
        public static readonly string OnPassivePopup                            = $"{MOD_PREFIX}_{nameof(OnPassivePopup)}";
        public static readonly string OnTargetBeingDamaged                      = $"{MOD_PREFIX}_{nameof(OnTargetBeingDamaged)}";
        public static readonly string OnTargetDamaged                           = $"{MOD_PREFIX}_{nameof(OnTargetDamaged)}";
    }
}
