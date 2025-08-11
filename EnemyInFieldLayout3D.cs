using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BOStuffPack
{
    [HarmonyPatch]
    public class EnemyInFieldLayout3D : MonoBehaviour
    {
        public EnemyInFieldLayout enemyLayout;
        public EnemyInFieldLayout3DData data;

        public void Initialization()
        {
            data.Renderer.materials.Last().SetFloat("_OutlineAlpha", 0f);
        }

        public void UpdateSlotSelection()
        {
            var cData = LoadedDBsHandler.CombatData;

            var alpha = 0f;
            var col = cData.EnemyBasicColor;
            if (enemyLayout.TurnSelected)
            {
                col = cData.EnemyTurnColor;
                alpha = 1f;
            }
            else if (enemyLayout.TargetSelected)
            {
                col = cData.EnemyTargetColor;
                alpha = 1f;
            }
            else if (enemyLayout.MouseSelected)
            {
                col = cData.EnemyHoverColor;
                alpha = 1f;
            }

            var outlineMat = data.Renderer.materials.Last();
            outlineMat.SetColor("_OutlineColor", col);
            outlineMat.SetFloat("_OutlineAlpha", alpha);
        }

        [HarmonyPatch(typeof(EnemyInFieldLayout), nameof(EnemyInFieldLayout.Initialization))]
        [HarmonyPostfix]
        private static void Initialize3DLayout_Postfix(EnemyInFieldLayout __instance)
        {
            var eifl3D = __instance.GetComponent<EnemyInFieldLayout3D>();

            if (eifl3D)
                eifl3D.Initialization();
        }

        [HarmonyPatch(typeof(EnemyInFieldLayout), nameof(EnemyInFieldLayout.UpdateSlotSelection))]
        [HarmonyPostfix]
        private static void Update3DLayoutSlotSelection_Postfix(EnemyInFieldLayout __instance)
        {
            var eifl3D = __instance.GetComponent<EnemyInFieldLayout3D>();

            if (eifl3D)
                eifl3D.UpdateSlotSelection();
        }
    }
}
