using UnityEngine;

public class TimePlayedTracker : MonoBehaviour
{
    [SerializeField] private bool autoStart = false;
    [SerializeField] private bool useUnscaledTime = true;

    private bool isRunning;
    private float accumulator;

    private void Start()
    {
        if (autoStart) StartTracking();
    }

    private void Update()
    {
        if (!isRunning) return;

        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        accumulator += dt;

        // Fire one event per elapsed second
        while (accumulator >= 1f)
        {
            accumulator -= 1f;
            AchievementEvents.OnSecondPassed?.Invoke(); // global “1 second passed” tick  :contentReference[oaicite:3]{index=3}
        }
    }

    public void StartTracking()
    {
        isRunning = true;
    }

    public void StopTracking()
    {
        isRunning = false;
    }

    public void ResetCounter()
    {
        accumulator = 0f;
    }
}

