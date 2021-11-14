using UnityEngine;

namespace NewCode.Weapons
{
    public class WeaponSoundEffect : WeaponEffect
    {
        [SerializeField] private AudioSource audioSource;

        public override void Play() => audioSource.Play();

        public override void Stop() => audioSource.Stop();
    }
}