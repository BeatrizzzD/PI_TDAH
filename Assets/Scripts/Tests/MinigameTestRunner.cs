using UnityEngine;

public class MinigameTestRunner : MonoBehaviour
{
    [SerializeField] private MinigameManager minigameManager;
    [SerializeField] private TaskType typeToTest = TaskType.Choice;
    [SerializeField] private int taskId = 0;
    [SerializeField] private float duration = 8f;

    [ContextMenu("Run Minigame Test")]
    public void RunTest()
    {
        var task = new TaskData
        {
            Id = taskId,
            Type = typeToTest,
            Duration = duration
        };

        minigameManager.StartMinigame(task, success =>
            Debug.Log($"[MinigameTest] Resultado: {(success ? "SUCESSO" : "FALHA")}"));
    }
}
