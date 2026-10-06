using UnityEngine;
using System.Collections;

public class DaisyGrowth : MonoBehaviour
{
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject stage4;

    public float timeBetweenStages = 60f;

    // How fast the pop animation happens
    public float popTime = 0.25f;

    void Start()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        // Start with only stage 1
        stage1.SetActive(true);
        stage2.SetActive(false);
        stage3.SetActive(false);
        stage4.SetActive(false);

        // Make stage 1 pop up
        yield return StartCoroutine(PopUp(stage1));

        // Wait 1 minute
        yield return new WaitForSeconds(timeBetweenStages);

        // Stage 1 goes down, Stage 2 pops up
        yield return StartCoroutine(ChangeStage(stage1, stage2));

        // Wait 1 minute
        yield return new WaitForSeconds(timeBetweenStages);

        // Stage 2 goes down, Stage 3 pops up
        yield return StartCoroutine(ChangeStage(stage2, stage3));

        // Wait 1 minute
        yield return new WaitForSeconds(timeBetweenStages);

        // Stage 3 goes down, Stage 4 pops up
        yield return StartCoroutine(ChangeStage(stage3, stage4));

        // Stage 4 stays forever 🌼
    }

    IEnumerator ChangeStage(GameObject oldStage, GameObject newStage)
    {
        // Old flower shrinks down
        yield return StartCoroutine(PopDown(oldStage));

        oldStage.SetActive(false);

        // New flower appears
        newStage.SetActive(true);

        // New flower pops up
        yield return StartCoroutine(PopUp(newStage));
    }

    IEnumerator PopUp(GameObject flower)
    {
        Vector3 finalScale = flower.transform.localScale;

        flower.transform.localScale = Vector3.zero;

        float timer = 0f;

        while (timer < popTime)
        {
            timer += Time.deltaTime;
            float t = timer / popTime;

            // Goes slightly bigger than normal
            flower.transform.localScale =
                Vector3.Lerp(Vector3.zero, finalScale * 1.2f, t);

            yield return null;
        }

        // Little bounce back to normal size
        timer = 0f;

        while (timer < popTime / 2f)
        {
            timer += Time.deltaTime;
            float t = timer / (popTime / 2f);

            flower.transform.localScale =
                Vector3.Lerp(finalScale * 1.2f, finalScale, t);

            yield return null;
        }

        flower.transform.localScale = finalScale;
    }

    IEnumerator PopDown(GameObject flower)
    {
        Vector3 startScale = flower.transform.localScale;

        float timer = 0f;

        while (timer < popTime)
        {
            timer += Time.deltaTime;
            float t = timer / popTime;

            flower.transform.localScale =
                Vector3.Lerp(startScale, Vector3.zero, t);

            yield return null;
        }

        // Restore scale for later
        flower.transform.localScale = startScale;
    }
}