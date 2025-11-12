using UnityEngine;

public class RestartGameCommand : ICommand
{
    private readonly GameState _state;
    private readonly System.Action _onPreReset;     // stop timers, hide results, etc.
    private readonly System.Action _onShowTrivia;   // bring back trivia & start first question

    public RestartGameCommand(GameState state, System.Action onPreReset, System.Action onShowTrivia)
    {
        _state = state;
        _onPreReset = onPreReset;
        _onShowTrivia = onShowTrivia;
    }

    public void Execute()
    {
        _onPreReset?.Invoke();   // e.g., StopCountdown()
        _state.Reset();          // <-- real reset
        _onShowTrivia?.Invoke(); // show trivia panel and StartNewQuestion()
    }

    public void Undo() { }
}
