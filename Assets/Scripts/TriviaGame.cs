using UnityEngine;
using UnityEngine.UI;

public class TriviaGame : MonoBehaviour
{
    [Header("Scene Loader")]
    public SceneManager sceneManager;

    [Header("Timer")]
    public CountDown roundTimer;     // uses your existing CountDown with onCountdownFinished :contentReference[oaicite:5]{index=5}

    [Header("Question UI")]
    public Text Question;
    public Text Answer_1, Answer_2, Answer_3;
    

    // Command pattern pieces
    private readonly CommandBus _bus = new CommandBus();
    private IMathMode _mode;
    private GameState State => GameSession.Instance.CurrentState;

    private void Awake()
    {
        _mode = new MultiplicationMode(); // swap this for AdditionMode, etc.

        // ensure we only subscribe once to the timer event
        if (roundTimer != null)
        {
            roundTimer.onCountdownFinished -= HandleTimeExpired;
            roundTimer.onCountdownFinished += HandleTimeExpired;
        }
    }

    private void Start()
    {
        StartNewQuestion();
    }
    
    private void StartNewQuestion()
    {
        _bus.Dispatch(new StartRoundCommand(
            State,
            _mode,
            onQuestionReady: q =>
            {
                // bind UI
                Question.text = q.Prompt;
                Answer_1.text = q.Choices[0];
                Answer_2.text = q.Choices[1];
                Answer_3.text = q.Choices[2];
            },
            onTimerRequested: seconds =>
            {
                State.BeginRound();
                roundTimer.StartCountdown(seconds);
            }
        ), record: false);
    }

    private void HandleTimeExpired()
    {
        _bus.Dispatch(new TimeExpiredCommand(
            State,
            getTimeRemaining: () => roundTimer.countdownTime,
            onEndRound: AdvanceOrShowResults
        ));
    }

    private void AdvanceOrShowResults()
    {
        roundTimer.StopCountdown(); // same as your old EndRound flow :contentReference[oaicite:6]{index=6}

        if (State.HasMoreQuestions())
            StartNewQuestion();
        else
            LoadResultsScene();
    }
    
    private void LoadResultsScene()
    {
        AchievementEvents.OnRoundEnded?.Invoke(new AchievementEvents.OnRoundEndedArgs
        {
            NumCorrectQuestions = State.CorrectAnswerCount,
            NumQuestionsAnswered = State.QuestionCount,
            TotalTimeTaken = State.TotalTimeTaken
        });

        sceneManager.LoadSceneByIndex(2);
    }

    // wired to the 3 answer buttons with indices 0,1,2
    public void OnAnswerClick(int answerIndex)
    {
        _bus.Dispatch(new SubmitAnswerCommand(
            State,
            answerIndex,
            getTimeRemaining: () => roundTimer.countdownTime,
            onEndRound: AdvanceOrShowResults
        ));
    }

    // restart from results (wire your Restart button here)
    public void OnClickRestartRound()
    {
        _bus.Dispatch(new RestartGameCommand(
            State,
            onPreReset: () =>
            {
                // make sure no lingering coroutines/timers keep firing
                roundTimer?.StopCountdown(); // your CountDown supports this. :contentReference[oaicite:0]{index=0}
            },
            onShowTrivia: () =>
            {
                sceneManager.LoadSceneByIndex(1);
            }
        ), record: false);
    }
}
