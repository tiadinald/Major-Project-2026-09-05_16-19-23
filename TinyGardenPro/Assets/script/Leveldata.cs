using UnityEngine;

/// <summary>
/// Defines one themed level (Moonroot Forest, Willow Waters, WinterVale,
/// JamRock). Create one asset per level via:
/// Assets > Create > TinyGarden > Level Data
/// </summary>
[CreateAssetMenu(fileName = "NewLevel", menuName = "TinyGarden/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Identity")]
    public string levelName = "Moonroot Forest";
    [TextArea] public string description;

    [Header("Environment")]
    [Tooltip("TerrainLayer(s) painted for this theme's ground (grass, snow, sand, mud, etc.)")]
    public TerrainLayer[] terrainLayers;
    public GameObject skyboxOrLightingPreset; // optional: a prefab that sets ambient/lighting

    [Header("Available Plants")]
    public PlantData[] availablePlants; // the seeds the player can choose from in this level

    [Header("Progression")]
    public int pointsToUnlockNextLevel = 200;
}