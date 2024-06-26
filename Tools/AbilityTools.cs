using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class AbilityTools
    {
        public static Dictionary<string, AbilitySO> LoadedBossAbilities = [];

        public static AbilityBuilder NewAbility(string name, string description, string sprite, List<EffectInfo> effects, List<IntentTargetInfo> intents)
        {
            return new AbilityBuilder()
            {
                Name = name,
                Description = description,
                
                Sprite = sprite,

                Effects = effects,
                Intents = intents
            };
        }

        public static AbilitySO GetAbility(string name)
        {
            var ret = GetEnemyAbility(name);

            if (ret != null)
                return ret;

            ret = GetCharacterAbility(name);

            return ret;
        }
    }

    public class AbilityBuilder
    {
        public string Name;
        public string Description;

        public string Sprite;

        public PrioritySO Priority = BrutalAPI.Priority.Normal;

        public AttackVisualsSO Visuals;
        public BaseCombatTargettingSO VisualTargets;

        public List<EffectInfo> Effects = [];
        public List<IntentTargetInfo> Intents = [];

        public string Footnotes;
        public List<RichTextProcessorBase> RichTextProcessors = [];

        public List<string> Flags = [];

        public UnitStoreData_BasicSO StoredValue;

        public string CustomID;

        public ManaColorSO[] Cost = [];
        public RaritySO Rarity = BrutalAPI.Rarity.Common;

        public AbilityBuilder WithVisuals(AttackVisualsSO visuals, BaseCombatTargettingSO visualTargets)
        {
            Visuals = visuals;
            VisualTargets = visualTargets;

            return this;
        }

        public AbilityBuilder WithCost(params ManaColorSO[] cost)
        {
            Cost = cost;

            return this;
        }

        public AbilityBuilder WithPriority(PrioritySO priority)
        {
            Priority = priority;

            return this;
        }

        public AbilityBuilder WithRarity(RaritySO rarity)
        {
            Rarity = rarity;

            return this;
        }

        public AbilityBuilder WithFootnotes(string footnotes, params RichTextProcessorBase[] richText)
        {
            Footnotes = footnotes;
            RichTextProcessors = richText.ToList();

            return this;
        }

        public AbilityBuilder WithFlags(params string[] flags)
        {
            Flags.AddRange(flags);

            return this;
        }

        public AbilityBuilder WithCustomId(string customId)
        {
            CustomID = customId;

            return this;
        }

        public AdvancedAbilitySO Build()
        {
            var ab = CreateScriptable<AdvancedAbilitySO>();

            ab.name = GetID();

            ab._abilityName = Name;
            ab._description = Description;

            ab.abilitySprite = Sprite == null ? GetAbility("Crush_A").abilitySprite : LoadSprite(Sprite);

            ab.priority = Priority;

            ab.visuals = Visuals;
            ab.animationTarget = VisualTargets;

            ab.effects = Effects.ToArray();
            ab.intents = Intents;

            ab.Footnotes = Footnotes;
            ab.RichTextProcessors = RichTextProcessors;

            ab.Flags = Flags;

            ab.specialStoredData = StoredValue;

            return ab;
        }

        public CharacterAbility Character()
        {
            return new() { ability = Build(), cost = Cost };
        }

        public CharacterAbility Character(params ManaColorSO[] cost)
        {
            return WithCost(cost).Character();
        }

        public EnemyAbilityInfo Enemy()
        {
            return new() { ability = Build(), rarity = Rarity };
        }

        public EnemyAbilityInfo Enemy(RaritySO rarity, PrioritySO priority)
        {
            return WithRarity(rarity).WithPriority(priority).Enemy();
        }

        public ExtraAbilityInfo Extra()
        {
            return new() { ability = Build(), cost = Cost, rarity = Rarity };
        }

        public ExtraAbilityInfo Extra(params ManaColorSO[] cost)
        {
            return WithCost(cost).Extra();
        }

        public ExtraAbilityInfo Extra(RaritySO rarity, PrioritySO priority)
        {
            return WithRarity(rarity).WithPriority(priority).Extra();
        }

        public string GetID()
        {
            if (!string.IsNullOrEmpty(CustomID))
                return CustomID;

            return $"{Name.ToId()}_A";
        }
    }
}
