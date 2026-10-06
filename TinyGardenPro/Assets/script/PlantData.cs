using UnityEngine;

/// <summary>
/// Defines one plantable species (e.g. Sunflower, Rose, Daisy, Tree).
/// Create one asset per plant via: Assets > Create > TinyGarden > Plant Data
/// </summary>
[CreateAssetMenu(fileName = "NewPlant", menuName = "TinyGarden/Plant Data")]
public class PlantData : ScriptableObject
{
    [Header("Identity")]
    public string plantName = "Sunflower";
    public Sprite seedIcon;          // shown on the seed-selection UI button
    public GameObject plantPrefab;   // the 3D model spawned in the garden

    [Header("Rules")]
    [Tooltip("If true, this plant is exempt from the 'no repeat in same row/column' rule (e.g. Daisies).")]
    public bool exemptFromDirectionRule = false;

    [Header("Economy")]
    [Tooltip("Points earned for a valid placement.")]
    public int pointsOnPlace = 10;

    [Tooltip("Points lost if placed breaking the direction rule.")]
    public int penaltyOnRuleBreak = 5;

    [Tooltip("Points required to unlock this plant in the seed selector.")]
    public int unlockCost = 0; // 0 = available from the start
}
