using FMODUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class ResourceLoader
    {
        public static Assembly ModAssembly;

        public static Texture2D LoadTexture(string name)
        {
            if (TryReadFromResource(name.TryAddExtension("png"), out var ba))
            {
                var tex = new Texture2D(1, 1);

                tex.LoadImage(ba);
                tex.filterMode = FilterMode.Point;

                return tex;
            }
            return null;
        }

        public static Sprite LoadSprite(string name, Vector2? pivot = null, int pixelsperunit = 32)
        {
            var tex = LoadTexture(name);

            if (tex != null)
                return Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), pivot ?? new Vector2(0.5f, 0.5f), pixelsperunit);

            return null;
        }

        public static bool TryReadFromResource(string resname, out byte[] ba)
        {
            var name = ModAssembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith($".{resname}"));

            if (!string.IsNullOrEmpty(name))
            {
                using var strem = ModAssembly.GetManifestResourceStream(name);

                ba = new byte[strem.Length];
                strem.Read(ba, 0, ba.Length);

                return true;
            }

            Debug.LogError($"Couldn't load from resource name {resname}, returning an empty byte array.");
            ba = new byte[0];

            return false;
        }

        public static string TryAddExtension(this string n, string e)
        {
            if (n.EndsWith($".{e}"))
            {
                return n;
            }
            return n + $".{e}";
        }

        public static void LoadFMODBankFromResource(string resname, bool loadSamples = false)
        {
            if (TryReadFromResource(resname.TryAddExtension("bank"), out var ba))
            {
                LoadFMODBankFromBytes(ba, resname, loadSamples);
            }
        }

        public static void LoadFMODBankFromBytes(byte[] ba, string bankName, bool loadSamples = false)
        {
            if (RuntimeManager.Instance.loadedBanks.TryGetValue(bankName, out var bnk))
            {
                bnk.RefCount++;

                if (loadSamples)
                    bnk.Bank.loadSampleData();

                return;
            }

            var bn = default(RuntimeManager.LoadedBank);
            var res = RuntimeManager.Instance.studioSystem.loadBankMemory(ba, FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out bn.Bank);

            if(res == FMOD.RESULT.OK)
            {
                bn.RefCount = 1;

                RuntimeManager.Instance.loadedBanks.Add(bankName, bn);

                if (loadSamples)
                    bn.Bank.loadSampleData();
            }

            else if(res == FMOD.RESULT.ERR_ALREADY_LOCKED)
            {
                bn.RefCount = 2;

                RuntimeManager.Instance.loadedBanks.Add(bankName, bn);
            }

            else
                throw new BankLoadException(bankName, res);
        }
    }
}
