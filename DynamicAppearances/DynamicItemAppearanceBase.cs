using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.DynamicAppearances
{
    public abstract class DynamicItemAppearanceBase
    {
        public virtual void ModifyItemDescription(ref string description)
        {
        }
    }
}
