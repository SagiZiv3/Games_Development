using UnityEngine;

namespace NewCode.Weapons
{
    public class Firearm : MonoBehaviour
    {
        [SerializeField] private string weaponName;
        [SerializeField] private float minDamage, maxDamage;
        [SerializeField] private float attackCooldownTime;
        [SerializeField] private float attackRange = 7.5f;
        [SerializeField] private Transform firePoint;
        [SerializeField] protected LayerMask targetsMask;
        [SerializeField] private WeaponEffect[] weaponEffects;
        private float nextAttackTime;

        public string WeaponName => weaponName;

        public void Fire()
        {
            if (Time.time >= nextAttackTime)
            {
                FireImpl();
                nextAttackTime = Time.time + attackCooldownTime;
                PlayEffects();
            }
        }

        protected void FireImpl()
        {
            if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit,
                attackRange, targetsMask))
            {
                ApplyDamageTo(hit.transform.gameObject);
            }
        }

        private void PlayEffects()
        {
            foreach (WeaponEffect weaponEffect in weaponEffects)
            {
                weaponEffect.Stop();
                weaponEffect.Play();
            }
        }

        protected void ApplyDamageTo(GameObject gObj)
        {
            if (gObj.TryGetComponent(out Characters.Health.Health health))
            {
                float damage = Random.Range(minDamage, maxDamage);
                health.DecreaseHealth(damage);
            }
        }
    }
}