using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Treasure
{
    public static class ConjoinedFungi
    {
        public static void Init()
        {
            var name = "Conjoined Fungi";
            var flav = $"\"{"We are not welcome elsewhere.".Scale(50)}\"";
            var desc = "On combat start, merge all duplicate enemies.";

            var item = NewItem<MultiCustomTriggerEffectWearable>(name, flav, desc, "ConjoinedFungi").AddToTreasure().Build();

            item.triggerEffects = new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effect(Enemies, CreateScriptable<MergeEnemiesEffect>())
                    })
                }
            };
        }
    }
}
