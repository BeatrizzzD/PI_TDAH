using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    [SerializeField] private NPCMarker[] npcMarkers;
    [SerializeField] private MinigameManager minigameManager;

    private List<TaskData> tasks;
    private TaskData activeTask;
    private bool isRunning;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Initialize(bool isPhase2)
    {
        tasks = TaskManagerLogic.GenerateTasks(isPhase2);
        isRunning = true;
        activeTask = null;
    }

    private void Update()
    {
        if (!isRunning || GameManager.Instance?.State == null) return;

        float time = GameManager.Instance.State.TimeElapsed;

        if (activeTask == null)
        {
            foreach (var task in tasks)
            {
                if (!task.IsActive && !task.IsCompleted && !task.IsFailed && time >= task.SpawnTime)
                {
                    ActivateTask(task);
                    break;
                }
            }
        }
        else if (!activeTask.IsCompleted && !activeTask.IsFailed)
        {
            float taskElapsed = time - activeTask.SpawnTime;
            if (taskElapsed >= activeTask.Duration)
                FailCurrentTask();
        }
    }

    private void ActivateTask(TaskData task)
    {
        task.IsActive = true;
        activeTask = task;
        GetMarker(task.NpcId)?.ShowIndicator();
    }

    public void OnPlayerReachedNPC(int npcId)
    {
        if (activeTask == null || activeTask.NpcId != npcId) return;
        if (GameManager.Instance.State.State == GameStateType.MinigameActive) return;

        GetMarker(npcId)?.HideIndicator();
        GameManager.Instance.OnMinigameStarted();

        minigameManager.StartMinigame(activeTask, success =>
        {
            if (success) CompleteCurrentTask();
            else FailCurrentTask();
            GameManager.Instance.OnMinigameEnded();
        });
    }

    private void CompleteCurrentTask()
    {
        if (activeTask == null) return;
        activeTask.IsCompleted = true;
        activeTask.IsActive = false;
        GameManager.Instance.OnTaskCompleted();
        activeTask = null;
    }

    public void ForceFailActiveTask()
    {
        if (activeTask == null) return;
        minigameManager.InterruptCurrentMinigame();
        FailCurrentTask();
        GameManager.Instance.OnMinigameEnded();
    }

    private void FailCurrentTask()
    {
        if (activeTask == null) return;
        activeTask.IsFailed = true;
        activeTask.IsActive = false;
        GetMarker(activeTask.NpcId)?.HideIndicator();
        GameManager.Instance.OnTaskFailed();
        activeTask = null;
    }

    public TaskData GetActiveTask() => activeTask;

    private NPCMarker GetMarker(int id)
    {
        foreach (var m in npcMarkers)
            if (m.NpcId == id) return m;
        return null;
    }
}
