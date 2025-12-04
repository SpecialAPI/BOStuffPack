using BOStuffPack.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;

namespace BOStuffPack.Content.Misc
{
    public class ShowItemRestoredUIAction(int id, string itemName, Sprite itemSprite = null) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            var ui = stats.combatUI;
            var audio = stats.audioController;

            var chPos = ui.TryGetCharacterFieldPosition(id);
            ui.PlaySoundOnPosition(audio.itemGet, chPos);
            var popup = ui._popUpHandler3D;

            if (itemSprite == null)
            {
                var currentText = popup.GetIdleText(false, out var idleList);
                currentText.transform.position = chPos;
                var t = popup._itemTextOptions.PrepareTextOptions(currentText, itemName, 0);
                currentText.Text.text = $"{itemName}\n{CustomLoc.GetUIData(CustomLoc.ItemRestoredID, CustomLoc.ItemRestoredDefault)}";
                t.OnComplete(() => popup.FinalizeTextShowcase(currentText, idleList));
                currentText.gameObject.SetActive(true);
                yield return t.WaitForPosition(popup._WaitTime);
            }
            else
            {
                var currentText = popup.GetIdleSpriteText(false, out var idleList);
                currentText.transform.position = chPos;
                var t = popup._ItemWithSpriteTextOptions.PrepareSpriteTextOptions(currentText, itemName, itemSprite, 0);
                currentText.Text.text = $"{itemName}\n{CustomLoc.GetUIData(CustomLoc.ItemRestoredID, CustomLoc.ItemRestoredDefault)}";
                t.OnComplete(() => popup.FinalizeSpriteTextShowcase(currentText, idleList));
                currentText.gameObject.SetActive(true);
                yield return t.WaitForPosition(popup._WaitTime);
            }
        }
    }
}
