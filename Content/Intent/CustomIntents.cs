using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Intent
{
    public static class CustomIntents
    {
        public const string INTENT_FURY_APPLY = "Fury_Apply";
        public const string INTENT_FURY_REMOVE = "Fury_Remove";

        public static void Init()
        {
            AddBasicIntent(INTENT_FURY_APPLY, "Fury");
            AddBasicIntent(INTENT_FURY_REMOVE, "Fury", StatusRemove_IntentColor);
        }
    }
}
