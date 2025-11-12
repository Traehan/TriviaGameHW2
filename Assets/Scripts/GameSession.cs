using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameState State = new GameState();
    

    public void ResetState()
    {
        State.Reset();
    }
}