using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class StatusEffectTools
    {
        public static T NewStatus<T>(string id, string statusId, string name, string description, string sprite, string appliedEvent = "event:/UI/Combat/Status/UI_CBT_STS_Update", string removedEvent = "event:/UI/Combat/Status/UI_CBT_STS_Remove", string updatedEvent = "event:/UI/Combat/Status/UI_CBT_STS_Update") where T : StatusEffect_SO
        {
            var st = CreateScriptable<T>();

            st.name = id;
            st._StatusID = statusId;

            st._EffectInfo = CreateScriptable<StatusEffectInfoSO>();

            st._EffectInfo._statusName = name;
            st._EffectInfo._description = description;

            st._EffectInfo._applied_SE_Event = appliedEvent;
            st._EffectInfo._updated_SE_Event = updatedEvent;
            st._EffectInfo._removed_SE_Event = removedEvent;

            st._EffectInfo.icon = LoadSprite(sprite);

            AddBasicIntent(IntentForStatus<T>(), sprite);
            AddBasicIntent(IntentForStatusRemove<T>(), sprite, StatusRemove_IntentColor);

            StatusField.AddNewStatusEffect(st, false);

            return st;
        }

        public static string IntentForStatus<T>() where T : StatusEffect_SO
        {
            return $"Status_{typeof(T).Name}";
        }

        public static string IntentForStatusRemove<T>() where T : StatusEffect_SO
        {
            return $"Rem_Status_{typeof(T).Name}";
        }

        public static T AddToGlossary<T>(this T st) where T : StatusEffect_SO
        {
            LoadedDBsHandler.GlossaryDB.AddNewStatusID(st.EffectInfo);

            return st;
        }

        public static T WithTutorial<T>(this T st, string tutorial, string characterStart, string enemyStart = "") where T : StatusEffect_SO
        {
            Dialogues.AddCustom_DialogueProgram(tutorial, Bundle.LoadAsset<YarnProgram>(tutorial));
            Tutorials.CreateAndAddCustom_StatusTutorial(st, tutorial, characterStart, enemyStart);

            return st;
        }
    }
}
