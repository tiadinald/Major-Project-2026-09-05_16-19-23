using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Hinge Rotation")]
    public float closedYAngle = 0f;
    public float openYAngle = 90f;
    public float openSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool shouldBeOpen = false;

    void Awake()
    {
        closedRotation = Quaternion.Euler(0f, closedYAngle, 0f);
        openRotation = Quaternion.Euler(0f, openYAngle, 0f);
    }

    void Update()
    {
        Quaternion target = shouldBeOpen ? openRotation : closedRotation;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * openSpeed);
    }

    public void Open()
    {
        shouldBeOpen = true;
    }

    public void Close()
    {
        shouldBeOpen = false;
    }
}