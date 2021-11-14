using UnityEngine;

namespace NewCode.Weapons
{
    public abstract class WeaponEffect : MonoBehaviour
    {
        public abstract void Play();
        public abstract void Stop();
    }
}