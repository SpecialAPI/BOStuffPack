using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class SetToValueValueModifier(int val) : IntValueModifier(10)
    {
        public override int Modify(int value)
        {
            return val;
        }
    }
}
