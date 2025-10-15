using UnityEngine;

public class StartRoundCommand : ICommand
{
    private readonly GameState _state;
    private readonly IMathMode _mode;
    private readonly System.Action<GameState.Question> _onQuestionReady;
    private readonly System.Action<float> _onTimerRequested;

    public StartRoundCommand(GameState state, IMathMode mode,
        System.Action<GameState.Question> onQuestionReady,
        System.Action<float> onTimerRequested)
    {
        _state = state;
        _mode = mode;
        _onQuestionReady = onQuestionReady;
        _onTimerRequested = onTimerRequested;
    }

    public void Execute()
    {
        var q = _mode.GenerateQuestion();
        _state.NextQuestion(q);
        _onQuestionReady?.Invoke(q);
        _onTimerRequested?.Invoke(_state.PerQuestionTime);
    }

    public void Undo() { /* not needed */ }
}

