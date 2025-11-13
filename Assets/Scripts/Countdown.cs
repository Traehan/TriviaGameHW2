using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class CountDown : MonoBehaviour
{
    public float countdownTime;
    public Text countdownText;

    public event Action onCountdownFinished;

    private Coroutine timerCoroutine;
    private bool finished = false;
    public bool IsRunning => timerCoroutine != null;
    public bool HasFinished => finished;

    private void OnDisable()
    {
        Debug.Log("[CountDown] OnDisable() called!");
    }

    private void OnEnable()
    {
        Debug.Log("[CountDown] OnEnable() called!");
    }
    public void StartCountdown(float startTime)
    {
        Debug.Log($"[CountDown] StartCountdown called again? timerCoroutine={(timerCoroutine != null)}");
        StopCountdown();
        finished = false;
        countdownTime = startTime;
        UpdateCountdownDisplay();
        timerCoroutine = StartCoroutine(CountdownCoroutine());
    }

    public void StopCountdown()
    {
        if (timerCoroutine != null)
        {
            UnityEngine.Debug.Log("[CountDown] StopCountdown() - stopping coroutine.");
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void ResetCountdown(float newTime)
    {
        UnityEngine.Debug.Log($"[CountDown] ResetCountdown({newTime}) called.");
        StopCountdown();
        finished = false;
        countdownTime = newTime;
        UpdateCountdownDisplay();
        timerCoroutine = StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        UnityEngine.Debug.Log("[CountDown] Coroutine started. initialTime=" + countdownTime);

        while (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            UpdateCountdownDisplay();

            // debug each frame (can be noisy, remove if too spammy)
            UnityEngine.Debug.Log($"[CountDown] ticking - time={countdownTime:F3}, delta={Time.deltaTime:F4}, timescale={Time.timeScale}");

            yield return null;
        }

        countdownTime = 0;
        UpdateCountdownDisplay();

        timerCoroutine = null;
        finished = true;

        // print stack trace so you can see where listeners were added/called from
        var st = new StackTrace(true);
        UnityEngine.Debug.Log("[CountDown] Countdown finished naturally. Firing onCountdownFinished. StackTrace:\n" + st.ToString());

        try
        {
            onCountdownFinished?.Invoke();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError("[CountDown] Exception invoking onCountdownFinished: " + ex);
        }
    }

    public void UpdateCountdownDisplay()
    {
        int total = Mathf.CeilToInt(countdownTime);
        total = Mathf.Max(total, 1); // note: this keeps "1" until the loop exits
        int minutes = total / 60;
        int seconds = total % 60;
        if (countdownText != null) countdownText.text = $"{minutes:00}:{seconds:00}";
    }
}
