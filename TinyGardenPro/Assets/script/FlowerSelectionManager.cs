using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Holds the currently selected flower and exposes it to whatever
/// script handles planting (e.g. your E-key planting script).
/// Buttons call SelectFlower() directly; locked flowers can't be selected
/// until UnlockSnowFlowers() is called (e.g. on entering Level 2 / WinterVale).
/// </summary>
public class FlowerSelectionManager : MonoBehaviour
{
    public static FlowerSelectionManager Instance { get; private set; }

    [Header("Level 1 flowers (always available)")]
    public PlantData daisy;
    public PlantData dandelion;
    public PlantData sunflower;

    [Header("Level 2 flowers (locked until unlocked)")]
    public PlantData snowFlower1;
    public PlantData snowFlower2;
    public PlantData snowFlower3;

    [Header("Snow flower buttons (to enable/disable visually)")]
    public List<GameObject> snowFlowerButtons; // drag the 3 snow-flower button GameObjects here

    public PlantData CurrentSelection { get; private set; }
    public bool SnowFlowersUnlocked { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        SetSnowButtonsActive(false); // locked at game start
        CurrentSelection = daisy;    // sensible default
    }

    // Wire each button's OnClick() to this, passing its own PlantData
    public void SelectFlower(PlantData flower)
    {
        CurrentSelection = flower;
    }

    public void UnlockSnowFlowers()
    {
        SnowFlowersUnlocked = true;
        SetSnowButtonsActive(true);
    }

    private void SetSnowButtonsActive(bool active)
    {
        foreach (var btn in snowFlowerButtons)
            if (btn != null) btn.SetActive(active);
    }
}