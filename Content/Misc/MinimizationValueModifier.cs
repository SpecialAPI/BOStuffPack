using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    public class MinimizationValueModifier(bool damageDealt, int min) : IntValueModifier(damageDealt ? 2 : 61)
    {
        public int min = min;

        public override int Modify(int value)
        {
            return Mathf.Min(value, min);
        }
    }
}
