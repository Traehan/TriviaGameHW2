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

    // NEW: Max-questions chosen on the selection screen
    public int SelectedMaxQuestions { get; private set; } = 3;

    // Optional “last results” snapshot
    public int LastCorrect { get; private set; }
    public int LastTotal { get; private set; }
    public float LastTime { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
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

    // CHANGED: set the “selected” value, clamp to [3..10]
    public void SetMaxQuestionCount(int count)
    {
        SelectedMaxQuestions = Mathf.Clamp(count, 3, 10);
    }

    public void GoToStart()
    {
        Current = AppState.StartScreen;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToSelection()
    {
        Current = AppState.GameSelection;
        SceneManager.LoadScene("GameSelectionScene");
    }

    public void GoToGame()
    {
        Current = AppState.InGame;
        SceneManager.LoadScene("Game");
    }

    public void GoToResults()
    {
        Current = AppState.Results;
        SceneManager.LoadScene("ResultsScreen");
    }

    public void ReportResults(int correct, int total, float time)
    {
        LastCorrect = correct;
        LastTotal  = total;
        LastTime   = time;
    }
}
