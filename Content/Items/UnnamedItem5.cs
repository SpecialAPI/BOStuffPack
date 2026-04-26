using BOStuffPack.Content.Conditions.Effector;
using BOStuffPack.Content.StoredValues;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    public static class UnnamedItem5
    {
        public static void Init()
        {
            var name = "Unnamed Item 5";
            var flav = "\"WIP\"";
            var desc = "This party member can now perform 2 abilities per turn. At the start of combat, double this party member's ability costs.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("UnnamedItem5_TW")
                .SetBasicInformation(name, flav, desc, "")
                .SetPrice(11)
                .AddToTreasure();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<MultiplyTargetAbilityCostsEffect>(), 2, Targeting.Slot_SelfSlot)
                    })
                },

                CharacterMultiAttackTriggerEffect.RestoreSV(LocalStoredValues.StoredValue_UnnamedItem5._UnitStoreDataID, 2),
                CharacterMultiAttackTriggerEffect.Refresh(LocalStoredValues.StoredValue_UnnamedItem5._UnitStoreDataID, true),
            });
        }
    }
}
