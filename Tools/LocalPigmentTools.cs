using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class LocalPigmentTools
    {
        public static List<ManaColorSO> GetComponentPigments(this ManaColorSO pigment)
        {
            if (pigment.pigmentTypes.Count <= 1)
                return [pigment];

            var comps = new List<ManaColorSO>();
            foreach (var type in pigment.pigmentTypes)
            {
                if (!LoadedDBsHandler.PigmentDB.PigmentPool.TryGetValue(type, out var comp))
                    continue;

                comps.Add(comp);
            }

            if (comps.Count == 0)
                comps.Add(pigment);

            return comps;
        }
    }
}
