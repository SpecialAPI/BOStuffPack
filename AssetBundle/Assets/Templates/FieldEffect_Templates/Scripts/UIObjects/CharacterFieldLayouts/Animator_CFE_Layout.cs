using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_CFE_Layout : CharacterFieldEffectLayout
{
    [Header("Animators List")]
    public Animator[] m_Animators = null;
    public Animator[] m_NoUnitAnimators = null;
    [SerializeField] string _AnimatorNameOffset = "Offset";

    bool _HasUnit = false;

    protected override void EnableLayout(bool hasUnit)
    {
        //Was not active, so first enable the all objects that don't require Unit check
        if (!IsActive)
        {
            foreach (Animator item in m_Animators)
            {
                item.gameObject.SetActive(true);
                float random = Random.Range(0f, 1f);
                item.SetFloat(_AnimatorNameOffset, random);
            }
        }

        //Takes care of the No Unit stuff!
        //Force update if it was not active before!
        TryUpdateLayout(hasUnit, !IsActive);
    }

    protected override void DisableLayout()
    {
        foreach (Animator item in m_Animators)
            item.gameObject.SetActive(false);

        foreach (Animator item in m_NoUnitAnimators)
            item.gameObject.SetActive(false);
    }

    public override void TryUpdateLayout(bool hasUnit, bool forceUpdate = false) 
    {
        //No change
        if (!forceUpdate && hasUnit == _HasUnit)
            return;

        if (!hasUnit)
        {
            foreach (Animator item in m_NoUnitAnimators)
            {
                item.gameObject.SetActive(true);
                float random = Random.Range(0f, 1f);
                item.SetFloat(_AnimatorNameOffset, random);
            }
        }
        else
        {
            foreach (Animator item in m_NoUnitAnimators)
                item.gameObject.SetActive(false);
        }

        _HasUnit = hasUnit;
    }
}
