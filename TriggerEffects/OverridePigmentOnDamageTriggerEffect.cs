using BOStuffPack.CustomTrigger.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.TriggerEffects
{
    public class OverridePigmentOnDamageTriggerEffect(List<ManaColorSO> pigments, bool nullPigmentIsCasterHealthColor = false) : TriggerEffect
    {
        public List<ManaColorSO> pigments = pigments;
        public bool nullPigmentIsCasterHealthColor = nullPigmentIsCasterHealthColor;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (pigments == null)
                return;

            if (args is not ModifyCanProducePigmentFromDamageReference canProduceRef || canProduceRef.damagedUnit is not IUnit damagedUnit)
                return;

            canProduceRef.canProducePigmentFromDamage = false;
            foreach (var _p in pigments)
            {
                var p = _p;
                if (nullPigmentIsCasterHealthColor && p == null)
                    p = sender.HealthColor;
                if (p == null)
                    continue;

                CombatManager.Instance.ProcessImmediateAction(new AddManaToManaBarAction(p, 1, damagedUnit.IsUnitCharacter, damagedUnit.ID));
            }
        }
    }
}
