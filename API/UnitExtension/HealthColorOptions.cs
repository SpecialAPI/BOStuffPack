using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

namespace BOStuffPack.API.UnitExtension
{
    [HarmonyPatch]
    public static class HealthColorOptions
    {
        public static Sprite ManaToggle_Disabled = LoadSprite("UI_ManaToggle_Disabled");

        [HarmonyPatch(typeof(CombatVisualizationController), nameof(CombatVisualizationController.FirstInitialization))]
        [HarmonyPrefix]
        public static void CopyButton(CombatVisualizationController __instance)
        {
            var button = __instance.transform.Find("Canvas").Find("LowerZone").Find("InformationCanvas").Find("CharacterCostZone").Find("AttackCostLayout").Find("LuckyManaLayout").Find("ChangeLuckyBlueButton");
            var healthlayout = __instance.transform.Find("Canvas").Find("LowerZone").Find("InformationCanvas").Find("InfoZoneLayout").Find("PortraitZone").Find("PortraitHolder").Find("PortraitSprite").Find("CombatHealthBarLayout");

            var newButton = Object.Instantiate(button.gameObject, healthlayout);

            newButton.SetActive(false);
            newButton.transform.localPosition = newButton.AddComponent<LocalPositionConstantSetter>().targetPos = new Vector3(125f, 0f, 0f);
            newButton.transform.localScale = new Vector3(3f, 3f, 3f);

            var buttonComp = newButton.GetComponent<Button>();
            var holder = healthlayout.gameObject.AddComponent<ChangeHealthColorButtonHolder>();

            holder.originalSprite = newButton.GetComponent<Image>().sprite;
            holder.button = newButton;

            buttonComp.onClick = new();
            buttonComp.onClick.AddListener(holder.ButtonClicked);

            var colors = buttonComp.colors;

            colors.disabledColor = __instance._characterCost._performAttackButton.colors.disabledColor;
            buttonComp.colors = colors;
        }

        [HarmonyPatch(typeof(CombatHealthBarLayout), nameof(CombatHealthBarLayout.SetInformation))]
        [HarmonyPrefix]
        public static void UpdateButton(CombatHealthBarLayout __instance)
        {
            if (CombatManager.Instance != null && CombatManager.Instance._combatUI != null)
            {
                var buttonhold = __instance.GetComponent<ChangeHealthColorButtonHolder>();

                if (buttonhold == null || buttonhold.button == null)
                    return;

                if (!CombatManager.Instance._stats.TryGetUnitOnField(CombatManager.Instance._combatUI.UnitInInfoID, CombatManager.Instance._combatUI.IsInfoFromCharacter, out var u))
                    return;

                var buttonActive = u != null && u.Ext().HealthColors.Count > 1;

                buttonhold.button.SetActive(buttonActive);
                buttonhold.button.transform.localPosition = new Vector3(125f, 0f, 0f);
            }
        }

        [HarmonyPatch(typeof(AttackCostLayout), nameof(AttackCostLayout.UpdatePerformAttackButton))]
        [HarmonyPostfix]
        public static void UpdateButtonInteractability(AttackCostLayout __instance)
        {
            if (CombatManager.Instance != null && CombatManager.Instance._combatUI != null)
            {
                var buttonhold = CombatManager.Instance._combatUI._infoZone._unitHealthBar.GetComponent<ChangeHealthColorButtonHolder>();
                if (buttonhold != null && buttonhold.button != null)
                {
                    buttonhold.button.GetComponent<Image>().sprite = (buttonhold.button.GetComponent<Button>().interactable = !__instance.PlayerInputLocked) ? buttonhold.originalSprite : ManaToggle_Disabled;
                }
            }
        }

        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.HealthColor), MethodType.Setter)]
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.HealthColor), MethodType.Setter)]
        [HarmonyPostfix]
        public static void ChangeCharacterColorOption(IUnit __instance, ManaColorSO value)
        {
            var ext = __instance.Ext();

            ext.HealthColors[ext.HealthColorIndex % ext.HealthColors.Count] = value;
        }
    }

    public class LocalPositionConstantSetter : MonoBehaviour
    {
        public Vector3 targetPos;

        public void LateUpdate()
        {
            transform.localPosition = targetPos;
        }
    }

    public class ChangeHealthColorButtonHolder : MonoBehaviour
    {
        public GameObject button;
        public CombatHealthBarLayout layout;
        public Sprite originalSprite;

        public void ButtonClicked()
        {
            if (CombatManager.Instance != null && CombatManager.Instance._combatUI != null)
            {
                IUnit u;
                if (CombatManager.Instance._combatUI.IsInfoFromCharacter)
                {
                    u = CombatManager.Instance._stats.TryGetCharacterOnField(CombatManager.Instance._combatUI.UnitInInfoID);
                }
                else
                {
                    u = CombatManager.Instance._stats.TryGetEnemyOnField(CombatManager.Instance._combatUI.UnitInInfoID);
                }
                if (u != null)
                {
                    var boolref = new BooleanReference(true);
                    CombatManager.Instance.PostNotification(TriggerCalls.CanChangeHealthColor.ToString(), u, boolref);

                    if (boolref.value)
                    {
                        var ext = u.Ext();
                        ext.HealthColorIndex = (ext.HealthColorIndex + 1) % ext.HealthColors.Count;

                        u.ChangeHealthColor(ext.HealthColors[ext.HealthColorIndex]);
                    }
                }
            }
        }
    }
}
