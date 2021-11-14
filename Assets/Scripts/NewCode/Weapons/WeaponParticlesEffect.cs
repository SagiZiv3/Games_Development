using UnityEngine;

namespace NewCode.Weapons
{
    public class WeaponParticlesEffect : WeaponEffect
    {
        [SerializeField] private ParticleSystem audioSource;

        public override void Play() => audioSource.Play();

        public override void Stop() => audioSource.Stop();
    }
}
