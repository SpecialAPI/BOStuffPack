using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger.Args
{
    public class OnPassivePopupReference(string name, Sprite sprite)
    {
        public readonly string localizedPassiveName = name;
        public readonly Sprite passiveIcon = sprite;
    }
}
