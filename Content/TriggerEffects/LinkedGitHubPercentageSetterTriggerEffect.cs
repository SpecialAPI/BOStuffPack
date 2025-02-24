using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class LinkedGitHubPercentageSetterTriggerEffect : TriggerEffect
    {
        public bool UseModsWithoutGitHub;
        public int PercentagePerMod;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, TriggerEffectExtraInfo extraInfo)
        {
            if (args is not DamageDealtValueChangeException ex)
                return;

            var list = UseModsWithoutGitHub ? LinkedGitHubManager.ModsWithoutGithubLinks : LinkedGitHubManager.ModsWithGithubLinks;
            var percentage = PercentagePerMod * list.Count;

            if (percentage <= 0)
                return;

            if (extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                CombatManager.Instance.AddUIAction(action);

            ex.AddModifier(new PercentageValueModifier(true, percentage, true));
        }

        public override bool ManuallyHandlePopup => true;
    }
}
