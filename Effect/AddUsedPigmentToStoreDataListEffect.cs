using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Effect
{
    public class AddUsedPigmentToStoreDataListEffect : AddUsedPigmentToStoreDataCollectionEffect<List<ManaColorSO>>
    {
        public static EffectSO Create(string storedValue) => Create<AddUsedPigmentToStoreDataListEffect>(storedValue);
    }
}
