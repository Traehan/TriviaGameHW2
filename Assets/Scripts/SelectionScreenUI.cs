using UnityEngine;

public class SelectionScreenUI : MonoBehaviour
{
    ModeKind chosen = ModeKind.Multiplication;

    public void OnPickAddition()       { chosen = ModeKind.Addition; }
    public void OnPickSubtraction()    { chosen = ModeKind.Subtraction; }
    public void OnPickMultiplication() { chosen = ModeKind.Multiplication; }
    public void OnPickDivision()       { chosen = ModeKind.Division; }
    public void OnPickAll()            { chosen = ModeKind.All; }

    public void OnStartRun()
    {
        AppStateController.Instance.SetSelectedMode(chosen);
        AppStateController.Instance.GoToGame();
    }

    public void OnBack()
    {
        AppStateController.Instance.GoToStart();
    }
}