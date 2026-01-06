using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class PerformMultipleTriggerEffectsTriggerEffect(List<TriggerEffect> effects) : TriggerEffect
    {
        public List<TriggerEffect> effects = effects;
        public bool preventMultipleManualPopups = true;
        public bool forceNotManualPopups = false;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (ManuallyHandlePopup && preventMultipleManualPopups)
                extraInfo = new()
                {
                    activation = extraInfo.activation,
                    handler = new EvilTriggerEffectHandler(extraInfo.handler),
                };

            foreach (var effect in effects)
                effect.DoEffect(sender, args, triggerInfo, extraInfo);
        }

        public override bool ManuallyHandlePopup => !forceNotManualPopups && effects.Any(x => x.ManuallyHandlePopup);

        public class EvilTriggerEffectHandler(ITriggerEffectHandler orig) : ITriggerEffectHandler
        {
            public string DisplayedName => orig.DisplayedName;
            public Sprite Sprite => orig.Sprite;

            public bool didpopup = false;

            public bool TryGetPopupUIAction(int unitId, bool isUnitCharacter, bool consumed, out CombatAction action)
            {
                if (didpopup)
                {
                    action = null;
                    return false;
                }

                didpopup = true;
                return orig.TryGetPopupUIAction(unitId, isUnitCharacter, consumed, out action);
            }
        }
    }
}
