using UnityEngine;

namespace NewCode.Characters.Health
{
    public abstract class HealthViewer : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Gradient healthGradient;

        private void OnEnable()
        {
            health.onHealthChanged.AddListener(HealthChanged);
        }

        private void OnDisable()
        {
            health.onHealthChanged.RemoveListener(HealthChanged);
        }

        protected Color GetHealthBarColor(float healthPercentage)
        {
            return healthGradient.Evaluate(healthPercentage);
        }

        protected abstract void HealthChanged(float newHealthPercentage);
    }
}