using UnityEngine;
using UnityEngine.UI;

namespace NewCode.Characters.Health
{
    public class UiHealthViewer : HealthViewer
    {
        [SerializeField] private Image healthBar;

        protected override void HealthChanged(float newHealthPercentage)
        {
            healthBar.color = GetHealthBarColor(newHealthPercentage);
            healthBar.fillAmount = newHealthPercentage;
        }
    }
}