using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 7f;
    public float height = 2f;
    public float mouseSensitivity = 3f;

    public float zoomSpeed = 3f;
    public float minDistance = 2f;
    public float maxDistance = 12f;

    [Header("Collision")]
    public LayerMask collisionMask = ~0; // everything by default - set this to exclude the Player layer
    public float collisionBuffer = 0.3f; // pull the camera slightly off the wall

    private float yaw = 0f;
    private float pitch = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, -10f, 70f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPoint = target.position + Vector3.up * height;

        // Desired camera position before collision check
        Vector3 desiredPosition = targetPoint - rotation * Vector3.forward * distance;

        // Raycast from the target out to the desired camera position
        float actualDistance = distance;
        if (Physics.Raycast(targetPoint, -(rotation * Vector3.forward), out RaycastHit hit, distance, collisionMask))
        {
            actualDistance = hit.distance - collisionBuffer;
        }

        transform.position = targetPoint - rotation * Vector3.forward * actualDistance;
        transform.LookAt(targetPoint);
    }
}