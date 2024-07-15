using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.UnitExtension
{
    public class UnitExt(IUnit unit)
    {
        public List<PerformedAbilityInformation> CurrentlyPerformedAbilities = [];

        public List<ManaColorSO> HealthColors = [unit.HealthColor];
        public int HealthColorIndex = 0;

        public void AddHealthColor(ManaColorSO color)
        {
            HealthColors.Add(color);
        }
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
