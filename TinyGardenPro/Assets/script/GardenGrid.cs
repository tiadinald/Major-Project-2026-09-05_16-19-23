using UnityEngine;

/// <summary>
/// An invisible logic grid laid over the plantable area of the Terrain.
/// Nothing here is rendered - it just maps world positions to cells and
/// tracks which plant type occupies each row/column so the direction
/// rule can be checked.
///
/// Setup: put this on an empty GameObject positioned at the corner of
/// your plantable patch, sized to match it with gridWidth/gridHeight
/// and cellSize.
/// </summary>
public class GardenGrid : MonoBehaviour
{
    [Header("Grid Size")]
    public int gridWidth = 10;
    public int gridHeight = 20;
    public float cellSize = 2f; // world units per cell - tune to your terrain scale

    // cells[x, y] holds the PlantData placed there, or null if empty
    private PlantData[,] cells;

    void Awake()
    {
        cells = new PlantData[gridWidth, gridHeight];
    }

    /// <summary>
    /// Converts a world-space hit point (from a raycast onto the terrain)
    /// into grid coordinates. Returns false if the point is outside the grid.
    /// </summary>
    public bool WorldToCell(Vector3 worldPoint, out int cellX, out int cellY)
    {
        Vector3 local = worldPoint - transform.position;
        cellX = Mathf.FloorToInt(local.x / cellSize);
        cellY = Mathf.FloorToInt(local.z / cellSize); // z = "depth" on the ground plane

        return cellX >= 0 && cellX < gridWidth && cellY >= 0 && cellY < gridHeight;
    }

    /// <summary>
    /// World-space centre of a cell - use this to snap the spawned plant
    /// visually onto the grid even though the ground looks like a real garden.
    /// </summary>
    public Vector3 CellToWorld(int cellX, int cellY)
    {
        float worldX = transform.position.x + (cellX + 0.5f) * cellSize;
        float worldZ = transform.position.z + (cellY + 0.5f) * cellSize;
        return new Vector3(worldX, transform.position.y, worldZ);
    }

    public bool IsCellOccupied(int x, int y) => cells[x, y] != null;

    /// <summary>
    /// Checks whether placing 'plant' at (x, y) breaks the "no repeat
    /// in the same row or column" rule. Daisies (exemptFromDirectionRule)
    /// never break the rule.
    /// </summary>
    public bool BreaksDirectionRule(PlantData plant, int x, int y)
    {
        if (plant.exemptFromDirectionRule) return false;

        // Check the whole row
        for (int i = 0; i < gridWidth; i++)
        {
            if (i != x && cells[i, y] == plant) return true;
        }
        // Check the whole column
        for (int j = 0; j < gridHeight; j++)
        {
            if (j != y && cells[x, j] == plant) return true;
        }
        return false;
    }

    public void PlaceAt(PlantData plant, int x, int y)
    {
        cells[x, y] = plant;
    }

    public void ExpandGrid(int addWidth, int addHeight)
    {
        int newW = gridWidth + addWidth;
        int newH = gridHeight + addHeight;
        PlantData[,] newCells = new PlantData[newW, newH];
        for (int x = 0; x < gridWidth; x++)
            for (int y = 0; y < gridHeight; y++)
                newCells[x, y] = cells[x, y];

        cells = newCells;
        gridWidth = newW;
        gridHeight = newH;
    }
}