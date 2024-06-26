using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.RichText
{
    public abstract class RichTextProcessorBase
    {
        public abstract string ProcessReplacements(string orig);
    }
}
