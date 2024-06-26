using UnityEngine;
using UnityEngine.UI;

public class Character_AnimHolder : MonoBehaviour
{
    #region EDITOR EXPOSED FIELDS
    [Header("References")]
    [SerializeField] Image _image = null;
    public Animator m_Animator = null;
    #endregion

    Sprite _frontSprite;
    Sprite _backSprite;
    public bool LookingAtPlayer { get; set; } = false;

    public void SetAnimator(RuntimeAnimatorController anim)
    {
        m_Animator.runtimeAnimatorController = anim;
    }
    public void SetAnimator(AnimatorOverrideController anim)
    {
        m_Animator.runtimeAnimatorController = anim;
    }
    public void SetSprites(Sprite front, Sprite back)
    {
        _frontSprite = front;
        _backSprite = back;
    }

    #region Animation Event Calls
    public void AnimationHasFinished()
    {
        Debug.Log("Animation finished called.");
    }

    public void SetCharacterSprite()
    {
        _image.sprite = (LookingAtPlayer) ? _frontSprite : _backSprite;
        m_Animator.SetBool("TurningAround", false);
        m_Animator.SetBool("LookingPlayer", LookingAtPlayer);
    }
    #endregion

}
