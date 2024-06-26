using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFieldEffectLayout : MonoBehaviour
{
    public bool IsActive { get; private set; } = false;
    public bool HasBeenAccessed { get; private set; } = false;
    
    //Called if we have to enable it!
    public void AccessLayout()
    {
        EnableLayout();
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
    protected abstract void EnableLayout();
    protected abstract void DisableLayout();
}
