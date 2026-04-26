using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class CharacterMultiAttackTriggerEffect(string storedValue) : TriggerEffect
    {
        public override bool ManuallyHandlePopup => true;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (sender.GetCanUseAbilitiesNoTrigger())
                return;

            sender.TryGetStoredData(storedValue, out var data);
            if (data.m_MainData <= 0)
                return;

            if (triggerInfo.doesPopup && extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                CombatManager.Instance.AddUIAction(action);

            sender.RefreshAbilityUse();
            data.m_MainData -= 1;
        }

        public static TriggerEffectAndTriggerInfo Refresh(string storedValue, bool doesPopup = true, string trigger = nameof(TriggerCalls.OnAbilityUsed), bool immediate = false)
        {
            return new TriggerEffectAndTriggerInfo()
            {
                trigger = trigger,
                doesPopup = doesPopup,
                immediate = immediate,

                effect = new CharacterMultiAttackTriggerEffect(storedValue)
            };
        }

        public static TriggerEffectAndTriggerInfo RestoreSV(string storedValue, int attacksPerTurn, bool doesPopup = false, string trigger = nameof(TriggerCalls.OnTurnStart), bool immediate = false)
        {
            return new TriggerEffectAndTriggerInfo()
            {
                trigger = trigger,
                doesPopup = doesPopup,
                immediate = immediate,

                effect = new PerformEffectTriggerEffect(new()
                {
                    Effects.GenerateEffect(CreateScriptable<CasterStoreValueSetterEffect>(x => x.m_unitStoredDataID = storedValue), attacksPerTurn - 1)
                })
            };
        }
    }
}
