using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class IntentTools
    {
        public static Color StatusRemove_IntentColor = new(0.3529f, 0.3529f, 0.3529f);

        public static IntentTargetInfo TargetIntent(BaseCombatTargettingSO targets, params string[] intents) => new() { intents = intents, targets = targets };

        public static string IntentForDamage(int damage)
        {
            if (damage <= 2)
                return IntentType_GameIDs.Damage_1_2.ToString();

            else if (damage <= 6)
                return IntentType_GameIDs.Damage_3_6.ToString();

            else if (damage <= 10)
                return IntentType_GameIDs.Damage_7_10.ToString();

            else if (damage <= 15)
                return IntentType_GameIDs.Damage_11_15.ToString();

            else if (damage <= 20)
                return IntentType_GameIDs.Damage_16_20.ToString();

            return IntentType_GameIDs.Damage_21.ToString();
        }

        public static void AddBasicIntent(string name, string sprite, Color? c = null)
        {
            Intents.AddCustom_Basic_IntentToPool(name, new()
            {
                id = name,
                _color = c ?? Color.white,
                _sprite = LoadSprite(sprite)
            });
        }
    }
}
