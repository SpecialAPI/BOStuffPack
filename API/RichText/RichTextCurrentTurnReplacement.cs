using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.RichText
{
    public class RichTextCurrentTurnReplacement : RichTextProcessorBase
    {
        public override string ProcessReplacements(string orig)
        {
            return orig.Replace("{CurrentTurn}", (CombatManager.Instance._stats.TurnsPassed + 1).ToString());
        }
    }
}
