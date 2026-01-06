using BOStuffPack.DynamicAppearances;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class UnnamedItem18DynamicAppearance(string dataKey) : DynamicItemAppearanceBase
    {
        public const string SavedStatusString = "Saved status effects: {0}";

        public override void ModifyItemDescription(ref string description)
        {
            if (string.IsNullOrEmpty(dataKey))
                return;

            var infoHolder = LoadedDBsHandler.InfoHolder;
            if (infoHolder == null)
                return;

            if (infoHolder.Run == null || infoHolder.Run.InGameData is not IInGameRunData dat)
                return;

            var savedStatuses = dat.GetStringData(dataKey);
            if (string.IsNullOrEmpty(savedStatuses))
                return;

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
                return;

            if (!description.EndsWith("\n"))
                description += "\n";

            description += string.Format(SavedStatusString, string.Join(", ", statusTexts)).Colorize(new Color32(0, 139, 255, 255));
        }
    }
}
