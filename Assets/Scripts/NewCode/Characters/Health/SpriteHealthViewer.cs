using UnityEngine;

namespace NewCode.Characters.Health
{
    public class SpriteHealthViewer : HealthViewer
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform spriteAnchor;
        private Vector3 spriteAnchorScale;

        private void Awake()
        {
            spriteAnchorScale = spriteAnchor.localScale;
        }

        protected override void HealthChanged(float newHealthPercentage)
        {
            spriteRenderer.color = GetHealthBarColor(newHealthPercentage);
            spriteAnchorScale.x = newHealthPercentage;
            spriteAnchor.localScale = spriteAnchorScale;
        }
    }
}