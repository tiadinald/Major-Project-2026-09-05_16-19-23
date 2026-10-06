using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Put this on each seed-selection Button in your UI. It holds a
/// reference to one PlantData asset and tells the PlantPlacer to select
/// it when clicked - this is the "tiny wrapper" needed because Button
/// OnClick events in the Inspector can't pass a ScriptableObject
/// argument directly.
///
/// Setup: add this component to a Button, assign 'plant' (the
/// PlantData asset this button represents) and 'placer' (your scene's
/// PlantPlacer), then in the Button's own OnClick() list, drag this
/// same GameObject in and pick SeedButton > Select().
/// </summary>
[RequireComponent(typeof(Button))]
public class SeedButton : MonoBehaviour
{
    [Header("This button represents...")]
    public PlantData plant;

    [Header("References")]
    public PlantPlacer placer;

    /// <summary>Call this from the Button's OnClick() list in the Inspector.</summary>
    public void Select()
    {
        if (plant == null || placer == null)
        {
            Debug.LogWarning($"SeedButton on {gameObject.name} is missing 'plant' or 'placer' reference.");
            return;
        }
        placer.SelectPlant(plant);
    }
}