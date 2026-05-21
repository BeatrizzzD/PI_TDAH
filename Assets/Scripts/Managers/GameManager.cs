using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TaskManager taskManager;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private MinigameManager minigameManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameSceneManager sceneManager;

    public GameState State { get; private set; }
    public bool IsPhase2 { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void StartPhase(bool isPhase2)
    {
        IsPhase2 = isPhase2;
        State = new GameState
        {
            CurrentPhase = isPhase2 ? PhaseType.Phase2 : PhaseType.Phase1,
            State = GameStateType.Playing
        };
        taskManager.Initialize(isPhase2);
        if (isPhase2) eventManager.Initialize();
        uiManager.ActivateHUD();
    }

    private void Update()
    {
        if (State == null || State.State == GameStateType.PhaseComplete) return;
        State.TimeElapsed += Time.deltaTime;
        if (State.TimeElapsed >= State.TimeLimit) EndPhase();
    }

    public void OnTaskCompleted()
    {
        State.CurrentPoints += Constants.PointsPerTask;
        State.TasksCompleted++;
        audioManager.PlaySuccess();
    }

    public void OnTaskFailed()
    {
        State.TasksFailed++;
        audioManager.PlayFailure();
    }

    public void OnMinigameStarted() => State.State = GameStateType.MinigameActive;
    public void OnMinigameEnded() => State.State = GameStateType.Playing;

    public void OnEventStarted(EventData eventData)
    {
        State.State = GameStateType.EventTriggered;
        State.IsEventActive = true;
        State.ActiveEvent = eventData;
    }

    public void OnEventEnded()
    {
        State.State = GameStateType.Playing;
        State.IsEventActive = false;
        State.ActiveEvent = null;
    }

    private void EndPhase()
    {
        State.State = GameStateType.PhaseComplete;
        var result = new PhaseResult
        {
            PointsEarned = State.CurrentPoints,
            TasksCompleted = State.TasksCompleted,
            PhaseName = IsPhase2 ? "Fase 2" : "Fase 1"
        };
        sceneManager.GoToResult(result);
    }
}
