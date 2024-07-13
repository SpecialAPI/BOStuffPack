using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace BOStuffPack.Content.TriggerEffects
{
    public class SwapHealingAndDamageTriggerEffect : TriggerEffect
    {
        public int HealToDamagePercentage;
        public int DamageToHealPercentage;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect effectsAndTrigger)
        {
            if (args is HealingDealtValueChangeException heal)
            {
                sender.SimpleSetStoredValue(StoredValue_SwapHealingAndDamageTriggering.name, 1);

                var dmg = heal.healingUnit.Damage(sender.WillApplyDamage(Mathf.Max(Mathf.RoundToInt(heal.amount * (HealToDamagePercentage / 100f + 1f)), 0), heal.healingUnit), sender, DeathType_GameIDs.None.ToString(), -1, true, true, false).damageAmount;
                heal.AddModifier(new SetToValueValueModifier(0));

                if (dmg > 0)
                    sender.DidApplyDamage(dmg);

                sender.SimpleSetStoredValue(StoredValue_SwapHealingAndDamageTriggering.name, 0);
            }
            else if (args is DamageDealtValueChangeException damage)
            {
                sender.SimpleSetStoredValue(StoredValue_SwapHealingAndDamageTriggering.name, 1);

                damage.damagedUnit.Heal(sender.WillApplyHeal(Mathf.Max(Mathf.RoundToInt(damage.amount * (DamageToHealPercentage / 100f + 1f)), 0), damage.damagedUnit), true, CombatType_GameIDs.Heal_Basic.ToString());
                damage.AddModifier(new SetToValueValueModifier(0));

                sender.SimpleSetStoredValue(StoredValue_SwapHealingAndDamageTriggering.name, 0);
            }
        }
    }
}
