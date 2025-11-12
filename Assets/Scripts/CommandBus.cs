using System.Collections.Generic;

public class CommandBus
{
    private readonly Stack<ICommand> _history = new Stack<ICommand>();

    public void Dispatch(ICommand command, bool record = true)
    {
        command.Execute();
        if (record) _history.Push(command);
    }

    public void UndoLast()
    {
        if (_history.Count == 0) return;
        var last = _history.Pop();
        last.Undo();
    }
}

