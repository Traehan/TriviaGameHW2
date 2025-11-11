using UnityEngine;

public class SubtractionMode : IMathMode
{
    public GameState.Question GenerateQuestion()
    {
        int x = Random.Range(1, 21);
        int y = Random.Range(1, 21);
        int ans = x - y;
        int t = Random.Range(1, 8);
        int slot = Random.Range(0, 3);
        var choices = new string[3];
        choices[slot] = ans.ToString();
        choices[(slot+1)%3] = (ans + t).ToString();
        choices[(slot+2)%3] = (ans - t).ToString();
        return new GameState.Question { Prompt = $"{x}+{y}?", CorrectIndex = slot, Choices = choices };
    }
}
