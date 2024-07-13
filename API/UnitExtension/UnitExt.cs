using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.UnitExtension
{
    public class UnitExt
    {
        public List<PerformedAbilityInformation> CurrentlyPerformedAbilities = [];
    }

    public class PerformedAbilityInformation
    {
        public int targetOffset;
        public bool isTargetsSwapped;

        public EffectAction effects;
        public PlayAbilityAnimationAction visuals;

        public AbilitySO ability;
    }
}
