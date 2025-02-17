using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Items.Shop
{
    public static class Eyepatch
    {
        public static void Init()
        {
            //var name = "Eyepatch";
            //var flav = "\"Hit the same less often\"";
            //var desc = "This party member has TargetShift (Left) as a passive. Adds \"Retarget\" as an additional ability, an ability that allows this party member to change their TargetShift.";
            //
            //var abilityName = "Retarget";
            //var abilityDesc = "If this party member's TargetShift is directed to the left, set TagetShift to Right. Otherwise, set TargetShift to Left.\n75% chance to refresh this party member.";
            //
            //var ability =
            //    NewAbility(abilityName, abilityDesc, "AttackIcon_Retarget", new()
            //    {
            //        Effect(null, CreateScriptable<CasterStoreValueCheckOverThresholdEffect>(x => { x.m_Threshold = 0; x.m_unitStoredDataID = StoredValue_TargetShift.name; })),
            //
            //        Effect(null, CreateScriptable<CasterStoreValueSetterEffect>(x => x.m_unitStoredDataID = StoredValue_TargetShift.name), 1).WithCondition(Previous(1, false)),
            //        Effect(null, CreateScriptable<CasterStoreValueSetterEffect>(x => x.m_unitStoredDataID = StoredValue_TargetShift.name), -1).WithCondition(Previous(2, true)),
            //
            //        Effect(Self, CreateScriptable<RefreshAbilityUseEffect>()).WithCondition(Chance(75))
            //    },
            //    new()
            //    {
            //        TargetIntent(Self, IntentType_GameIDs.Misc.ToString(), IntentType_GameIDs.Misc.ToString())
            //    })
            //
            //    .WithVisuals(Visuals_Slap, Self)
            //
            //    .WithFootnotes("This ability is not affected by TargetShift")
            //    .WithFlags("Ignore_TargetShift")
            //
            //    .Character(Pigments.Yellow);
            //
            //var item = NewItem<BasicWearable>(name, flav, desc, "Eyepatch").AddModifiers(ExtraPassive(TargetShiftGenerator(-1)), ExtraAbility(ability)).AddToShop(4).Build();
        }
    }
}
