using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class LateMaximizationValueModifier(int a) : IntValueModifier(1000)
    {
        public override int Modify(int value)
        {
            return Mathf.Max(value, a);
        }
    }
}
