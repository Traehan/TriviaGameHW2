using UnityEngine;

public class AllMode : IMathMode
{
    IMathMode[] modes = new IMathMode[] {
        new AdditionMode(), new SubtractionMode(), new MultiplicationMode(), new DivisionMode()
    };

    public GameState.Question GenerateQuestion()
    {
        int i = Random.Range(0, modes.Length);
        return modes[i].GenerateQuestion();
    }
}