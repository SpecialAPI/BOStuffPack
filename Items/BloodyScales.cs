namespace BOStuffPack.Items
{
    public static class BloodyScales
    {
        public static readonly string ID = "BloodyScales_TW".Prefix();

        public static void Init()
        {
            var name = "Bloody Scales";
            var flav = "\"For weighing blood.\"";
            var desc = "Upon this party member receiving direct damage, apply 1 Fury to them.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "BloodyScales")
                .SetPrice(2)
                .AddToTreasure();

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnDirectDamaged.ToString(),
                    immediate = false,
                    doesPopup = true,
                    
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CommonEffects.ApplyStatus(GrimoireStatusField.Fury), 1, Targeting.Slot_SelfSlot)
                    })
                }
            });
        }
    }
}