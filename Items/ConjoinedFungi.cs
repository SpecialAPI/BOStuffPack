using BOStuffPack.Effect;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Items
{
    public static class ConjoinedFungi
    {
        public static readonly string ID = "ConjoinedFungi_TW".Prefix();

        public static void Init()
        {
            var name = "Conjoined Fungi";
            var flav = $"\"We are not welcome elsewhere.\"";
            var desc = "At the start of each turn merge all duplicate enemies.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(ID)
                .SetBasicInformation(name, flav, desc, "ConjoinedFungi")
                .SetPrice(2)
                .AddToTreasure();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnTurnStart.ToString(),

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<MergeEnemiesEffect>(), 0, Targeting.Unit_AllOpponents)
                    })
                }
            };
        }
    }
}
