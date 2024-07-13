using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class TextTools
    {
        public static string ToId(this string orig)
        {
            return Regex.Replace(orig, @"[^a-zA-Z0-9_]", "");
        }

        public static string Colorize(this string txt, Color c)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(c)}>{txt}</color>";
        }

        public static string Italic(this string txt)
        {
            return $"<i>{txt}</i>";
        }

        public static string Scale(this string txt, int scalePercent)
        {
            return $"<size={scalePercent}%>{txt}</size>";
        }

        public static string Subscript(this string txt)
        {
            return $"<sub>{txt}</sub>";
        }

        public static string Superscript(this string txt)
        {
            return $"<sup>{txt}</sup>";
        }

        public static string Strikethrough(this string txt)
        {
            return $"<s>{txt}</s>";
        }

        public static string Underline(this string txt)
        {
            return $"<u>{txt}</u>";
        }

        public static string NoBreak(this string txt)
        {
            return $"<nobr>{txt}</nobr>";
        }

        public static string VerticalOffset(this string txt, float offsetUnits)
        {
            return $"<voffset={offsetUnits}em>{txt}</voffset>";
        }

        public static string Align(this string txt, TextAlignment alignment)
        {
            return $"<align=\"{alignment.ToString().ToLowerInvariant()}\">{txt}</align>";
        }

        public static string ReplaceAndKeepCase(this string input, string orig, string replacement)
        {
            var pattern = $@"\b{orig}\b";

            var match = Regex.Matches(input, pattern, RegexOptions.IgnoreCase);
            var idxOffs = 0;

            var idxDiff = replacement.Length - orig.Length;

            foreach (Match m in match)
            {
                var rep = GetReplacement(m.Value, replacement);
                var part2Start = m.Index + m.Length + idxOffs;

                input = $"{input.Substring(0, m.Index + idxOffs)}{rep}{(part2Start >= input.Length ? "" : input.Substring(part2Start))}";
                idxOffs += idxDiff;
            }

            return input;
        }

        public static string GetReplacement(string match, string replacement)
        {
            if (match == match.ToLowerInvariant())
                return replacement.ToLowerInvariant();

            if (match == match.ToUpperInvariant())
                return replacement.ToUpperInvariant();

            if (match == match.ToFirstWordTitleInvariant())
                return replacement.ToFirstWordTitleInvariant();

            return replacement;
        }

        public static string ToFirstWordTitleInvariant(this string input)
        {
            var sp = input.Split(' ');

            for (int i = 0; i < sp.Length; i++)
            {
                sp[i] = i == 0 ? sp[i].ToTitleInvariant() : sp[i].ToLowerInvariant();
            }

            return string.Join(" ", sp);
        }

        public static string ToTitleInvariant(this string input)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input);
        }

        public static string TargetString(int offset)
        {
            var str = "";

            for (var i = 1; i < Mathf.Abs(offset); i++)
                str += "Far ";

            if (offset > 0)
                str += "Right";

            else if (offset < 0)
                str += "Left";

            else
                str = "Opposing";

            return str;
        }
    }
}
