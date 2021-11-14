using System;
using UnityEngine;
using UnityEngine.Events;

namespace NewCode.Characters.Health
{
    [Serializable]
    public class HealthChangedEvent : UnityEvent<float>
    {
    }

    public class Health : MonoBehaviour
    {
        public float HealthPercentage => currentHealth / maxHealth;
        public HealthChangedEvent onHealthChanged;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private UpdatesViewer updatesViewer;
        private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
            onHealthChanged.Invoke(HealthPercentage);
        }

        public void DecreaseHealth(float delta)
        {
            updatesViewer.WriteHealthLost(transform.name, delta);
            SetCurrentHealth(-delta);
        }

        public void IncreaseHealth(float delta)
        {
            float healthBefore = currentHealth;
            SetCurrentHealth(delta);
            float healthAfter = currentHealth;
            updatesViewer.WriteHealthLost(transform.name, healthAfter - healthBefore);
        }

        private void SetCurrentHealth(float delta)
        {
            currentHealth += delta;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            onHealthChanged.Invoke(HealthPercentage);
        }
    }
}