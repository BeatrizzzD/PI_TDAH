using System.Collections.Generic;
using UnityEngine;

public static class TaskManagerLogic
{
    private static readonly TaskType[] taskTypes = { TaskType.Delivery, TaskType.Email, TaskType.Choice };

    public static List<TaskData> GenerateTasks(bool isPhase2)
    {
        var tasks = new List<TaskData>();
        var shuffled = ShuffleTypes(Constants.TotalTasks);

        for (int i = 0; i < Constants.TotalTasks; i++)
        {
            var type = shuffled[i];
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

    private static List<TaskType> ShuffleTypes(int count)
    {
        var list = new List<TaskType>();
        for (int i = 0; i < count; i++)
            list.Add(taskTypes[i % taskTypes.Length]);

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
        return list;
    }

    private static float GetDurationForType(TaskType type) => type switch
    {
        TaskType.Choice   => Constants.MinigameDurationChoice,
        TaskType.Email    => Constants.MinigameDurationEmail,
        TaskType.Delivery => Constants.MinigameDurationDelivery,
        _                 => 10f
    };
}
