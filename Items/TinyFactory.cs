using BOStuffPack.Conditions.Effector;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class TinyFactory
    {
        public static readonly string ID = "TinyFactory_SW".Prefix();
        public static readonly string ProducedPigmentSVID = "TinyFactoryProducedPigment";

        public static void Init()
        {
            var name = "Tiny Factory";
            var flav = "\"Tiny industries full of tiny people.\"";
            var desc = "This party member's \"Slap\" now costs universal grey pigment. At the start of each turn, produce all pigment used to perform this party member's \"Slap\" this combat.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "TinyFactory")
                .SetPrice(5)
                .SetStaticModifiers(BasicAbilityCostReplacementStaticModifier.Create(Pigments.Grey, 1))
                .AddToShop();

            NewStoredValue<IntEnumerableStoredValue>(ProducedPigmentSVID)
                .SetColor(StoredValueColor_Positive).SetFormat("Tiny Factory: {0}");

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnAbilityUsed.ToString(),
                    immediate = true,
                    doesPopup = false,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(AddUsedPigmentToStoreDataListEffect.Create(ProducedPigmentSVID))
                    }),

                    conditions = new()
                    {
                        AbilityIsBasicEffectorCondition.Create(true)
                    }
                },
                new()
                {
                    trigger = TriggerCalls.OnTurnStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(ProducePigmentFromStoreDataEnumerableEffect.Create(ProducedPigmentSVID))
                    }),

                    conditions = new()
                    {
                        StoreDataEnumerableEmptyEffectorCondition.Create(ProducedPigmentSVID, false)
                    }
                }
            });
        }
    }
}
