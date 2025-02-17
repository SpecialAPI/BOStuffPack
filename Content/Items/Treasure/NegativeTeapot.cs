using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class NegativeTeapot
    {
        public static void Init()
        {
            //var name = "Negative Teapot";
            //var flav = "\"Double Negative\"";
            //var desc = "This character has permanent TargetSwap as a passive ability.\nAll damage dealt by this character is converted into 25% more healing.\nAll healing done by this character is converted into 75% more damage.";
            //
            //var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "NegativeTeapot").AddModifiers(ExtraPassive(TargetSwapped)).AddToTreasure().Build();
            //
            //item.triggerEffects = new()
            //{
            //    new()
            //    {
            //        trigger = TriggerCalls.OnWillApplyDamage.ToString(),
            //        doesPopup = true,
            //        immediate = true,
            //
            //        effect = new SwapHealingAndDamageTriggerEffect()
            //        {
            //            DamageToHealPercentage = 25,
            //            HealToDamagePercentage = 75
            //        },
            //
            //        conditions = new()
            //        {
            //            CreateScriptable<StoredValueComparisonEffectorCondition>(x => x.value = StoredValue_SwapHealingAndDamageTriggering.name)
            //        }
            //    },
            //
            //    new()
            //    {
            //        trigger = TriggerCalls.OnWillApplyHeal.ToString(),
            //        doesPopup = true,
            //        immediate = true,
            //
            //        effect = new SwapHealingAndDamageTriggerEffect()
            //        {
            //            DamageToHealPercentage = 25,
            //            HealToDamagePercentage = 75
            //        },
            //
            //        conditions = new()
            //        {
            //            CreateScriptable<StoredValueComparisonEffectorCondition>(x => x.value = StoredValue_SwapHealingAndDamageTriggering.name)
            //        }
            //    }
            //};
        }
    }
}
