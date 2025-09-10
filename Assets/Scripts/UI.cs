using UnityEngine;

public class UI : MonoBehaviour
{

    public GameObject StartButton;
    public GameObject StartScreen;
    public GameObject QuitButton;
    public GameObject TriviaGame;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClickStart()
    {
        //Disable Button
        StartScreen.SetActive(false);
        TriviaGame.SetActive(true);
        
        
        //start coroutine for 5 seconds
        //reveal GO
        //disable Start Screen ---> Change to Game Screen
        
    }

    public void OnButtonClickQuit()
    {
        Application.Quit();
    }
}
