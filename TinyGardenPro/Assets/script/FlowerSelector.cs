using UnityEngine;

public class FlowerSelector : MonoBehaviour
{
    public static FlowerSelector instance;

    public GameObject selectedFlower;

    void Awake()
    {
        Debug.Log("FlowerSelector Awake called on: " + gameObject.name, this);
        instance = this;
    }
    public void SelectFlower(GameObject flower)
    {
        selectedFlower = flower;
        Debug.Log("Selected: " + flower.name);
    }
}