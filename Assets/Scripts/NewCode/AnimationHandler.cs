using UnityEngine;

namespace NewCode
{
    public class AnimationHandler : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private RuntimeAnimatorController combatAnimatorController;
        private static readonly int SpeedAnimatorVariable = Animator.StringToHash("speed");
        private static readonly int DeadAnimatorVariable = Animator.StringToHash("dead");

        public void SetWalkingSpeed(Vector3 speed)
        {
            if (speed.magnitude > 1f)
            {
                speed.Normalize();
            }

            speed = transform.InverseTransformDirection(speed);
            speed = Vector3.ProjectOnPlane(speed, Vector3.zero);
            float forwardAmount = speed.z;
            // update the animator parameters
            animator.SetFloat(SpeedAnimatorVariable, forwardAmount, 0.1f, Time.deltaTime);
        }

        public void SwitchToCombatAnimator()
        {
            animator.runtimeAnimatorController = combatAnimatorController;
        }

        public void ShowDeadAnimation()
        {
            animator.SetBool(DeadAnimatorVariable, true);
        }
    }
}