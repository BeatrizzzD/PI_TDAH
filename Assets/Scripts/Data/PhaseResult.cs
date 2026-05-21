public class PhaseResult
{
    public bool IsVictory => TasksCompleted >= Constants.TasksToWin;
    public int PointsEarned;
    public int TasksCompleted;
    public string PhaseName;
}
