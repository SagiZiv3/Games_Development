using System.Collections;
using UnityEngine;

namespace NewCode.Characters.Npc
{
    public class BloodHandler : MonoBehaviour
    {
        [SerializeField] private GameObject blood;
        [SerializeField] private AnimationCurve bloodCurve;
        [SerializeField] private float startDelay = 2.25f;
        [SerializeField] private float animationTime = 3.75f;
        [SerializeField] private Vector3 targetScale = new Vector3(1.15f, 0.001f, 1.6f);

        public void HealthChanged(float healthPercentage)
        {
            if (healthPercentage == 0.0f)
                StartCoroutine(ShowBloodAnimation());
        }

        private IEnumerator ShowBloodAnimation()
        {
            yield return new WaitForSeconds(startDelay);
            float timePassed = 0f;
            blood.SetActive(true);
            Vector3 startSize = new Vector3(0f, targetScale.y, 0f);
            while (timePassed <= animationTime)
            {
                float percentage = timePassed / animationTime;
                blood.transform.localScale = Vector3.Lerp(startSize, targetScale, bloodCurve.Evaluate(percentage));
                yield return null;
                timePassed += Time.deltaTime;
            }

            blood.transform.localScale = targetScale;
        }
    }
}