using UnityEngine;

public class TaskData
{
    public int Id;
    public TaskType Type;
    public Vector2 Location;
    public int NpcId;
    public float SpawnTime;
    public float Duration;
    public int PointsReward = Constants.PointsPerTask;
    public bool IsActive;
    public bool IsCompleted;
    public bool IsFailed;
}
