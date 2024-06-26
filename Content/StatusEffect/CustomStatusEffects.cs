using BOStuffPack.Content.StatusEffect.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.StatusEffect
{
    public static class CustomStatusEffects
    {
        public static StatusEffect_SO Fury;
        public static StatusEffect_SO Survive;
        public static StatusEffect_SO Weakened;

        public static void Init()
        {
            Fury = NewStatus<FuryStatusEffect>("Fury_SE", "Fury_ID", "Fury", "When performing an ability, perform it again and reduce Fury by 1 for each point of Fury.\n1 point of Fury is lost at the end of each turn.", "Fury", "event:/FuryApply").AddToGlossary().WithTutorial("FuryTutorial", "FuryTutorial_Characters", "FuryTutorial_Enemies");

            Survive = NewStatus<SurviveStatusEffect>("Survive_SE", "Survive_ID", "Survive", "Survive 1 fatal hit for each point of Survive.", "Survive", "event:/Combat/StatusEffects/SE_Divine_Apl").AddToGlossary().WithTutorial("SurviveTutorial", "SurviveTutorial_Characters", "SurviveTutorial_Enemies");

            Weakened = NewStatus<WeakenedStatusEffect>("Weakened_SE", "Weakened_ID", "Weakened", "Weakened party members are 1 level lower than they would be otherwise for each point of Weakened.\nDamage dealt by Weakened enemies is multiplied by 0.85 for each point of Weakened.\n1 point of Weakened is lost at the end of each turn.", "Weaken", "event:/WeakenedApply").AddToGlossary().WithTutorial("WeakenedTutorial", "WeakenedTutorial_Characters", "WeakenedTutorial_Enemies");
        }
    }
}
