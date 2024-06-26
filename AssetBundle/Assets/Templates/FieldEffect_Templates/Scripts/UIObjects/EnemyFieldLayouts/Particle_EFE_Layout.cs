using UnityEngine;

public class Particle_EFE_Layout : EnemyFieldEffectLayout
{
    [Header("Particles List")]
    public ParticleSystem[] m_FieldParticles = null;

    protected override void EnableLayout()
    {
        foreach (ParticleSystem item in m_FieldParticles)
            item.Play();
    }

    protected override void DisableLayout()
    {
        foreach (ParticleSystem item in m_FieldParticles)
            item.Stop();
    }
}
