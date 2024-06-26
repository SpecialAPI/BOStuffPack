using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class ItemTools
    {
        public static ItemBuilder<T> NewItem<T>(string name, string flavor, string description, string sprite) where T : BaseWearableSO
        {
            return new()
            {
                Name = name,
                Flavor = flavor,
                Description = description,

                Sprite = sprite
            };
        }

        public static BaseWearableSO GetTotallyRandomTreasure()
        {
            var holder = LoadedDBsHandler.InfoHolder;
            var items = new List<string>(LoadedDBsHandler.ItemPoolDB._TreasurePool);

            while (items.Count > 0)
            {
                var idx = Random.Range(0, items.Count);
                var stuff = GetWearable(items[idx]);

                items.RemoveAt(idx);

                if (stuff != null && (!stuff.startsLocked || holder == null || holder.Game == null || holder.Game.IsItemUnlocked(stuff.name)))
                    return stuff;
            }
            return null;
        }
    }

    public class ItemBuilder<T> where T : BaseWearableSO
    {
        public string Name;
        public string Flavor;
        public string Description;

        public string Sprite;

        public BaseItemPools BasePools;
        public Dictionary<string, int> ModdedItemPools = [];

        public List<WearableStaticModifierSetterSO> Modifiers = [];

        public int FishProbability;
        public int ShopPrice;

        public string CustomID;

        public ItemBuilder<T> AddToTreasure()
        {
            BasePools |= BaseItemPools.Treasure;

            return this;
        }

        public ItemBuilder<T> AddToShop(int price)
        {
            BasePools |= BaseItemPools.Shop;
            ShopPrice = price;

            return this;
        }

        public ItemBuilder<T> AddToFish(int probability)
        {
            BasePools |= BaseItemPools.Fish;
            FishProbability = probability;

            return this;
        }

        public ItemBuilder<T> AddToModded(string poolName, int probability)
        {
            return AddToModded((poolName, probability));
        }

        public ItemBuilder<T> AddToModded(params (string poolName, int probability)[] moddedPools)
        {
            foreach(var (poolName, probability) in moddedPools)
            {
                ModdedItemPools[poolName] = probability;
            }

            return this;
        }

        public ItemBuilder<T> AddModifiers(params WearableStaticModifierSetterSO[] mods)
        {
            Modifiers.AddRange(mods);

            return this;
        }

        public T Build()
        {
            var itm = CreateScriptable<T>();

            itm.name = GetID();

            itm._itemName = Name;
            itm._flavourText = Flavor;
            itm._description = Description;

            itm.wearableImage = LoadSprite(Sprite);

            itm.staticModifiers = [.. Modifiers];

            itm.shopPrice = ShopPrice;
            itm.isShopItem = BasePools == BaseItemPools.Shop;

            LoadedDBsHandler.ItemUnlocksDB.AddNewItem(itm.name, itm, null, BasePools.HasFlag(BaseItemPools.Shop), BasePools.HasFlag(BaseItemPools.Treasure));

            if (BasePools.HasFlag(BaseItemPools.Shop))
                Database.Shop.Add(itm.name);

            if(BasePools.HasFlag(BaseItemPools.Treasure))
                Database.Treasures.Add(itm.name);

            if(BasePools.HasFlag(BaseItemPools.Treasure) || BasePools.HasFlag(BaseItemPools.Shop))
                Database.NormallyAccessibleItems.Add(itm.name);

            if (BasePools.HasFlag(BaseItemPools.Fish))
            {
                ItemUtils.AddItemFishingRodPool(itm, FishProbability, itm.startsLocked);
                ItemUtils.AddItemCanOfWormsPool(itm, FishProbability, itm.startsLocked);

                Database.Fish.Add(itm.name);
            }

            Database.AllItems.Add(itm.name);

            foreach (var md in ModdedItemPools)
                PostStart.ModdedPoolItems.Add((itm, md.Key, md.Value));

            return itm;
        }

        public string GetID()
        {
            if(!string.IsNullOrEmpty(CustomID))
                return CustomID;

            var idPostfix = string.Empty;

            if (BasePools.HasFlag(BaseItemPools.Shop))
                idPostfix += "_SW";

            if (BasePools.HasFlag(BaseItemPools.Treasure))
                idPostfix += "_TW";

            if (string.IsNullOrEmpty(idPostfix) || BasePools.HasFlag(BaseItemPools.Fish) || ModdedItemPools.Count > 0)
                idPostfix += "_ExtraW";

            return $"{Name.ToId()}{idPostfix}";
        }

        [Flags]
        public enum BaseItemPools
        {
            None = 0,
            Shop = 1,
            Treasure = 2,
            Fish = 4
        }
    }
}
