using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("State")]
    public int currentPoints = 0;
    public int flowersPlanted = 0;

    [Header("Events")]
    public UnityEvent<int> onPointsChanged;
    public UnityEvent<int> onPointsDelta;
    public UnityEvent<int> onFlowerCountChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void AddPoints(int amount)
    {
        currentPoints = Mathf.Max(0, currentPoints + amount);
        onPointsChanged?.Invoke(currentPoints);
        onPointsDelta?.Invoke(amount);
    }

    public void AddFlower(int pointsAwarded)
    {
        flowersPlanted++;
        onFlowerCountChanged?.Invoke(flowersPlanted);
        AddPoints(pointsAwarded);
    }

    public bool CanAfford(int cost) => currentPoints >= cost;

    public bool TrySpend(int cost)
    {
        if (!CanAfford(cost)) return false;
        currentPoints -= cost;
        onPointsChanged?.Invoke(currentPoints);
        return true;
    }
}