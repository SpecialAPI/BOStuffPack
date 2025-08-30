using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class LinkedGitHubAdditionSetterTriggerEffect : TriggerEffect
    {
        public bool UseModsWithoutGitHub;
        public int AdditionPerMod;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            var list = UseModsWithoutGitHub ? LinkedGitHubManager.ModsWithoutGithubLinks : LinkedGitHubManager.ModsWithGithubLinks;
            var addition = AdditionPerMod * list.Count;

            if(addition <= 0)
                return;

            if (extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                CombatManager.Instance.AddUIAction(action);

            ex.AddModifier(new ScarsValueModifier(addition));
        }

        public override bool ManuallyHandlePopup => true;
    }
}
