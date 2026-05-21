using System.Collections.Generic;

public static class TaskManagerLogic
{
    private static readonly TaskType[] taskTypes = { TaskType.Delivery, TaskType.Email, TaskType.Choice };

    public static List<TaskData> GenerateTasks(bool isPhase2)
    {
        var tasks = new List<TaskData>();
        for (int i = 0; i < Constants.TotalTasks; i++)
        {
            var type = taskTypes[i % taskTypes.Length];
            tasks.Add(new TaskData
            {
                Id = i,
                Type = type,
                SpawnTime = i * Constants.TaskSpawnInterval,
                Duration = GetDurationForType(type),
                NpcId = i
            });
        }
        return tasks;
    }

    private static float GetDurationForType(TaskType type) => type switch
    {
        TaskType.Choice   => Constants.MinigameDurationChoice,
        TaskType.Email    => Constants.MinigameDurationEmail,
        TaskType.Delivery => Constants.MinigameDurationDelivery,
        _                 => 10f
    };
}
