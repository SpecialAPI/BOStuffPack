using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class CharacterTools
    {
        public static AdvancedCharacterSO NewCharacter(string name, ManaColorSO healthColor, string sprite, string backSprite = null, string overworldSprite = null)
        {
            return CreateScriptable<AdvancedCharacterSO>(x =>
            {
                x.name = $"{name.ToId()}_CH";

                x._characterName = name;
                x.healthColor = healthColor;

                x.characterSprite = LoadSprite(sprite);
                x.characterBackSprite = LoadSprite(backSprite ?? $"{sprite}_Back");
                x.characterOWSprite = LoadSprite(overworldSprite ?? $"{sprite}_OW", new(0.5f, 0f));

                x.movesOnOverworld = true;

                x.usesAllAbilities = false;
                x.usesBasicAbility = true;

                x.basicCharAbility = LoadedDBsHandler.AbilityDB.SlapAbility;

                x.passiveAbilities = [];

                LoadedDBsHandler.CharacterDB.AddNewCharacter(x.name, x);
            });
        }

        public static void SetupSounds(this CharacterSO ch, string damagedSound, string deathSound, string dialogueSound)
        {
            ch.damageSound = damagedSound ?? "";
            ch.deathSound = deathSound ?? "";
            ch.dxSound = dialogueSound ?? "";
        }

        public static void GrabDamagedFrom(this CharacterSO ch, string targetName, CharacterSoundType targetSound = CharacterSoundType.Damage)
        {
            ch.damageSound = GetCharacter(targetName).GetCharacterSound(targetSound);
        }

        public static void GrabDeathFrom(this CharacterSO ch, string targetName, CharacterSoundType targetSound = CharacterSoundType.Death)
        {
            ch.damageSound = GetCharacter(targetName).GetCharacterSound(targetSound);
        }

        public static void GrabDialogueFrom(this CharacterSO ch, string targetName, CharacterSoundType targetSound = CharacterSoundType.Dialogue)
        {
            ch.damageSound = GetCharacter(targetName).GetCharacterSound(targetSound);
        }

        public static void RankedDataSetup(this CharacterSO ch, int ranks, Func<int, CharacterRankedData> setup)
        {
            ch.rankedData ??= [];
            ch.rankedData.Clear();

            for(int i = 0; i < ranks; i++)
                ch.rankedData.Add(setup(i));
        }

        public static string GetCharacterSound(this CharacterSO ch, CharacterSoundType type)
        {
            return type switch
            {
                CharacterSoundType.Damage => ch.damageSound,
                CharacterSoundType.Death => ch.deathSound,
                CharacterSoundType.Dialogue => ch.dxSound,

                _ => ""
            };
        }

        public static T Choose<T>(int rank, params T[] data)
        {
            return data[Mathf.Clamp(rank, 0, data.Length - 1)];
        }

        public enum CharacterSoundType
        {
            Damage,
            Death,
            Dialogue
        }
    }
}
