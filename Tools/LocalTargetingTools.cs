using BOStuffPack.Content.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Tools
{
    public static class LocalTargetingTools
    {
        public static BaseCombatTargettingSO FilterUnit(this BaseCombatTargettingSO orig, Func<IUnit, bool> filter)
        {
            var f = CreateScriptable<UnitFilterTargeting>();
            f.orig = orig;
            f.filter = filter;

            return f;
        }

        public static BaseCombatTargettingSO FilterByHealth(this BaseCombatTargettingSO orig, bool getWeakest, bool ignoreDead = true)
        {
            var f = CreateScriptable<FilterByHealthTargeting>();
            f.orig = orig;
            f.getWeakest = getWeakest;
            f.ignoreDead = ignoreDead;

            return f;
        }

        public static BaseCombatTargettingSO Join(this BaseCombatTargettingSO a, BaseCombatTargettingSO b, bool? areTargetAllies, bool? areTargetSlots)
        {
            JoinTargeting j;

            if(a is JoinTargeting aj)
            {
                if (b is JoinTargeting bj)
                    aj.targeting.AddRange(bj.targeting);
                else
                    aj.targeting.Add(b);

                j = aj;

                return aj;
            }
            else if(b is JoinTargeting bj)
            {
                bj.targeting.Insert(0, a);

                j = bj;
            }
            else
            {
                j = CreateScriptable<JoinTargeting>();

                j.targeting.Add(a);
                j.areTargetAllies = a.AreTargetAllies;
                j.areTargetSlots = a.AreTargetSlots;

                j.targeting.Add(b);
            }

            if (areTargetAllies is bool ata)
                j.areTargetAllies = ata;
            if (areTargetSlots is bool ats)
                j.areTargetSlots = ats;

            return j;
        }
    }
}
