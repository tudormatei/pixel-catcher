using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PostProcessingManager postProcessingManager;

    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject comboAdderPrefab;
    [SerializeField] private GameObject comboRemoverPrefab;

    [SerializeField] private Vector3 objectSpawnLocation;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private TextWriter textWriter;

    [SerializeField] private List<string> messages;

    private void Start()
    {
        StartCoroutine(Presentation());
    }

    private IEnumerator Presentation()
    {
        yield return new WaitForSecondsRealtime(2f);
        yield return StartCoroutine(LerpTimeScale(0f, 2f, 4f, 1f, messages[0], .05f));
        yield return new WaitForSecondsRealtime(2f);
        Instantiate(pointPrefab, objectSpawnLocation, Quaternion.Euler(new Vector3(0f, 0f, 180f)));
        yield return StartCoroutine(LerpTimeScale(0f, .5f, 4f, 1f, messages[1], .05f));
        yield return new WaitForSecondsRealtime(3f);
        Instantiate(spikePrefab, objectSpawnLocation, Quaternion.identity);
        yield return StartCoroutine(LerpTimeScale(0f, .5f, 2.5f, 1f, messages[2], .05f));
        yield return new WaitForSecondsRealtime(3f);
        Instantiate(shieldPrefab, objectSpawnLocation, Quaternion.identity);
        yield return StartCoroutine(LerpTimeScale(0f, .5f, 4f, 1f, messages[3], .05f));
        for(int i = 0;i < 10; i++)
        {
            Instantiate(spikePrefab, new Vector3(Random.Range(-15f, 15f), Random.Range(13f, 30f), 0f), Quaternion.identity);
        }
        yield return new WaitForSecondsRealtime(5.5f);
        Instantiate(comboAdderPrefab, objectSpawnLocation, Quaternion.identity);
        yield return StartCoroutine(LerpTimeScale(0f, .5f, 3f, 1f, messages[4], .05f));
        yield return new WaitForSecondsRealtime(3f);
        Instantiate(comboRemoverPrefab, objectSpawnLocation, Quaternion.identity);
        yield return StartCoroutine(LerpTimeScale(0f, .5f, 3f, 1f, messages[5], .05f));
        yield return new WaitForSecondsRealtime(4f);

        postProcessingManager.TutorialFinishEffect();
    }

    private IEnumerator LerpTimeScale(float inEnd, float transitionDuration, float secondsToWaitBetween, float outEnd, string message, float timeBetweenLetters)
    {
        float timeIn = 0;
        float startIn = Time.timeScale;

        while (timeIn < transitionDuration)
        {
            Time.timeScale = Mathf.Lerp(startIn, inEnd, timeIn / transitionDuration);
            timeIn += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = inEnd;

        textWriter.AddWriter(text, message, timeBetweenLetters, true);

        yield return new WaitForSecondsRealtime(secondsToWaitBetween);

        text.text = "";

        float timeOut = 0;
        float startOut = Time.timeScale;

        while(timeOut < transitionDuration)
        {
            Time.timeScale = Mathf.Lerp(startOut, outEnd, timeOut / transitionDuration);
            timeOut += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = outEnd;
    }
}
