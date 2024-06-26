using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterFieldEffectLayout : MonoBehaviour
{
    [Header("Front Objects")]
    public RectTransform[] m_Front = null;
    public RectTransform[] m_Back = null;
    public RectTransform[] m_Swap = null;

    public bool IsActive { get; private set; } = false;
    public bool HasBeenAccessed { get; private set; } = false;


    public void InitializeLayout(RectTransform frontHolder, RectTransform backHolder, RectTransform swapHolder)
    {
        foreach (RectTransform item in m_Front)
            ParentItem(frontHolder, item);

        foreach (RectTransform item in m_Back)
            ParentItem(backHolder, item);

        foreach (RectTransform item in m_Swap)
            ParentItem(swapHolder, item);
    }

    void ParentItem(RectTransform parent, RectTransform item)
    {
        item.SetParent(parent);
        item.offsetMax = Vector2.zero;
        item.offsetMin = Vector2.zero;
    }

    //Called if we have to enable it!
    public void AccessLayout(bool hasUnit)
    {
        EnableLayout(hasUnit);
        IsActive = true;
        HasBeenAccessed = true;
    }
    //Called when we have been accessing layouts, if HasBeenAccessed is false means that needs to be disabled!
    public void EndAccessLayout()
    {
        if (!HasBeenAccessed)
        {
            DisableLayout();
            IsActive = false;
        }

        HasBeenAccessed = false;
    }
    protected abstract void EnableLayout(bool hasUnit);
    protected abstract void DisableLayout();
    public virtual void TryUpdateLayout(bool hasUnit, bool forceUpdate = false) { }
}
