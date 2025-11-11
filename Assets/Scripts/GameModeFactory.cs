public static class GameModeFactory
{
    public static IMathMode Create(ModeKind kind)
    {
        switch (kind)
        {
            case ModeKind.Addition:       return new AdditionMode();
            case ModeKind.Subtraction:    return new SubtractionMode();
            case ModeKind.Multiplication: return new MultiplicationMode();
            case ModeKind.Division:       return new DivisionMode();
            case ModeKind.All:            return new AllMode();
            default:                      return new MultiplicationMode();
        }
    }
}