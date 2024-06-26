using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UIElements.StyleSheets.Dimension;

namespace BOStuffPack.API.CustomEvent
{
    public static class CustomEvents
    {
        public const string ON_ABILITY_PERFORMED_CONTEXT = "AbilityPerformedContext";
        public const string ON_ABILITY_FINISHED = "AbilityFinished";
        public const string ON_BEFORE_ABILITY_EFFECTS = "BeforeAbilityEffects";
        public const string CAN_PRODUCE_PIGMENT_COLOR = "CanProducePigmentColor";
        public const string MODIFY_WRONG_PIGMENT_AMOUNT = "ModifyWrongPigmentAmount";
        public const string MODIFY_ABILITY_RANK = "ModifyAbilityRank";
    }

    [HarmonyPatch]
    public static class EventPatches
    {
        public static MethodInfo apc_c_te = AccessTools.Method(typeof(EventPatches), nameof(AbilityPerformedContext_Character_TriggerEvent));
        public static MethodInfo apc_e_te = AccessTools.Method(typeof(EventPatches), nameof(AbilityPerformedContext_Enemy_TriggerEvent));

        public static MethodInfo bae_c_te = AccessTools.Method(typeof(EventPatches), nameof(BeforeAbilitEffects_Character_TriggerEvent));
        public static MethodInfo bae_e_te = AccessTools.Method(typeof(EventPatches), nameof(BeforeAbilitEffects_Enemy_TriggerEvent));

        public static MethodInfo cppc_ama_te = AccessTools.Method(typeof(EventPatches), nameof(CanProducePigmentColor_AddManaAction_TriggerEvent));
        public static MethodInfo cppc_hcc_te = AccessTools.Method(typeof(EventPatches), nameof(CanProducePigmentColor_HealthColorCondition_TriggerEvent));

        public static MethodInfo mwpa_ui_te = AccessTools.Method(typeof(EventPatches), nameof(ModifyWrongPigmentAmount_UI_TriggerEvent));
        public static MethodInfo mwpa_c_te = AccessTools.Method(typeof(EventPatches), nameof(ModifyWrongPigmentAmount_Character_TriggerEvent));

