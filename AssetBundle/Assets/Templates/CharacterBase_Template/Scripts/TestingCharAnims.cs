using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCharAnims : MonoBehaviour
{
    [Header("References (Do not modify)")]
    [SerializeField] Character_AnimHolder _Character;
    [Header("Sprites")]
    [SerializeField] Sprite _frontSprite;
    [SerializeField] Sprite _backSprite;
    [Header("Animator created from the Basic one")]
    [SerializeField] AnimatorOverrideController _AnimatorToTest;
    [Header("Custom Animator")]
    [SerializeField] RuntimeAnimatorController _OriginalAnimatorToTest;

    [Header("TESTING Animation Controls")]
    public bool _StartCombat = false;
    public bool _TurnCharacterAround = false;
    public bool _HurtCharacter = false;
    public bool _KillCharacter = false;
    public bool _ObliterateCharacter = false;
    public bool _AriseCharacter = false;
    public bool _FleeCharacter = false;

    void Start()
    {
        _Character.SetSprites(_frontSprite, _backSprite);
        if(_AnimatorToTest != null)
            _Character.SetAnimator(_AnimatorToTest);
        else if(_OriginalAnimatorToTest != null)
            _Character.SetAnimator(_OriginalAnimatorToTest);
    }

    private void Update()
    {
        if (_StartCombat)
        {
            _StartCombat = false;
            _Character.m_Animator.SetTrigger("Start");
        }
        if (_HurtCharacter)
        {
            _HurtCharacter = false;
            _Character.m_Animator.SetTrigger("Hurt");
        }
        if (_KillCharacter)
        {
            _KillCharacter = false;
            _Character.m_Animator.SetTrigger("Dying");
        }
        if (_ObliterateCharacter)
        {
            _ObliterateCharacter = false;
            _Character.m_Animator.SetTrigger("Obliterate");
        }
        if (_FleeCharacter)
        {
            _FleeCharacter = false;
            _Character.m_Animator.SetTrigger("Fleeting");
        }
        if (_AriseCharacter)
        {
            _AriseCharacter = false;
            _Character.m_Animator.SetTrigger("Arise");
        }
        if (_TurnCharacterAround)
        {
            _TurnCharacterAround = false;
            _Character.LookingAtPlayer = !_Character.LookingAtPlayer;
            if (!_Character.m_Animator.GetBool("TurningAround"))
                _Character.m_Animator.SetBool("TurningAround", true);
        }
    }

}
