using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private float speed = 3;
    [SerializeField] private float angularSpeed = 25;
    private CharacterController controller;
    private float rotationAboutY, rotationAboutX;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        const float dy = -1; // Gravitation

        // rotation about Y
        rotationAboutY += Input.GetAxis("Mouse X") * angularSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, rotationAboutY, 0);

        // rotation about X
        rotationAboutX -= Input.GetAxis("Mouse Y") * angularSpeed * Time.deltaTime;
        camera.transform.localEulerAngles = new Vector3(rotationAboutX, 0, 0);

        // moving forward/backward/left/right
        var dz = Input.GetAxis("Vertical");
        var dx = Input.GetAxis("Horizontal");

        var motion = new Vector3(dx, 0, dz).normalized * (speed * Time.deltaTime);
        motion.y = dy;
        motion = transform.TransformDirection(motion);
        controller.Move(motion); //in Global coordinates
    }
}