        public static MethodInfo mar_te = AccessTools.Method(typeof(EventPatches), nameof(ModifyAbilityRank_TriggerEvent));

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.UseAbility))]
        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.UseAbility))]
        [HarmonyILManipulator]
        public static void AbilityPerformedContext_Transpiler(ILContext ctx, MethodBase mthd)
        {
            var crs = new ILCursor(ctx);

            foreach(var m in crs.MatchAfter(x => x.MatchNewobj<EndAbilityAction>()))
            {
                crs.Emit(OpCodes.Ldarg_0);
                crs.Emit(OpCodes.Ldarg_1);
                crs.Emit(OpCodes.Ldloc_0);

                if (mthd.DeclaringType == typeof(CharacterCombat))
                {
                    crs.Emit(OpCodes.Ldarg_2);
                    crs.Emit(OpCodes.Call, apc_c_te);
                }

                else
                {
                    crs.Emit(OpCodes.Call, apc_e_te);
                }
            }
        }

        public static EndAbilityAction AbilityPerformedContext_Character_TriggerEvent(EndAbilityAction curr, CharacterCombat ch, int abilityIdx, AbilitySO ability, FilledManaCost[] cost)
        {
            CombatManager.Instance.AddRootAction(new AbilityContextNotifyAction(ch, abilityIdx, ability, cost, CustomEvents.ON_ABILITY_FINISHED));
            CombatManager.Instance.AddRootAction(new AbilityContextNotifyAction(ch, abilityIdx, ability, cost, CustomEvents.ON_ABILITY_PERFORMED_CONTEXT));

            return curr;
        }

        public static EndAbilityAction AbilityPerformedContext_Enemy_TriggerEvent(EndAbilityAction curr, EnemyCombat en, int abilityIdx, AbilitySO ability)
        {
            CombatManager.Instance.AddRootAction(new AbilityContextNotifyAction(en, abilityIdx, ability, null, CustomEvents.ON_ABILITY_FINISHED));
            CombatManager.Instance.AddRootAction(new AbilityContextNotifyAction(en, abilityIdx, ability, null, CustomEvents.ON_ABILITY_PERFORMED_CONTEXT));

            return curr;
        }

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.UseAbility))]
        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.UseAbility))]
        [HarmonyILManipulator]
        public static void BeforeAbilityEffects_Transpiler(ILContext ctx, MethodBase mthd)
        {
            var crs = new ILCursor(ctx);

            foreach (var m in crs.MatchAfter(x => x.MatchNewobj<EffectAction>()))
            {
                crs.Emit(OpCodes.Ldarg_0);
                crs.Emit(OpCodes.Ldarg_1);
                crs.Emit(OpCodes.Ldloc_0);

                if (mthd.DeclaringType == typeof(CharacterCombat))
                {
                    crs.Emit(OpCodes.Ldarg_2);
                    crs.Emit(OpCodes.Call, bae_c_te);
                }

                else
                {
                    crs.Emit(OpCodes.Call, bae_e_te);
                }
            }
        }

        public static EffectAction BeforeAbilitEffects_Character_TriggerEvent(EffectAction curr, CharacterCombat ch, int abilityIdx, AbilitySO ability, FilledManaCost[] cost)
        {
            CombatManager.Instance.AddRootAction(new AbilityContextNotifyAction(ch, abilityIdx, ability, cost, CustomEvents.ON_BEFORE_ABILITY_EFFECTS));

            return curr;
        }

        public static EffectAction BeforeAbilitEffects_Enemy_TriggerEvent(EffectAction curr, EnemyCombat en, int abilityIdx, AbilitySO ability)
        {
            CombatManager.Instance.AddRootAction(new AbilityContextNotifyAction(en, abilityIdx, ability, null, CustomEvents.ON_BEFORE_ABILITY_EFFECTS));

            return curr;
        }

        [HarmonyPatch(typeof(AddManaToManaBarAction), nameof(AddManaToManaBarAction.Execute))]
        [HarmonyPatch(typeof(HealthColorDetectionEffectorCondition), nameof(HealthColorDetectionEffectorCondition.MeetCondition))]
        [HarmonyILManipulator]
        public static void CanProducePigmentColor_Transpiler(ILContext ctx, MethodBase mthd)
        {
            var crs = new ILCursor(ctx);

            foreach(var m in crs.MatchAfter(x => x.MatchLdfld<ManaColorSO>(nameof(ManaColorSO.canGenerateMana))))
            {
                if(mthd.DeclaringType == typeof(AddManaToManaBarAction))
                {
                    crs.Emit(OpCodes.Ldarg_0);
                    crs.Emit(OpCodes.Call, cppc_ama_te);
                }

                else
                {
                    crs.Emit(OpCodes.Ldloc_0);
                    crs.Emit(OpCodes.Call, cppc_hcc_te);
                }
            }
        }

        public static bool CanProducePigmentColor_AddManaAction_TriggerEvent(bool curr, AddManaToManaBarAction pigment)
        {
            return CanProducePigmentColor_HealthColorCondition_TriggerEvent(curr, pigment._mana);
        }

        public static bool CanProducePigmentColor_HealthColorCondition_TriggerEvent(bool curr, ManaColorSO pigment)
        {
            var boolref = new CanProducePigmentColorInfo(pigment, new BooleanReference(curr));

            foreach (var u in CombatManager.Instance._stats.UnitsOnField())
                CombatManager.Instance.PostNotification(CustomEvents.CAN_PRODUCE_PIGMENT_COLOR, u, boolref);

            return boolref.reference.value;
        }

        [HarmonyPatch(typeof(AttackCostLayout), nameof(AttackCostLayout.CalculatePerformAttackButtonState))]
        [HarmonyILManipulator]
        public static void ModifyWrongPigmentAmount_UI_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            foreach(var m in crs.MatchAfter(x => x.OpCode == OpCodes.Blt_S))
            {
                crs.Emit(OpCodes.Ldarg_0);
                crs.Emit(OpCodes.Call, mwpa_ui_te);
            }
        }

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.CalculateAbilityCostsDamage))]
        [HarmonyILManipulator]
        public static void ModifyWrongPigmentAmount_Character_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if(!crs.JumpBeforeNext(x => x.MatchCallOrCallvirt<CharacterCombat>($"get_{nameof(CharacterCombat.LastCalculatedWrongMana)}")))
            {
                crs.Emit(OpCodes.Ldarg_0);
                crs.Emit(OpCodes.Call, mwpa_c_te);
            }
        }

        public static void ModifyWrongPigmentAmount_UI_TriggerEvent(AttackCostLayout layout)
        {
            var manager = CombatManager.Instance;
            var stats = manager._stats;
            var ui = manager._combatUI;

            if (!stats.TryGetUnit(ui.UnitInInfoID, ui.IsInfoFromCharacter, out var unit))
                return;

            var intRef = new IntegerReference(layout.SlotsThatDealDamage);
            CombatManager.Instance.PostNotification(CustomEvents.MODIFY_WRONG_PIGMENT_AMOUNT, unit, intRef);

            layout.SlotsThatDealDamage = Mathf.Max(0, intRef.value);
        }

        public static void ModifyWrongPigmentAmount_Character_TriggerEvent(CharacterCombat cc)
        {
            var intRef = new IntegerReference(cc.LastCalculatedWrongMana);
            CombatManager.Instance.PostNotification(CustomEvents.MODIFY_WRONG_PIGMENT_AMOUNT, cc, intRef);

            cc.LastCalculatedWrongMana = Mathf.Max(0, intRef.value);
        }

        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.SetUpDefaultAbilities))]
        [HarmonyILManipulator]
        public static void ModifyAbilityRank_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            foreach(var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<CharacterCombat>($"get_{nameof(CharacterCombat.ClampedRank)}")))
            {
                crs.Emit(OpCodes.Ldarg_0);
                crs.Emit(OpCodes.Call, mar_te);
            }
        }

        public static int ModifyAbilityRank_TriggerEvent(int curr, CharacterCombat cc)
        {
            var intRef = new IntegerReference(curr);
            CombatManager.Instance.PostNotification(CustomEvents.MODIFY_ABILITY_RANK, cc, intRef);

            return cc.Character.ClampRank(intRef.value);
        }
    }
}
