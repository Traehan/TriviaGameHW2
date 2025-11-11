using UnityEngine;
using UnityEngine.SceneManagement;

public enum AppState { StartScreen, GameSelection, InGame, Results }
public enum ModeKind { Addition, Subtraction, Multiplication, Division, All }

public class AppStateController : MonoBehaviour
{
    public static AppStateController Instance { get; private set; }

    public AppState Current { get; private set; } = AppState.StartScreen;

    // Player selections / last results (persist across scenes)
    public ModeKind SelectedMode { get; private set; } = ModeKind.Multiplication;
    public int LastCorrect { get; private set; }
    public int LastTotal { get; private set; }
    public float LastTime { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        // Ensure a controller exists before any scene loads
        if (Instance != null) return;
        var go = new GameObject("AppStateController");
        go.AddComponent<AppStateController>();
        DontDestroyOnLoad(go);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSelectedMode(ModeKind mode) => SelectedMode = mode;

    public void GoToStart()
    {
        Current = AppState.StartScreen;
        SceneManager.LoadScene("StartScreen");
    }

    public void GoToSelection()
    {
        Current = AppState.GameSelection;
        SceneManager.LoadScene("GameSelection");
    }

    public void GoToGame()
    {
        Current = AppState.InGame;
        SceneManager.LoadScene("TriviaGame");
    }

    public void GoToResults()
    {
        Current = AppState.Results;
        SceneManager.LoadScene("Results");
    }

    public void ReportResults(int correct, int total, float time)
    {
        LastCorrect = correct;
        LastTotal = total;
        LastTime = time;
    }
}
