using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject[] seedButtons; // drag DaisyButton, DandelionButton, SunflowerButton, SnowflowerButton here

    void Start()
    {
        SetButtonsActive(false);
    }

    public void OnPlayButtonPressed()
    {
        gameObject.SetActive(false);
        SetButtonsActive(true);
    }

    void SetButtonsActive(bool active)
    {
        foreach (var btn in seedButtons)
            if (btn != null) btn.SetActive(active);
    }
}