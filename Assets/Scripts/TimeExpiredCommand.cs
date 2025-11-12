public class TimeExpiredCommand : ICommand
{
    private readonly GameState _state;
    private readonly System.Func<float> _getTimeRemaining;
    private readonly System.Action _onEndRound;

    public TimeExpiredCommand(GameState state,
        System.Func<float> getTimeRemaining, System.Action onEndRound)
    {
        _state = state;
        _getTimeRemaining = getTimeRemaining;
        _onEndRound = onEndRound;
    }

    public void Execute()
    {
        if (!_state.RoundActive) return;

        float t = _getTimeRemaining?.Invoke() ?? 0f;

        // count as incorrect when timer runs out
        AchievementEvents.OnQuestionAnswered?.Invoke(new AchievementEvents.OnQuestionAnsweredArgs {
            AnsweredCorrectly = false,
            TimeRemaining = t
        });

        _state.RecordAnswer(false, t);
        _state.EndRound();
        _onEndRound?.Invoke();
    }

    public void Undo() { }
}