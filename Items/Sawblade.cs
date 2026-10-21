using BOStuffPack.Conditions.Effector;
using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class Sawblade
    {
        public static readonly string ID = "Sawblade_TW".Prefix();
        public static readonly string PopupSVID = "SawbladeStoredValue";

        public static void Init()
        {
            var healthReq = 50;
            var damageUp = 50;
            var gutted = 1;

            var name = "Sawblade";
            var flav = "\"Goodbye forever.\"";
            var desc = $"This party member deals {damageUp}% more damage to targets at {healthReq}% health or less. Upon dealing damage, apply {gutted} Gutted to the target.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "Sawblade")
                .SetPrice(10)
                .AddItemTypes(ItemType_GameIDs.Knife.ToString())
                .AddToTreasure();

            NewStoredValue<UnitStoreData_BasicSO>(PopupSVID);
            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillApplyDamage.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new PercentageModifierSetterTriggerEffect(damageUp, true)
                        .Add(new TargetStoredValueChangeTriggerEffect(PopupSVID, 1, IntOperation.Set)),

                    conditions = new()
                    {
                        TargetHealthComparisonEffectorCondition.Create(healthReq, IntComparison.LessThanOrEqual, true)
                    }
                },
                new()
                {
                    trigger = LocalCustomTriggers.OnTargetDamaged,
                    immediate = false,
                    doesPopup = true,

                    effect = null, // do popup if didnt apply damageup

                    conditions = new()
                    {
                        TargetStoredValueComparisonEffectorCondition.Create(PopupSVID, 0, IntComparison.LessThanOrEqual)
                    }
                },
                new()
                {
                    trigger = LocalCustomTriggers.OnTargetDamaged,
                    immediate = false,
                    doesPopup = false,

                    effect = new TargetApplyStatusTriggerEffect(StatusField.Gutted, 1, unitValueIndex: 1)
                        .Add(new TargetStoredValueChangeTriggerEffect(PopupSVID, 0, IntOperation.Set)),
                }
            });
        }
    }
}
