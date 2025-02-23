using BOStuffPack.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.TriggerEffects
{
    public class PickyEaterBonusDamageSetterTriggerEffect : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, TriggerEffectExtraInfo extraInfo)
        {
            if (args is not DamageDealtValueChangeException ex || ex.damagedUnit is not IUnit u)
                return;

            string name;

            if (u is CharacterCombat cc)
                name = cc.Character._characterName;
            else if (u is EnemyCombat ec)
                name = ec.Enemy._enemyName;
            else
                return;

            ex.AddModifier(new PickyEaterIntValueModifier(name));
        }
    }

    public class PickyEaterIntValueModifier(string enemyName) : IntValueModifier(11)
    {
        internal static readonly Func<string, bool>[] PickyEaters = new Func<string, bool>[]
        {
            BrownPickyEater,
            BluePickyEater,
            GreenPickyEater,
            YellowPickyEater,
            OrangePickyEater,
            RedPickyEater
        };

        public override int Modify(int value)
        {
            var satisfied = GetSatisfiedPickyEaters(enemyName);

            if (satisfied <= 0)
                return value;

            return value + Mathf.Max(1, Mathf.FloorToInt(value * satisfied * 0.1f));
        }

        public static int GetSatisfiedPickyEaters(string name)
        {
            var words = name.Split(' ');
            var pickyEatersSatisfied = new int[PickyEaters.Length];

            foreach(var w in words)
            {
                for(var i = 0; i < pickyEatersSatisfied.Length; i++)
                {
                    if (pickyEatersSatisfied[i] == 1)
                        continue;

                    if (!(PickyEaters[i]?.Invoke(w) ?? false))
                        continue;

                    pickyEatersSatisfied[i] = 1;
                }
            }

            return pickyEatersSatisfied.Sum();
        }

        public static bool BrownPickyEater(string word)
        {
            const string CONSONANTS = "bcdfgjklmnpqstvxzhrwy";
            var numConsonants = word.ToLowerInvariant().Count(x => CONSONANTS.Contains(x));

            return numConsonants % 2 == 1;
        }

        public static bool BluePickyEater(string word)
        {
            return word.ToLowerInvariant().Contains('n');
        }

        public static bool GreenPickyEater(string word)
        {
            return DictionaryAPI.GetWords(word).Count > 0;
        }

        public static bool YellowPickyEater(string word)
        {
            if(word.Length < 2)
                return false;

            const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";

            for(var i = 0; i < word.Length - 1; i++)
            {
                var nextCh = word[i + 1];
                var nextIdx = ALPHABET.IndexOf(nextCh);

                if(nextIdx < 0)
                {
                    i++;
                    continue;
                }

                var thisCh = word[i];
                var thisIdx = ALPHABET.IndexOf(thisCh);

                if (thisIdx < 0)
                    continue;

                if (nextIdx != thisIdx + 1)
                    continue;

                return true;
            }

            return false;
        }

        public static bool OrangePickyEater(string word)
        {
            return word.Length is 6 or 7;
        }

        public static bool RedPickyEater(string word)
        {
            if(word.Length < 2)
                return false;

            const string VOWELS = "aeiou";
            var found = "";

            foreach(var c in word)
            {
                if (!VOWELS.Contains(c))
                    continue;

                if (found.Contains(c))
                    return true;

                found += c;
            }

            return false;
        }
    }
}
