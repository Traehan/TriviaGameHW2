using UnityEngine;
using UnityEngine.UI;

public class GameSelectionUI : MonoBehaviour
{
    [Header("Mode Buttons (optional)")]
    public Button additionBtn;
    public Button subtractionBtn;
    public Button multiplicationBtn;
    public Button divisionBtn;
    public Button allBtn;

    [Header("Max Questions UI")]
    public Slider maxQuestionsSlider;   // min=3, max=10, whole numbers
    public Text maxQuestionsValueText;  // shows “3 … 10”

    [Header("Start")]
    public Button startButton;

    private void Awake()
    {
        // Bind mode buttons if assigned
        additionBtn?.onClick.AddListener(() => AppStateController.Instance.SetSelectedMode(ModeKind.Addition));
        subtractionBtn?.onClick.AddListener(() => AppStateController.Instance.SetSelectedMode(ModeKind.Subtraction));
        multiplicationBtn?.onClick.AddListener(() => AppStateController.Instance.SetSelectedMode(ModeKind.Multiplication));
        divisionBtn?.onClick.AddListener(() => AppStateController.Instance.SetSelectedMode(ModeKind.Division));
        allBtn?.onClick.AddListener(() => AppStateController.Instance.SetSelectedMode(ModeKind.All));

        // Slider setup
        if (maxQuestionsSlider != null)
        {
            maxQuestionsSlider.wholeNumbers = true;
            maxQuestionsSlider.minValue = 3;
            maxQuestionsSlider.maxValue = 10;
            maxQuestionsSlider.value = Mathf.Clamp(AppStateController.Instance.SelectedMaxQuestions, 3, 10);
            maxQuestionsSlider.onValueChanged.AddListener(OnMaxQuestionsChanged);
            OnMaxQuestionsChanged(maxQuestionsSlider.value);
        }

        // Start button
        startButton?.onClick.AddListener(OnStartClicked);
    }

    private void OnDestroy()
    {
        // Remove listeners to avoid ghost refs if you reload this scene
        additionBtn?.onClick.RemoveAllListeners();
        subtractionBtn?.onClick.RemoveAllListeners();
        multiplicationBtn?.onClick.RemoveAllListeners();
        divisionBtn?.onClick.RemoveAllListeners();
        allBtn?.onClick.RemoveAllListeners();
        startButton?.onClick.RemoveAllListeners();
        if (maxQuestionsSlider != null)
            maxQuestionsSlider.onValueChanged.RemoveAllListeners();
    }

    private void OnMaxQuestionsChanged(float raw)
    {
        int clamped = Mathf.Clamp(Mathf.RoundToInt(raw), 3, 10);
        AppStateController.Instance.SetMaxQuestionCount(clamped);
        if (maxQuestionsValueText) maxQuestionsValueText.text = clamped.ToString();
    }

    private void OnStartClicked()
    {
        // Go to Game scene; TriviaGame will read the selected mode & max-questions
        AppStateController.Instance.GoToGame();
    }
}
