using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UI;

public class TriviaGame : MonoBehaviour
{
    public GameObject TriviaPanel;
    public GameObject QuestionPanel;
    public List<GameObject> Answers = new List<GameObject>();
    public Text Question;
    public Text Answer_1;
    public Text Answer_2;
    public Text Answer_3;
    private int questionCount = 0;
    bool answer_1 = false;
    bool answer_2 = false;
    bool answer_3 = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        while (questionCount <= 3) //runs the game for three rounds
        {
            PlayRound();
        }
        
        //RestartGame()
        //display score
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FillQuestion()
    {
        //randomize integers for the quiz
        int x = Random.Range(0, 12);
        int y = Random.Range(0, 12);
        int product = x * y;

        Question.text = x + "x" + y + "?";
        
        int randomPromptNumber = Random.Range(0, 3); // randomizes which answer box gets the correct answer

        if (randomPromptNumber == 1)
        {
            Answer_1.text = product.ToString();
            Answer_2.text = (product+x).ToString();
            Answer_3.text = (product-y).ToString();
            answer_1 = true;
            
        } 
        else if (randomPromptNumber == 2)
        {
            Answer_1.text = (product+x).ToString();
            Answer_2.text = (product).ToString();
            Answer_3.text = (product-y).ToString();
            answer_2 = true;
            
        }
        else if (randomPromptNumber == 3)
        {
            Answer_1.text = (product+x).ToString();
            Answer_2.text = (product-y).ToString();
            Answer_3.text = (product).ToString();
            answer_3 = true;
        }
    }

    public void PlayRound()
    {
        FillQuestion();
        questionCount++;
    }

    public void OnClickAnswer()
    {
        
    }
    
}
