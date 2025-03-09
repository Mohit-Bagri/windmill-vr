using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFader : MonoBehaviour
{
    public Text breathInstruction;
    public float fadeDuration = 0.5f; // Smooth transition speed

    private Coroutine fadeRoutine;

    void Start()
    {
        breathInstruction.canvasRenderer.SetAlpha(0f); // Ensure text is invisible at start
    }

    public IEnumerator FadeInText(string newText)
    {
        breathInstruction.text = newText;
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeTextAlpha(0f, 1f));
        yield return fadeRoutine;
    }

    public IEnumerator FadeOutText()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeTextAlpha(1f, 0f));
        yield return fadeRoutine;
        breathInstruction.text = ""; // ✅ Fully hide text after fading out
    }

    public void SetText(string newText)
    {
        breathInstruction.text = newText;
    }

    private IEnumerator FadeTextAlpha(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            breathInstruction.canvasRenderer.SetAlpha(newAlpha);
            yield return null;
        }
        breathInstruction.canvasRenderer.SetAlpha(endAlpha);
    }
}
