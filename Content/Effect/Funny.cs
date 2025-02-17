using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine.Rendering.PostProcessing;

namespace BOStuffPack.Content.Effect
{
    public class Funny : EffectSO
    {
        public float minMult = 0f;
        public float maxMult = 2f;

        public float minMaxValue = 20f;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            CombatManager.Instance.AddUIAction(new Hilarious(minMult, maxMult, minMaxValue));
            exitAmount = PreviousExitValue;

            return true;
        }
    }

    public class Hilarious(float minMult = 0f, float maxMult = 2f, float minMaxValue = 20f) : CombatAction
    {
        public static FieldInfo s = AccessTools.Field(typeof(PostProcessEffectSettings), "parameters");

        public float GetNewValue(float old)
        {
            return Random.Range(minMult * old, Mathf.Max(minMaxValue, maxMult * old));
        }

        public override IEnumerator Execute(CombatStats stats)
        {
            var layer = 1 << LayerMask.NameToLayer("PostProcessing");
            var cams = Camera.allCameras;

            if (cams != null && Plugin.PostProcessResources != null)
            {
                foreach (var cam in cams)
                {
                    if (cam != null && cam.gameObject != null && cam.GetComponent<PostProcessLayer>() == null)
                    {
                        var pplayer = cam.gameObject.AddComponent<PostProcessLayer>();
                        pplayer.Init(Plugin.PostProcessResources);
                        pplayer.volumeLayer = layer;
                    }
                }
            }

            var vol = PostProcessManager.instance.GetHighestPriorityVolume(layer);

            if (vol == null || vol.profile == null)
                yield break;

            var profile = vol.profile;

            var selectedSettings = profile.settings.Where(x => x is not Vignette && x is not Grain);
            var param = selectedSettings.SelectMany(x => s.GetValue(x) as ReadOnlyCollection<ParameterOverride>);

            var ints = param.OfType<IntParameter>();
            var floats = param.OfType<FloatParameter>();

            var intInterps = new List<(IntParameter param, int origVal, int newVal)>();
            var floatInterps = new List<(FloatParameter param, float origVal, float newVal)>();

            foreach (var f in ints)
            {
                var newVal = Mathf.RoundToInt(GetNewValue(f.value));

                intInterps.Add((f, f.value, newVal));
            }

            foreach (var f in floats)
            {
                var newVal = GetNewValue(f.value);

                floatInterps.Add((f, f.value, newVal));
            }

            if (intInterps.Count <= 0 && floatInterps.Count <= 0)
                yield break;

            var interpTime = 0.5f;

            for (var i = 0f; i < interpTime; i += Time.deltaTime)
            {
                foreach (var (p, origVal, newVal) in intInterps)
                    p.value = Mathf.RoundToInt(Mathf.Lerp(origVal, newVal, i / interpTime));

                foreach (var (p, origVal, newVal) in floatInterps)
                    p.value = Mathf.Lerp(origVal, newVal, i / interpTime);

                yield return null;
            }

            foreach (var (p, origVal, newVal) in intInterps)
                p.value = newVal;

            foreach (var (p, origVal, newVal) in floatInterps)
                p.value = newVal;
        }
    }
}
