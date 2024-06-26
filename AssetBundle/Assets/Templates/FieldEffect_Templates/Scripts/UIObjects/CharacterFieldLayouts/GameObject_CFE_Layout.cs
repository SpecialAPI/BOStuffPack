using UnityEngine;

public class GameObject_CFE_Layout : CharacterFieldEffectLayout
{
    [Header("GameObjects List")]
    public GameObject[] m_Objects = null;

    protected override void EnableLayout(bool hasUnit)
    {
        foreach (GameObject item in m_Objects)
            item.SetActive(true);
    }

    protected override void DisableLayout()
    {
        foreach (GameObject item in m_Objects)
            item.SetActive(false);
    }
}
