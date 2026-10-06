using UnityEngine;

public class DoorTriggerZone : MonoBehaviour
{
    public DoorController[] doorsToControl;
    public string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            foreach (DoorController door in doorsToControl)
            {
                door.Open();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            foreach (DoorController door in doorsToControl)
            {
                door.Close();
            }
        }
    }
}