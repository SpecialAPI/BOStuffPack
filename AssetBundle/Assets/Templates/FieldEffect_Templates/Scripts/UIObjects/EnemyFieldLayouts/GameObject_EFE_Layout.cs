using UnityEngine;

public class GameObject_EFE_Layout : EnemyFieldEffectLayout
{
    [Header("GameObjects List")]
    public GameObject[] m_Objects = null;

    protected override void EnableLayout()
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
