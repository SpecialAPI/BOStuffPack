using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class RangeTools
    {
        public static bool IsInRange(this int value, int? min, int? max)
        {
            if (min is int min_ && value < min_)
                return false;

            if (max is int max_ && value > max_)
                return false;

            return true;
        }

        public static bool IsInRange(this int value, (int? min, int? max) range)
        {
            return value.IsInRange(range.min, range.max);
        }

        public static bool IsInRange(this int value, (int min, int max) range)
        {
            return value.IsInRange(range.min, range.max);
        }
    }


}
