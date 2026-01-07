using BOStuffPack.Content.Items;
using BOStuffPack.DynamicAppearances;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class WrittenBookDynamicAppearance(string abilityDataKey, string passiveDataKey) : DynamicItemAppearanceBase
    {
        public static readonly string ExtraAbilityString = "This party member has \"{0}\" as an additional ability.".Colorize(new Color32(0, 139, 255, 255));
        public static readonly string ExtraPassiveString = "This party member has {0} as a passive.".Colorize(new Color32(0, 139, 255, 255));

        public override void ModifyItemDescription(ref string description)
        {
            if (string.IsNullOrEmpty(abilityDataKey) || string.IsNullOrEmpty(passiveDataKey))
                return;

            var infoHolder = LoadedDBsHandler.InfoHolder;
            if (infoHolder == null)
                return;

            if (infoHolder.Run == null || infoHolder.Run.InGameData is not IInGameRunData dat)
                return;

            var d = new List<string>();

            if (WrittenBook.DeserializeAbility(dat.GetStringData(abilityDataKey), out var ab))
                d.Add(string.Format(ExtraAbilityString, ab.ability.GetAbilityLocData().text));
            if (WrittenBook.DeserializePassive(dat.GetStringData(passiveDataKey), out var pa))
                d.Add(string.Format(ExtraPassiveString, pa.GetPassiveLocData().text));

            if (d.Count == 0)
                return;

            description = string.Join(" ", d);
        }
    }
}
