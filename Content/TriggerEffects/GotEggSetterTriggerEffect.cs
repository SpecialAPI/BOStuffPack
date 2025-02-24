using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class GotEggSetterTriggerEffect : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, TriggerEffectExtraInfo extraInfo)
        {
            if (args is not DamageDealtValueChangeException ex)
                return;

            var eggUnits = new List<IUnit>();
            var eggItems = new List<IUnit>();

            foreach(var u in CombatManager.Instance._stats.UnitsOnField())
            {
                if(u == null)
                    continue;

                if(u.UnitTypes != null && Array.IndexOf(u.UnitTypes, "Egg") >= 0)
                    eggUnits.Add(u);

                if(u.HasUsableItem && u.HeldItem._ItemTypeIDs != null && Array.IndexOf(u.HeldItem._ItemTypeIDs, "Egg") >= 0)
                    eggItems.Add(u);
            }

            var extraEggsIntRef = new IntegerReference(0);
            CombatManager.Instance.PostNotification("TheEvil_ExtraEggs", null, extraEggsIntRef);
            var extraEggs = extraEggsIntRef.value;

            var eggs = eggUnits.Count + eggItems.Count + extraEggs;

            if (eggs <= 0)
                return;

            var addition = (int)Mathf.Log(eggs, 2) + 1;

            if (addition <= 0)
                return;

            if (extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                CombatManager.Instance.AddUIAction(action);

            ex.AddModifier(new AdditionValueModifier(true, addition));
        }

        public override bool ManuallyHandlePopup => true;
    }
}
