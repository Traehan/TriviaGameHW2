using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TriviaGame : MonoBehaviour
{

    [Header("Timer")]
    public CountDown roundTimer;

    [Header("Question UI")]
    public Text Question;
    public Text Answer_1, Answer_2, Answer_3;

    private readonly CommandBus _bus = new CommandBus();
    private IMathMode _mode;
    private ModeKind currentMode;
    private GameState State => GameSession.State;

    private void Awake()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;

        currentMode = AppStateController.Instance.SelectedMode;
        var existing = FindObjectsOfType<TriviaGame>();
        if (existing.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        // CHANGED: read selected mode from AppStateController
        _mode = GameModeFactory.Create(AppStateController.Instance.SelectedMode);

        if (roundTimer != null)
        {
            roundTimer.onCountdownFinished -= HandleTimeExpired;
            roundTimer.onCountdownFinished += HandleTimeExpired;
        }
    }

    private void Start()
    {
        StartCoroutine(InitializeAfterFrame());
    }

    private IEnumerator InitializeAfterFrame()
    {
        yield return null;

        Question   = Question  ?? FindTextByName("Question_Text");
        Answer_1   = Answer_1  ?? FindTextByName("AnswerText_1");
        Answer_2   = Answer_2  ?? FindTextByName("AnswerText_2");
        Answer_3   = Answer_3  ?? FindTextByName("AnswerText_3");
        roundTimer = roundTimer ?? GameObject.FindObjectOfType<CountDown>(true);

        if (roundTimer != null)
        {
            roundTimer.onCountdownFinished -= HandleTimeExpired;
            roundTimer.onCountdownFinished += HandleTimeExpired;
        }

        if (roundTimer == null || Question == null)
        {
            Debug.LogError("TriviaGame: Missing required UI references. Check scene setup!");
            yield break;
        }

        // NEW: apply the selected max-questions from the selection screen
        State.MaxQuestions = Mathf.Clamp(AppStateController.Instance.SelectedMaxQuestions, 3, 10);

        StartNewQuestion();
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            Question   = GameObject.Find("QuestionText")?.GetComponent<Text>();
            Answer_1   = GameObject.Find("Answer1Text")?.GetComponent<Text>();
            Answer_2   = GameObject.Find("Answer2Text")?.GetComponent<Text>();
            Answer_3   = GameObject.Find("Answer3Text")?.GetComponent<Text>();
            roundTimer = GameObject.Find("RoundTimer")?.GetComponent<CountDown>();
            if (roundTimer != null)
            {
                roundTimer.onCountdownFinished -= HandleTimeExpired;
                roundTimer.onCountdownFinished += HandleTimeExpired;
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
        if (roundTimer != null)
            roundTimer.onCountdownFinished -= HandleTimeExpired;
    }

    private void StartNewQuestion()
    {
        _bus.Dispatch(new StartRoundCommand(
            State,
            _mode,
            onQuestionReady: q =>
            {
                SafeSetText(Question, q.Prompt);
                SafeSetText(Answer_1, q.Choices[0]);
                SafeSetText(Answer_2, q.Choices[1]);
                SafeSetText(Answer_3, q.Choices[2]);
            },
            onTimerRequested: seconds =>
            {
                State.BeginRound();
                if (roundTimer != null) roundTimer.StartCountdown(seconds);
                else Debug.LogWarning("TriviaGame: roundTimer missing when starting countdown!");
            }
        ), record: false);
    }

    private void HandleTimeExpired()
    {
        if (roundTimer == null) return;

        _bus.Dispatch(new TimeExpiredCommand(
            State,
            getTimeRemaining: () => roundTimer.countdownTime,
            onEndRound: AdvanceOrShowResults
        ));
    }

    private void AdvanceOrShowResults()
    {
        if (roundTimer != null) roundTimer.StopCountdown();

        if (State.HasMoreQuestions()) StartNewQuestion();
        else LoadResultsScene();
    }

    private void LoadResultsScene()
    {
        AchievementEvents.OnRoundEnded?.Invoke(new AchievementEvents.OnRoundEndedArgs
        {
            NumCorrectQuestions   = State.CorrectAnswerCount,
            NumQuestionsAnswered  = State.QuestionCount,
            TotalTimeTaken        = State.TotalTimeTaken,
            CurrentMode         = currentMode
        });

        AppStateController.Instance.GoToResults();
    }

    public void OnAnswerClick(int answerIndex)
    {
        if (roundTimer == null) return;

        _bus.Dispatch(new SubmitAnswerCommand(
            State,
            answerIndex,
            getTimeRemaining: () => roundTimer.countdownTime,
            onEndRound: AdvanceOrShowResults
        ));
    }

    public void OnClickRestartRound()
    {
        if (roundTimer != null)
        {
            roundTimer.onCountdownFinished -= HandleTimeExpired;
            roundTimer.StopCountdown();
        }

        GameSession.State.Reset();
        Destroy(gameObject);
        SceneManager.LoadScene("GameSelectionScene");
    }

    private void SafeSetText(Text target, string value)
    {
        if (target != null) target.text = value;
    }

    private Text FindTextByName(string name)
    {
        foreach (var text in GameObject.FindObjectsOfType<Text>(true))
            if (text.name == name) return text;
        return null;
    }
}
