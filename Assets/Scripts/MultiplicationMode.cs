using UnityEngine;

public class MultiplicationMode : IMathMode
{
    public GameState.Question GenerateQuestion()
    {
        int x = Random.Range(1, 13);
        int y = Random.Range(1, 13);
        int product = x * y;
        int tinker = Random.Range(1, 8);

        int slot = Random.Range(0, 3); // 0..2
        var choices = new string[3];

        // place correct answer into random slot, fill the others with +/- tinker
        choices[slot] = product.ToString();
        choices[(slot + 1) % 3] = (product + tinker).ToString();
        choices[(slot + 2) % 3] = (product - tinker).ToString();

        return new GameState.Question
        {
            Prompt = $"{x}x{y}?",
            CorrectIndex = slot,
            Choices = choices
        };
    }
}