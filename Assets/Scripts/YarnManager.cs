using UnityEngine;
using TMPro;
using System.Collections;

public class YarnManager : MonoBehaviour
{
    public int totalYarn = 4;
    private int collectedYarn = 0;

    public TMP_Text yarnText; // Changed from Text to TMP_Text
    public CanvasGroup yarnUI;
    public float uiDisplayTime = 2f;

    private Coroutine hideUICoroutine;

    void Start()
    {
        UpdateYarnUI();
        yarnUI.alpha = 0;
    }

    public void CollectYarn()
    {
        collectedYarn++;
        UpdateYarnUI();

        StopAllCoroutines();
        StartCoroutine(SlideUIIn());
        hideUICoroutine = StartCoroutine(HideUIAfterDelay());
    }

    void UpdateYarnUI()
    {
        yarnText.text = $"Yarn: {collectedYarn}/{totalYarn}";
    }

    IEnumerator SlideUIIn()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yarnUI.alpha = Mathf.Lerp(yarnUI.alpha, 1, elapsed / duration);
            yield return null;
        }
        yarnUI.alpha = 1;
    }

    IEnumerator HideUIAfterDelay()
    {
        yield return new WaitForSeconds(uiDisplayTime);

        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yarnUI.alpha = Mathf.Lerp(yarnUI.alpha, 0, elapsed / duration);
            yield return null;
        }
        yarnUI.alpha = 0;
    }
}
