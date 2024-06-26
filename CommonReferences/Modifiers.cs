using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CommonReferences
{
    public static class Modifiers
    {
        public static WearableStaticModifierSetterSO ExtraAbility(CharacterAbility ab)
        {
            return CreateScriptable<ExtraAbility_Wearable_SMS>(x => x._extraAbility = ab);
        }

        public static WearableStaticModifierSetterSO BasicAbility(CharacterAbility ab)
        {
            return CreateScriptable<BasicAbilityChange_Wearable_SMS>(x => x._basicAbility = ab);
        }

        public static WearableStaticModifierSetterSO ExtraPassive(BasePassiveAbilitySO passive)
        {
            return CreateScriptable<ExtraPassiveAbility_Wearable_SMS>(x => x._extraPassiveAbility = passive);
        }

        public static WearableStaticModifierSetterSO ModdedData(string id, ItemModifierDataSetter dat)
        {
            return CreateScriptable<ModdedDataSetter_Wearable_SMS>(x =>
            {
                x.m_ModdedDataID = id;
                x.m_ModdedData = dat;
            });
        }
    }
}
