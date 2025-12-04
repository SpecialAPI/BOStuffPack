using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class StringDataSetterPostCombatResult(string dataKey, string value) : ModdedPostCombatResult
    {
        public override void ApplyPostCombatResult(GameInformationHolder info)
        {
            if (info.Run == null || info.Run.inGameData is not RunInGameData dat)
                return;

            dat.SetStringData(dataKey, value);
        }
    }
}
