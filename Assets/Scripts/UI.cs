using UnityEngine;

public class UI : MonoBehaviour
{
    
    public GameObject StartScreen;
    public GameObject TriviaGame;
    public GameObject OptionsPanel;
    
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
        OptionsPanel.SetActive(true);
        
        
        //start coroutine for 5 seconds
        //reveal GO
        //disable Start Screen ---> Change to Game Screen
        
    }

    public void OnButtonClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode(); // or: UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit(); // quits the built game
#endif
    }
}
