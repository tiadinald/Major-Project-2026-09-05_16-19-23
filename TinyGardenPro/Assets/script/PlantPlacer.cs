using UnityEngine;

/// <summary>
/// Handles the actual planting interaction: player clicks/taps on the
/// terrain, we raycast to find the world point, snap it to the logic
/// grid, check the rules, spawn the plant, and award/deduct points.
/// </summary>
public class PlantPlacer : MonoBehaviour
{
    [Header("References")]
    public Camera gardenCamera;
    public GardenGrid grid;
    public ScoreManager scoreManager;
    public LayerMask plantableLayer; // set this to whatever layer your Terrain is on

    [Header("Current Selection")]
    public PlantData selectedPlant; // set by the seed-selection UI

    void Update()
    {
        if (selectedPlant == null) return;
        if (!Input.GetMouseButtonDown(0)) return;

        // Optional: skip clicks that land on UI (seed bar, buttons, etc.)
        if (UnityEngine.EventSystems.EventSystem.current != null &&
            UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        TryPlantAtScreenPoint(Input.mousePosition);
    }

    void TryPlantAtScreenPoint(Vector3 screenPoint)
    {
        Ray ray = gardenCamera.ScreenPointToRay(screenPoint);
        if (!Physics.Raycast(ray, out RaycastHit hit, 500f, plantableLayer)) return;

        if (!grid.WorldToCell(hit.point, out int cellX, out int cellY)) return;
        if (grid.IsCellOccupied(cellX, cellY)) return; // cell already has a plant

        bool breaksRule = grid.BreaksDirectionRule(selectedPlant, cellX, cellY);

        // Spawn the plant snapped to the cell centre, but the terrain
        // underneath still looks like a real garden, not a grid.
        Vector3 spawnPos = grid.CellToWorld(cellX, cellY);
        Instantiate(selectedPlant.plantPrefab, spawnPos, Quaternion.identity);

        grid.PlaceAt(selectedPlant, cellX, cellY);

        if (breaksRule)
            scoreManager.AddPoints(-selectedPlant.penaltyOnRuleBreak);
        else
            scoreManager.AddFlower(selectedPlant.pointsOnPlace); // counts the flower AND adds points
    }

    public void SelectPlant(PlantData plant)
    {
        selectedPlant = plant;
    }
}