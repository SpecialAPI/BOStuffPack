using BOStuffPack.API.UnitExtension;
using System;
using System.Collections.Generic;
using System.Text;

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
        public const string MODIFY_TARGETTING = "ModifyTargetting";
        public const string MODIFY_TARGETTING_INTENTS = "ModifyTargettingIntents";
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

        public static MethodInfo mt_aaetl_atl = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_AddAbilityEffectsToList_AddToList));
        public static MethodInfo mt_aaetl_se = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_AddAbilityEffectsToList_SetEffects));
        public static MethodInfo mt_aaetl_rfl = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_AddAbilityEffectsToList_RemoveFromList));
        public static MethodInfo mt_sarv_e_s = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_SetAndResetVariables_Effects_Set));
        public static MethodInfo mt_sarv_v_s = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_SetAndResetVariables_Visuals_Set));
        public static MethodInfo mt_sarv_r = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_SetAndResetVariables_Reset));
        public static MethodInfo mt_i_te = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_Intent_TriggerEvent));
        public static MethodInfo mt_i_s = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_Intent_Set));
        public static MethodInfo mt_i_ts = AccessTools.Method(typeof(EventPatches), nameof(ModifyTargetting_Intent_TargetSwap));

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
                    crs.Emit(OpCodes.Call, bae_e_te);
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

        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.UseAbility), typeof(int))]
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.UseAbility), typeof(int), typeof(FilledManaCost[]))]
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.TryPerformRandomAbility), [])]
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.TryPerformRandomAbility), typeof(AbilitySO))]
        [HarmonyILManipulator]
        public static void ModifyTargetting_AddAbilityEffectsToList_Transpiler(ILContext ctx, MethodBase mthd)
        {
            var crs = new ILCursor(ctx);

            var infoLocal = crs.DeclareLocal<PerformedAbilityInformation>();

            if (!crs.JumpToNext(x => x.MatchNewobj<PlayAbilityAnimationAction>()))
                return;

            if (mthd.Name == nameof(CharacterCombat.TryPerformRandomAbility))
                if (mthd.GetParameters().Length > 0)
                    crs.Emit(OpCodes.Ldarg_0);

                else
                    crs.Emit(OpCodes.Ldloc_1);

            else
                crs.Emit(OpCodes.Ldloc_0);

            crs.Emit(OpCodes.Ldarg_0);
            crs.Emit(OpCodes.Ldloca_S, infoLocal);
            crs.Emit(OpCodes.Call, mt_aaetl_atl);

            if (!crs.JumpToNext(x => x.MatchNewobj<EffectAction>()))
                return;

            crs.Emit(OpCodes.Ldloc_S, infoLocal);
            crs.Emit(OpCodes.Call, mt_aaetl_se);

            if (!crs.JumpBeforeNext(x => x.MatchRet()))
                return;

            crs.Emit(OpCodes.Ldarg_0);
            crs.Emit(OpCodes.Ldloc_S, infoLocal);
            crs.Emit(OpCodes.Call, mt_aaetl_rfl);
        }

        public static PlayAbilityAnimationAction ModifyTargetting_AddAbilityEffectsToList_AddToList(PlayAbilityAnimationAction action, AbilitySO ability, IUnit caster, out PerformedAbilityInformation info)
        {
            ModifyTargettingAPI.ResetTemporaryTargettingModifications();

            var ext = caster.Ext();

            if (ext != null)
            {
                ext.CurrentlyPerformedAbilities.Add(info = new() { visuals = action, ability = ability });

                CombatManager.Instance.AddRootAction(new PreprocessAbilityInformationAction(info, caster));
            }
            else
                info = null;

            return action;
        }

        public static EffectAction ModifyTargetting_AddAbilityEffectsToList_SetEffects(EffectAction action, PerformedAbilityInformation info)
        {
            ModifyTargettingAPI.ResetTemporaryTargettingModifications();

            if (info != null)
                info.effects = action;

            return action;
        }

        public static void ModifyTargetting_AddAbilityEffectsToList_RemoveFromList(IUnit caster, PerformedAbilityInformation info)
        {
            if (info != null && caster != null)
                CombatManager.Instance.AddRootAction(new RemoveAbilityInformationFromListAction(info, caster));
        }

        [HarmonyPatch(typeof(EffectAction), nameof(EffectAction.Execute), MethodType.Enumerator)]
        [HarmonyILManipulator]
        public static void ModifyTargetting_SetAndResetVariables_Effects_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            foreach(var m in crs.MatchBefore(x => x.MatchCallOrCallvirt<BaseCombatTargettingSO>(nameof(BaseCombatTargettingSO.GetTargets))))
            {
                crs.Emit(OpCodes.Ldloc_1);
                crs.Emit(OpCodes.Call, mt_sarv_e_s);
            }

            crs.Index = 0;
            foreach (var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<BaseCombatTargettingSO>(nameof(BaseCombatTargettingSO.GetTargets))))
            {
                crs.Emit(OpCodes.Call, mt_sarv_r);
            }
        }

        public static bool ModifyTargetting_SetAndResetVariables_Effects_Set(bool curr, EffectAction act)
        {
            ModifyTargettingAPI.ResetTemporaryTargettingModifications();

            if (act == null)
                return curr;

            var ext = act._caster.Ext();

            if (ext != null && ext.CurrentlyPerformedAbilities.FirstOrDefault(x => x.effects == act) is PerformedAbilityInformation inf && inf != null)
            {
                ModifyTargettingAPI.targetOffset = inf.targetOffset;
                ModifyTargettingAPI.targetSwapped = inf.isTargetsSwapped;
            }

            return curr;
        }

        public static TargetSlotInfo[] ModifyTargetting_SetAndResetVariables_Reset(TargetSlotInfo[] curr)
        {
            ModifyTargettingAPI.ResetTemporaryTargettingModifications();

            return curr;
        }

        [HarmonyPatch(typeof(PlayAbilityAnimationAction), nameof(PlayAbilityAnimationAction.Execute), MethodType.Enumerator)]
        [HarmonyILManipulator]
        public static void ModifyTargetting_SetAndResetVariables_Visuals_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            foreach (var m in crs.MatchBefore(x => x.MatchCallOrCallvirt<BaseCombatTargettingSO>(nameof(BaseCombatTargettingSO.GetTargets))))
            {
                crs.Emit(OpCodes.Ldloc_1);
                crs.Emit(OpCodes.Call, mt_sarv_v_s);
            }

            crs.Index = 0;
            foreach (var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<BaseCombatTargettingSO>(nameof(BaseCombatTargettingSO.GetTargets))))
            {
                crs.Emit(OpCodes.Call, mt_sarv_r);
            }
        }

        public static bool ModifyTargetting_SetAndResetVariables_Visuals_Set(bool curr, PlayAbilityAnimationAction act)
        {
            ModifyTargettingAPI.ResetTemporaryTargettingModifications();

            if (act == null)
                return curr;

            var ext = act._caster.Ext();

            if (ext != null && ext.CurrentlyPerformedAbilities.FirstOrDefault(x => x.visuals == act) is PerformedAbilityInformation inf && inf != null)
            {
                ModifyTargettingAPI.targetOffset = inf.targetOffset;
                ModifyTargettingAPI.targetSwapped = inf.isTargetsSwapped;
            }

            return curr;
        }

        [HarmonyPatch(typeof(Targetting_BySlot_Index), nameof(Targetting_BySlot_Index.GetTargets))]
        [HarmonyPatch(typeof(CustomOpponentTargetting_BySlot_Index), nameof(CustomOpponentTargetting_BySlot_Index.GetTargets))]
        [HarmonyPostfix]
        public static void ModifyTargetting_BySlot_Index_Postfix(ref TargetSlotInfo[] __result, SlotsCombat slots)
        {
            var offs = ModifyTargettingAPI.targetOffset ?? 0;
            var swapped = ModifyTargettingAPI.targetSwapped ?? false;

            if (offs == 0 && !swapped)
                return;

            var slot = new List<TargetSlotInfo>();

            foreach(var s in __result)
            {
                var newSlot = slots.GetGenericAllySlotTarget(s.SlotID + offs, s.IsTargetCharacterSlot != swapped);

                if(newSlot != null)
                    slot.Add(newSlot);
            }

            __result = [.. slot];
        }

        [HarmonyPatch(typeof(GenericTargetting_BySlot_Index), nameof(GenericTargetting_BySlot_Index.GetTargets))]
        [HarmonyPostfix]
        public static void ModifyTargetting_Generic_BySlot_Index_Postfix(ref TargetSlotInfo[] __result, SlotsCombat slots)
        {
            var swapped = ModifyTargettingAPI.targetSwapped ?? false;

            if (!swapped)
                return;

            var slot = new List<TargetSlotInfo>();

            foreach (var s in __result)
            {
                var newSlot = slots.GetGenericAllySlotTarget(s.SlotID, s.IsTargetCharacterSlot != swapped);

                if (newSlot != null)
                    slot.Add(newSlot);
            }

            __result = [.. slot];
        }

        [HarmonyPatch(typeof(CombatVisualizationController), nameof(CombatVisualizationController.ShowTargeting))]
        [HarmonyILManipulator]
        public static void ModifyTargetting_Intent_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchStloc(0)))
                return;

            var targettingStuff = crs.DeclareLocal<ModifyTargettingInfo>();

            crs.Emit(OpCodes.Ldarg_1);
            crs.Emit(OpCodes.Ldarg_3);
            crs.Emit(OpCodes.Ldarg_S, (byte)5);
            crs.Emit(OpCodes.Ldloca_S, targettingStuff);

            crs.Emit(OpCodes.Call, mt_i_te);

            foreach (var m in crs.MatchBefore(x => x.MatchCallOrCallvirt<BaseCombatTargettingSO>(nameof(BaseCombatTargettingSO.GetTargets))))
            {
                crs.Emit(OpCodes.Ldloc_S, targettingStuff);
                crs.Emit(OpCodes.Call, mt_i_s);
            }

            crs.Index = 0;
            if (!crs.JumpToNext(x => x.MatchStloc(0)))
                return;

            foreach (var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<BaseCombatTargettingSO>(nameof(BaseCombatTargettingSO.GetTargets))))
            {
                crs.Emit(OpCodes.Call, mt_sarv_r);
            }

            crs.Index = 0;
            if (!crs.JumpToNext(x => x.MatchStloc(0)))
                return;

            foreach(var m in crs.MatchAfter(x => x.MatchCallOrCallvirt<BaseCombatTargettingSO>($"get_{nameof(BaseCombatTargettingSO.AreTargetAllies)}")))
            {
                crs.Emit(OpCodes.Ldloc_S, targettingStuff);
                crs.Emit(OpCodes.Call, mt_i_ts);
            }
        }

        public static void ModifyTargetting_Intent_TriggerEvent(AbilitySO ab, int casterId, bool casterIsCharacter, out ModifyTargettingInfo inf)
        {
            inf = new(new(false), new(0), ab);

            if (!CombatManager.Instance._stats.TryGetUnit(casterId, casterIsCharacter, out var u))
                return;

            CombatManager.Instance.PostNotification(CustomEvents.MODIFY_TARGETTING_INTENTS, u, inf);
        }

        public static bool ModifyTargetting_Intent_Set(bool curr, ModifyTargettingInfo inf)
        {
            ModifyTargettingAPI.ResetTemporaryTargettingModifications();

            if (inf == null)
                return curr;

            if(inf.intReference != null)
                ModifyTargettingAPI.targetOffset = inf.intReference.value;

            if (inf.boolReference != null)
                ModifyTargettingAPI.targetSwapped = inf.boolReference.value;

            return curr;
        }

        public static bool ModifyTargetting_Intent_TargetSwap(bool curr, ModifyTargettingInfo inf)
        {
            if (inf == null)
                return curr;

            if (inf.boolReference != null)
                return curr != inf.boolReference.value;

            return curr;
        }
    }
}
