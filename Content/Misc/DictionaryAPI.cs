using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BOStuffPack.Content.Misc
{
    public static class DictionaryAPI
    {
        public static readonly Dictionary<string, List<DictionaryWord>> RealWordCache = [];

        public static List<DictionaryWord> GetWords(string w)
        {
            if (RealWordCache.TryGetValue(w, out var words))
                return words;

            var u = HttpUtility.UrlEncode(w);
            var url = $"https://api.dictionaryapi.dev/api/v2/entries/en/{u}";

            using var client = new HttpClient();
            var task = client.GetStringAsync(url);

            while (!task.IsCompleted) { }

            words = (task.Status == TaskStatus.RanToCompletion) ? JsonConvert.DeserializeObject<List<DictionaryWord>>(task.Result) : [];
            return RealWordCache[w] = words;
        }

        public static IEnumerator LoadDictionaryWords(string w)
        {
            if (RealWordCache.ContainsKey(w))
                yield break;

            var u = HttpUtility.UrlEncode(w);
            var url = $"https://api.dictionaryapi.dev/api/v2/entries/en/{u}";

            using var client = new HttpClient();
            var task = client.GetStringAsync(url);

            while (!task.IsCompleted)
                yield return null;

            var words = (task.Status == TaskStatus.RanToCompletion) ? JsonConvert.DeserializeObject<List<DictionaryWord>>(task.Result) : [];
            RealWordCache[w] = null;
        }

        public class DictionaryWord
        {
            public string word;
            public string phonetic;
            public List<Phonetic> phonetics;
            public List<Meaning> meanings;
            public License license;
            public List<string> sourceUrls;

            public class Phonetic
            {
                public string text;
                public string audio;
                public string sourceUrl;
                public License license;
            }

            public class License
            {
                public string name;
                public string url;
            }

            public class Meaning
            {
                public string partOfSpeech;
                public List<Definition> definitions;
                public List<string> synonyms;
                public List<string> antonyms;
            }

            public class Definition
            {
                public string definition;
                public List<string> synonyms;
                public List<string> antonyms;
                public string example;
            }
        }
    }
}
