using BOStuffPack.CustomTrigger.Args;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class OverridePigmentOnDamageTriggerEffect : TriggerEffect
    {
        public List<ManaColorSO> pigments = [];

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (pigments == null)
                return;

            if (args is not ModifyCanProducePigmentFromDamageReference canProduceRef || canProduceRef.damagedUnit is not IUnit damagedUnit)
                return;

            canProduceRef.canProducePigmentFromDamage = false;
            foreach (var p in pigments)
            {
                if(p == null)
                    continue;

                CombatManager.Instance.ProcessImmediateAction(new AddManaToManaBarAction(p, 1, damagedUnit.IsUnitCharacter, damagedUnit.ID));
            }
        }
    }
}
