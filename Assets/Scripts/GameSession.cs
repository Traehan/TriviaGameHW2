using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        // ensure singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // create new state at the start of the session
        CurrentState = new GameState();
    }

    public void ResetState()
    {
        CurrentState = new GameState();
    }
}