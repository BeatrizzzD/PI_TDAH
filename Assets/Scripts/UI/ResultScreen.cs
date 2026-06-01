using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI tasksText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameSceneManager sceneManager;

    private void Start()
    {
        menuButton.onClick.AddListener(() => sceneManager.GoToMenu());
        var result = GameSceneManager.GetAndClearResult();
        if (result != null) Display(result);
    }

    private void Display(PhaseResult result)
    {
        titleText.text = $"{result.PhaseName} Completa";
        pointsText.text = $"Pontos Ganhos: {result.PointsEarned}/{Constants.TotalTasks * Constants.PointsPerTask}";
        tasksText.text = $"Tarefas Completadas: {result.TasksCompleted}/{Constants.TotalTasks}";
        resultText.text = result.IsVictory
            ? "VITÓRIA - VOCÊ PASSOU!"
            : "DERROTA - Tente novamente.";
    }
}
