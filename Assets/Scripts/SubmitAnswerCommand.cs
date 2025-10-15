public class SubmitAnswerCommand : ICommand
{
    private readonly GameState _state;
    private readonly int _answerIndex;
    private readonly System.Func<float> _getTimeRemaining;
    private readonly System.Action _onEndRound;
    private bool _wasCorrect;
    private float _timeSnap;

    public SubmitAnswerCommand(GameState state, int answerIndex,
        System.Func<float> getTimeRemaining, System.Action onEndRound)
    {
        _state = state;
        _answerIndex = answerIndex;
        _getTimeRemaining = getTimeRemaining;
        _onEndRound = onEndRound;
    }

    public void Execute()
    {
        if (!_state.RoundActive) return;

        // snapshot time remaining so UI/achievements stay consistent
        _timeSnap = _getTimeRemaining?.Invoke() ?? 0f;
        _wasCorrect = (_answerIndex == _state.CurrentQuestion.CorrectIndex);

        // fire your existing achievement event for answers
        AchievementEvents.OnQuestionAnswered?.Invoke(new AchievementEvents.OnQuestionAnsweredArgs {
            AnsweredCorrectly = _wasCorrect,
            TimeRemaining = _timeSnap
        });

        _state.RecordAnswer(_wasCorrect, _timeSnap);
        _state.EndRound();
        _onEndRound?.Invoke();
    }

    public void Undo() { /* could roll back QuestionCount/score if you add undo UI */ }
}

