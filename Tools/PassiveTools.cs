using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class PassiveTools
    {
        public static T NewPassive<T>(string id, string passiveId, string name, string sprite) where T : BasePassiveAbilitySO
        {
            var pass = CreateScriptable<T>();

            pass.name = id;
            pass.m_PassiveID = passiveId;
            pass._triggerOn = [];

            pass.passiveIcon = LoadSprite(sprite);

            pass._passiveName = name;
            pass.AutoDescription("This passive is not meant for allies");

            return pass;
        }

        public static T AutoDescription<T>(this T pass, string descriptionTemplate) where T : BasePassiveAbilitySO
        {
            pass._characterDescription = descriptionTemplate.DoInserts(false);
            pass._enemyDescription = descriptionTemplate.DoInserts(true);

            return pass;
        }

        public static string DoInserts(this string description, bool enemy)
        {
            for (int i = 0; i < CharacterDescriptionInserts.Length; i++)
            {
                var idx = i;

                if (enemy)
                    idx = i % 2 == 0 ? i + 1 : i - 1;

                if (idx >= CharacterDescriptionInserts.Length)
                    idx = i;

                var orig = (DescriptionInsert)i;
                var rep = CharacterDescriptionInserts[idx];

                description = description.ReplaceAndKeepCase(orig.ToString(), rep);
            }

            return description;
        }

        public static T WithCharacterDescription<T>(this T pass, string description) where T : BasePassiveAbilitySO
        {
            pass._characterDescription = description;

            return pass;
        }

        public static T WithEnemyDescription<T>(this T pass, string description) where T : BasePassiveAbilitySO
        {
            pass._enemyDescription = description;

            return pass;
        }

        public static T AddToGlossary<T>(this T pass, string glossaryDescription) where T: BasePassiveAbilitySO
        {
            LoadedDBsHandler.GlossaryDB.AddNewPassive(new(pass._passiveName, glossaryDescription, pass.passiveIcon));

            return pass;
        }

        public static T WithStoredValue<T>(this T pass, UnitStoreData_BasicSO storedValue) where T : BasePassiveAbilitySO
        {
            pass.specialStoredData = storedValue;

            return pass;
        }

        public static readonly string[] CharacterDescriptionInserts = new string[]
        {
            "party member",
            "enemy",

            "party members",
            "enemies",
        };

        public enum DescriptionInsert
        {
            ally,
            opponent,

            allies,
            opponents
        }
    }
}
