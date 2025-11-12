using UnityEngine;

public class DivisionMode : IMathMode
{
    public GameState.Question GenerateQuestion()
    {
        int divisor = Random.Range(1, 13);
        int quotient = Random.Range(1, 13);
        int dividend = divisor * quotient; // ensures clean division

        int ans = quotient;
        int t = Random.Range(1, 4);
        int slot = Random.Range(0, 3);
        var choices = new string[3];
        choices[slot] = ans.ToString();
        choices[(slot+1)%3] = Mathf.Max(1, ans + t).ToString();
        choices[(slot+2)%3] = Mathf.Max(1, ans - t).ToString();

        return new GameState.Question { Prompt = $"{dividend}÷{divisor}?", CorrectIndex = slot, Choices = choices };
    }
}