using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UI;

public class TriviaGame : MonoBehaviour
{
    public GameObject UI;
    public GameObject TriviaPanel;
    public GameObject ResultPanel;
    public Button[] AnswerButtons = new Button[2];
    public Text Question;
    public Text Answer_1;
    public Text Answer_2;
    public Text Answer_3;
    private int questionCount = 0;
    bool answer_1 = false;
    bool answer_2 = false;
    bool answer_3 = false;
    private int CorrectAnswerCount = 0;
    public Text ResultsText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            questionCount = 0;
            PlayRound();
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
        int tinkerNumber =  Random.Range(1, 8); //generates a number to give the other answerboxes

        answer_1 = answer_2 = answer_3 = false; //resets each question boolean
        int randomPromptNumber = Random.Range(1, 4); // randomizes which answer box gets the correct answer

        if (randomPromptNumber == 1)
        {
            Answer_1.text = product.ToString();
            Answer_2.text = (product+tinkerNumber).ToString();
            Answer_3.text = (product-tinkerNumber).ToString();
            answer_1 = true;
            
        } 
        else if (randomPromptNumber == 2)
        {
            Answer_1.text = (product+tinkerNumber).ToString();
            Answer_2.text = (product).ToString();
            Answer_3.text = (product-tinkerNumber).ToString();
            answer_2 = true;
            
        }
        else if (randomPromptNumber == 3)
        {
            Answer_1.text = (product-tinkerNumber).ToString();
            Answer_2.text = (product+tinkerNumber).ToString();
            Answer_3.text = (product).ToString();
            answer_3 = true;
        }
    }

    public void OnAnswerClick_One()
    {
        if (answer_1 == true)
        {
            CorrectAnswerCount++;
            PlayRound();
        }
        else
        {
            PlayRound();
        }
    }
    
    public void OnAnswerClick_Two()
    {
        if (answer_2 == true)
        {
            CorrectAnswerCount++;
            PlayRound();
        }
        else
        {
            PlayRound();
        }
    }
    
    public void OnAnswerClick_Three()
    {
        if (answer_3 == true)
        {
            CorrectAnswerCount++;
            PlayRound();
        }
        else
        {
            PlayRound();
        }
    }

    public void PlayRound()
    {
        if (questionCount < 3)
        {
            FillQuestion();
            questionCount++;
        }
        else
        {
            TriviaPanel.SetActive(false);
            ResultPanel.SetActive(true);
            DisplayScore();
        }
    }

    public void OnClickRestartRound()
    {
        if (TriviaPanel.activeInHierarchy == true) //checks if you are on Result Screen
        {
            questionCount = 0;
            CorrectAnswerCount = 0;
            PlayRound();
        }
        else
        {
            ResultPanel.SetActive(false);
            UI.SetActive(false); //fixes bug where you can't press quit or restart, could optimize later
            TriviaPanel.SetActive(true);
            UI.SetActive(true);
            questionCount = 0;
            CorrectAnswerCount = 0;
            PlayRound();
        }
        
    }

    public void DisplayScore()
    {
        ResultsText.text = "Score: " + CorrectAnswerCount.ToString() + "/3";
    }
    
}
