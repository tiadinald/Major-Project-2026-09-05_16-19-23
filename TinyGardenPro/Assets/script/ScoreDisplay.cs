using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text flowerCountText; // optional second label

    public void UpdatePoints(int newTotal) => scoreText.text = "Points: " + newTotal;
    public void UpdateFlowerCount(int newCount) => flowerCountText.text = "Flowers: " + newCount;
}