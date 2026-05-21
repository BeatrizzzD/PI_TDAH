public class GameState
{
    public PhaseType CurrentPhase;
    public int CurrentPoints;
    public int TasksCompleted;
    public int TasksFailed;
    public float TimeElapsed;
    public float TimeLimit = Constants.PhaseDuration;
    public GameStateType State = GameStateType.Playing;
    public bool IsEventActive;
    public EventData ActiveEvent;
}
