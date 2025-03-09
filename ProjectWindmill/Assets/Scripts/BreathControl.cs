using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BreathControl : MonoBehaviour
{
    public TextFader textFader; // Drag and drop in Inspector
    public Text roundCounter;
    public Windmill windmill;

    [Header("Breathing Durations (Adjustable)")]
    public int totalRounds = 5;
    public float startingCountdown = 5f;
    public float inhaleDuration = 3f;
    public float holdDuration = 2f;
    public float exhaleDuration = 4f;
    public float relaxDuration = 2f;

    private int currentRound = 0;

    void Start()
    {
        roundCounter.text = "Round: 0/" + totalRounds; // ✅ Display correct total rounds from start
        StartCoroutine(CountdownBeforeStart());
    }

    IEnumerator CountdownBeforeStart()
    {
        // Fade in "GET READY..."
        yield return StartCoroutine(textFader.FadeInText("Starting in " + (int)startingCountdown + " seconds"));

        for (int timeLeft = (int)startingCountdown; timeLeft > 0; timeLeft--)
        {
            textFader.SetText("Starting in " + timeLeft + " seconds"); // ✅ Smoothly update text without fading
            yield return new WaitForSeconds(1f);
        }

        // Fade out text **without delay** to start breathing
        yield return StartCoroutine(textFader.FadeOutText());

        StartCoroutine(BreathCycle());
    }

    IEnumerator BreathCycle()
{
    while (currentRound < totalRounds)
    {
        currentRound++;
        roundCounter.text = "Round: " + currentRound + "/" + totalRounds;

        // Inhale Phase
        yield return StartCoroutine(UpdateBreathPhase("Inhale", inhaleDuration));

        // Hold Phase
        yield return StartCoroutine(UpdateBreathPhase("Hold", holdDuration));

        // **Exhale Phase - Progressive Speed Windmill**
        windmill.StartWindmill();
        yield return StartCoroutine(UpdateBreathPhase("Exhale", exhaleDuration));

        // **Stop Windmill Gradually**
        windmill.StopWindmill();

        // Relax Phase
        yield return StartCoroutine(UpdateBreathPhase("Relax", relaxDuration));
    }

    yield return StartCoroutine(textFader.FadeInText("Session Complete!"));
}


    IEnumerator UpdateBreathPhase(string phase, float duration)
    {
        yield return StartCoroutine(textFader.FadeInText(phase + " for " + duration + " seconds"));

        for (int timeLeft = (int)duration; timeLeft > 0; timeLeft--)
        {
            textFader.SetText(phase + " for " + timeLeft + " seconds"); // ✅ No fade, just updates text
            yield return new WaitForSeconds(1f);
        }

        yield return StartCoroutine(textFader.FadeOutText()); // ✅ Smooth transition to next phase
    }
}
