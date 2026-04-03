using BOStuffPack.CustomTrigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items
{
    [HarmonyPatch]
    public static class DirtBlock
    {
        public const int Chance = 157;
        public const int MaxRoll = 1_000_000;

        public static void Init()
        {
            var name = "Dirt Block";
            var flav = "\"Copyright (c) 2026 Captain Pretzel\"";
            var desc = "This item cannot be destroyed in combat.";

            var item = NewItem<MultiCustomTriggerEffectWearable>("DirtBlock_ExtraW")
                .SetBasicInformation(name, flav, desc, "DirtBlock")
                .SetPrice(0)
                .ExcludeFromConsole();
            LoadedWearables[item.name] = item;

            item.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = LocalCustomTriggers.ModifyCanDestroyItem,
                    doesPopup = false,
                    immediate = true,
                    
                    effect = new BoolHolderSetterTriggerffect(false)
                }
            });
        }

        [HarmonyPatch(typeof(RunDataSO), nameof(RunDataSO.GetPrizeItem))]
        [HarmonyPatch(typeof(RunDataSO), nameof(RunDataSO.BuyShopItem))]
        [HarmonyILManipulator]
        public static void MaybeGiveDirtBlock_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchCallOrCallvirt<PlayerInGameData>(nameof(PlayerInGameData.AddNewItem))))
                return;

            crs.Emit(OpCodes.Ldarg_0);
            crs.EmitStaticDelegate(MaybeGiveDirtBlock_Give);
        }

        public static void MaybeGiveDirtBlock_Give(RunDataSO run)
        {
            var playerData = run.playerData;
            if (!playerData.HasItemSpace)
                return;

            var roll = Random.Range(0, MaxRoll);
            if (roll >= Chance)
                return;

            playerData.AddNewItem(GetWearable(Profile.GetID("DirtBlock_ExtraW")));
        }
    }
}
