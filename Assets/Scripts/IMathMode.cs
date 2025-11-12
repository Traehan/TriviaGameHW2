public interface IMathMode
{
    // returns a fully formed question (prompt + 3 choices + correct index)
    GameState.Question GenerateQuestion();
}
