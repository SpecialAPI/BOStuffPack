using BOStuffPack.DynamicAppearances;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class BookmarkDynamicAppearance(string seDataKey, string svDataKey) : DynamicItemAppearanceBase
    {
        public static readonly string SavedStatusString = "Saved status effects: {0}.".Colorize(new Color32(0, 139, 255, 255));
        public static readonly string SavedSVString = "{0} saved stored values.".Colorize(new Color32(0, 139, 255, 255));

        public override void ModifyItemDescription(ref string description)
        {
            if (string.IsNullOrEmpty(seDataKey))
                return;

            var infoHolder = LoadedDBsHandler.InfoHolder;
            if (infoHolder == null)
                return;

            if (infoHolder.Run == null || infoHolder.Run.InGameData is not IInGameRunData dat)
                return;

            var seString = GetStatusString(dat);
            if (!string.IsNullOrEmpty(seString))
            {
                if(!description.EndsWith("\n"))
                    description += "\n";
                description += seString;
            }

            var svString = GetSVString(dat);
            if (!string.IsNullOrEmpty(svString))
            {
                if(!description.EndsWith("\n"))
                    description += "\n";
                description += svString;
            }
        }

        public string GetSVString(IInGameRunData dat)
        {
            var savedValues = dat.GetStringData(svDataKey);
            if (string.IsNullOrEmpty(savedValues))
                return string.Empty;

            var svData = savedValues.Split(['|'], StringSplitOptions.RemoveEmptyEntries);
            var validSVCounter = 0;
            foreach (var sv in svData)
            {
                var commaIdx = sv.IndexOf(',');
                if (commaIdx < 0)
                    continue;

                var svID = sv.Substring(0, commaIdx);
                var amtString = sv.Substring(commaIdx + 1);

                if (!LoadedDBsHandler.MiscDB.m_UnitStoreDataPool.ContainsKey(svID) || !int.TryParse(amtString, out var amt))
                    continue;

                validSVCounter++;
            }

            if (validSVCounter <= 0)
                return string.Empty;

            return string.Format(SavedSVString, validSVCounter);
        }

        public string GetStatusString(IInGameRunData dat)
        {
            var savedStatuses = dat.GetStringData(seDataKey);
            if (string.IsNullOrEmpty(savedStatuses))
                return string.Empty;

            var statusData = savedStatuses.Split(['|'], StringSplitOptions.RemoveEmptyEntries);
            var statusTexts = new List<string>();
            foreach (var stData in statusData)
            {
                var commaIdx = stData.IndexOf(',');
                if (commaIdx < 0)
                    continue;

                var statusId = stData.Substring(0, commaIdx);
                var amtString = stData.Substring(commaIdx + 1);

                if (!LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(statusId, out var seSO) || !int.TryParse(amtString, out var amt))
                    continue;

                var statusName = seSO.EffectInfo.GetStatusLocData().text;

                // EXTREMELY EVIL WORKAROUND
                var tempHolder = seSO.GenerateHolder(amt, 0);
                var displayText = seSO.DisplayText(tempHolder);

                var fullText = !string.IsNullOrEmpty(displayText) ?
                    $"{displayText} {statusName}" :
                    statusName;

                statusTexts.Add(fullText);
            }

            if (statusTexts.Count <= 0)
                return string.Empty;

            return string.Format(SavedStatusString, string.Join(", ", statusTexts));
        }
    }
}
