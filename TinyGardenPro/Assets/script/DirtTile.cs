using UnityEngine;

public class DirtTile : MonoBehaviour
{
    [Header("Plant Settings")]
    public Transform plantPoint;
    public float plantScale = 0.002f;

    [Header("UI")]
    public GameObject plantPrompt;

    private bool playerNearby = false;
    private bool hasPlant = false;

    void Start()
    {
        if (plantPrompt != null)
            plantPrompt.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && !hasPlant && Input.GetKeyDown(KeyCode.E))
        {
            PlantSelectedFlower();
        }
    }

    void PlantSelectedFlower()
    {
        GameObject flowerPrefab = FlowerSelector.instance != null
            ? FlowerSelector.instance.selectedFlower
            : null;

        if (flowerPrefab == null)
        {
            Debug.LogWarning("No flower selected - click a seed button first.");
            return;
        }

        if (plantPoint == null)
        {
            Debug.LogError("Plant Point is missing!");
            return;
        }

        GameObject wrapper = new GameObject(flowerPrefab.name + "_Wrapper");
        wrapper.transform.position = plantPoint.position;
        wrapper.transform.rotation = plantPoint.rotation;
        wrapper.transform.localScale = Vector3.one * plantScale;

        GameObject planted = Instantiate(flowerPrefab, wrapper.transform);
        planted.transform.localPosition = Vector3.zero;
        planted.transform.localRotation = Quaternion.identity;

        hasPlant = true;

        if (plantPrompt != null)
            plantPrompt.SetActive(false);

        if (ScoreManager.Instance != null)
        {
            FlowerInfo info = planted.GetComponent<FlowerInfo>();
            int points = info != null ? info.points : 10;
            ScoreManager.Instance.AddFlower(points);
        }

        Debug.Log(flowerPrefab.name + " planted!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (!hasPlant && plantPrompt != null)
                plantPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (plantPrompt != null)
                plantPrompt.SetActive(false);
        }
    }
}