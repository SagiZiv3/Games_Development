using UnityEngine;

namespace NewCode.Characters.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementHandler : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private Camera eyes;
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float minEyesRotation = -25f, maxEyesRotation = 45;
        [SerializeField] private float boostMultiplier = 1.3f;
        private Vector3 eyesRotation;

        private void Awake()
        {
            eyesRotation = eyes.transform.eulerAngles;
        }

        public void Move(Vector3 movement, bool boost=false)
        {
            Vector3 dir = movement.normalized;
            float speed = boost ? movementSpeed * boostMultiplier : movementSpeed;
            Vector3 change = transform.TransformDirection(dir) * (speed * Time.deltaTime);
            rigidbody.MovePosition(rigidbody.position + change);
        }

        public void Rotate(Vector3 rotation)
        {
            Vector3 bodyRotation = new Vector3(0, rotation.x, 0);
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(bodyRotation));
            eyesRotation.x = Mathf.Clamp(eyesRotation.x + rotation.y, minEyesRotation, maxEyesRotation);
            eyesRotation.y = 0;
            eyes.transform.localRotation = Quaternion.Euler(eyesRotation);
        }

        private void OnValidate()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            if (eyes == null)
            {
                eyes = GetComponentInChildren<Camera>();
            }
        }
    }
}