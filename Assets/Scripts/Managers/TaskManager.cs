using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Initialize(bool isPhase2) { }
    public TaskData GetActiveTask() => null;
    public void ForceFailActiveTask() { }
    public void OnPlayerReachedNPC(int npcId) { }
}
